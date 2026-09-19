using GunmarksWebApi.DTOs;

namespace GunmarksWebApi.Services
{
    public interface ITankService
    {
        // Получить список танков с фильтрацией, сортировкой и пагинацией
        Task<PaginatedResultDto<TankDto>> GetTanksAsync(TankFilterDto filter);

        // Получить один танк по Id
        Task<TankDto?> GetByIdAsync(int id);
    }
}