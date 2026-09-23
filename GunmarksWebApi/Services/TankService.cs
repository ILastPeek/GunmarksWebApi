using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Domain.Entities;
using GunmarksWebApi.Domain.Enums;
using GunmarksWebApi.DTOs;
using GunmarksWebApi.Repositories.Abstract;

namespace GunmarksWebApi.Services
{
    public class TankService : ITankService
    {
        private readonly ITankRepository _tankRepository;

        // DI-контейнер передаст сюда ITankRepository
        public TankService(ITankRepository tankRepository)
        {
            _tankRepository = tankRepository;
        }

        // ============================================================
        // ПОЛУЧИТЬ СПИСОК ТАНКОВ С ФИЛЬТРАЦИЕЙ, СОРТИРОВКОЙ И ПАГИНАЦИЕЙ
        // ============================================================
        public async Task<PaginatedResultDto<TankDto>> GetTanksAsync(TankFilterDto filter)
        {
            // ============================================================
            // 1. НАЧИНАЕМ С БАЗОВОГО ЗАПРОСА (IQueryable)
            // ============================================================
            var query = _tankRepository.GetAll();

            // ============================================================
            // 2. ФИЛЬТРАЦИЯ
            // ============================================================

            // Поиск по имени
            if (!string.IsNullOrWhiteSpace(filter.SearchName))
            {
                query = query.Where(t => t.Name.Contains(filter.SearchName));
            }

            // Нация
            if (filter.NationId.HasValue)
            {
                query = query.Where(t => t.NationId == filter.NationId.Value);
            }

            // Тип техники
            if (filter.TankTypeId.HasValue)
            {
                query = query.Where(t => t.TankTypeId == filter.TankTypeId.Value);
            }

            // Уровень
            if (filter.Level.HasValue)
            {
                query = query.Where(t => t.Level == filter.Level.Value);
            }

            // Статус (приводим enum к int)
            if (filter.Status.HasValue)
            {
                query = query.Where(t => (int)t.Status == filter.Status.Value);
            }

            // ============================================================
            // 3. СОРТИРОВКА
            // ============================================================
            query = filter.SortOrder switch
            {
                "name" => query.OrderBy(t => t.Name).ThenByDescending(t => t.Mark3),
                "name_desc" => query.OrderByDescending(t => t.Name).ThenByDescending(t => t.Mark3),

                "level" => query.OrderBy(t => t.Level).ThenByDescending(t => t.Mark3),
                "level_desc" => query.OrderByDescending(t => t.Level).ThenByDescending(t => t.Mark3),

                "nation" => query.OrderBy(t => t.Nation.Name).ThenBy(t => t.Name),
                "nation_desc" => query.OrderByDescending(t => t.Nation.Name).ThenBy(t => t.Name),

                "tankType" => query.OrderBy(t => t.TankType.ShortName).ThenBy(t => t.Name),
                "tankType_desc" => query.OrderByDescending(t => t.TankType.ShortName).ThenBy(t => t.Name),

                "mark1" => query.OrderBy(t => t.Mark1).ThenBy(t => t.Name),
                "mark1_desc" => query.OrderByDescending(t => t.Mark1).ThenBy(t => t.Name),

                "mark2" => query.OrderBy(t => t.Mark2).ThenBy(t => t.Name),
                "mark2_desc" => query.OrderByDescending(t => t.Mark2).ThenBy(t => t.Name),

                "mark3" => query.OrderBy(t => t.Mark3).ThenBy(t => t.Name),
                "mark3_desc" => query.OrderByDescending(t => t.Mark3).ThenBy(t => t.Name),

                // По умолчанию — по 3-й отметке убывание
                _ => query.OrderByDescending(t => t.Mark3).ThenBy(t => t.Name)
            };

            // ============================================================
            // 4. ПАГИНАЦИЯ
            // ============================================================
            var totalItems = await query.CountAsync();

            var tanks = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // ============================================================
            // 5. МАППИНГ: Tank (Entity) → TankDto
            // ============================================================
            var tankDtos = tanks.Select(MapToDto).ToList();

            // ============================================================
            // 6. ОБОРАЧИВАЕМ В PaginatedResultDto И ВОЗВРАЩАЕМ
            // ============================================================
            return new PaginatedResultDto<TankDto>
            {
                Items = tankDtos,
                TotalItems = totalItems,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        // ============================================================
        // ПОЛУЧИТЬ ОДИН ТАНК ПО ID
        // ============================================================
        public async Task<TankDto?> GetByIdAsync(int id)
        {
            var tank = await _tankRepository.GetByIdAsync(id);

            // Если танк не найден — возвращаем null
            if (tank == null)
                return null;

            return MapToDto(tank);
        }

        // ============================================================
        // ВСПОМОГАТЕЛЬНЫЙ МЕТОД: Entity → DTO
        // ============================================================
        // Приватный — используется только внутри сервиса.
        // Если бы он понадобился снаружи — вынесли бы в отдельный класс Mapper.
        private static TankDto MapToDto(Tank tank)
        {
            return new TankDto
            {
                Id = tank.Id,
                Name = tank.Name,
                Level = tank.Level,
                LevelRoman = tank.LevelRoman,
                Mark1 = tank.Mark1,
                Mark2 = tank.Mark2,
                Mark3 = tank.Mark3,

                NationId = tank.NationId,
                NationName = tank.Nation?.Name ?? string.Empty,
                NationCssClass = tank.Nation?.CssClass ?? string.Empty,

                TankTypeId = tank.TankTypeId,
                TankTypeName = tank.TankType?.Name ?? string.Empty,
                TankTypeShortName = tank.TankType?.ShortName ?? string.Empty,
                TankTypeCssClass = tank.TankType?.CssClass ?? string.Empty,

                Status = tank.Status.ToString()
            };
        }

        // ============================================================
        // СОЗДАТЬ НОВЫЙ ТАНК
        // ============================================================
        public async Task<TankDto> CreateAsync(CreateTankDto dto)
        {
            // Проверяем, что нация и тип существуют
            // (чтобы не получить ошибку внешнего ключа при сохранении)
            // Для этого нам нужны репозитории — но их сейчас нет в TankService.
            // Пока пропустим эту проверку — но в реальном проекте её надо делать.

            var tank = new Tank
            {
                Name = dto.Name,
                Level = dto.Level,
                Mark1 = dto.Mark1,
                Mark2 = dto.Mark2,
                Mark3 = dto.Mark3,
                Status = (TankStatus)dto.Status,
                NationId = dto.NationId,
                TankTypeId = dto.TankTypeId
            };

            await _tankRepository.AddAsync(tank);
            await _tankRepository.SaveChangesAsync();

            // Перечитываем танк, чтобы подгрузить навигационные свойства Nation и TankType
            var created = await _tankRepository.GetByIdAsync(tank.Id);

            return MapToDto(created!);
        }

        // ============================================================
        // ОБНОВИТЬ ТАНК
        // ============================================================
        public async Task<TankDto?> UpdateAsync(int id, UpdateTankDto dto)
        {
            // Ищем танк по Id
            var tank = await _tankRepository.GetByIdAsync(id);

            // Если не найден — возвращаем null (контроллер вернёт 404)
            if (tank == null)
                return null;

            // Обновляем поля
            tank.Name = dto.Name;
            tank.Level = dto.Level;
            tank.Mark1 = dto.Mark1;
            tank.Mark2 = dto.Mark2;
            tank.Mark3 = dto.Mark3;
            tank.Status = (TankStatus)dto.Status;
            tank.NationId = dto.NationId;
            tank.TankTypeId = dto.TankTypeId;

            _tankRepository.Update(tank);
            await _tankRepository.SaveChangesAsync();

            // Перечитываем, чтобы обновлённые связи (Nation/TankType) были актуальны
            var updated = await _tankRepository.GetByIdAsync(tank.Id);
            return MapToDto(updated!);
        }

        // ============================================================
        // УДАЛИТЬ ТАНК
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var tank = await _tankRepository.GetByIdAsync(id);

            // Если не найден — false
            if (tank == null)
                return false;

            _tankRepository.Delete(tank);
            await _tankRepository.SaveChangesAsync();
            return true;
        }
    }
}