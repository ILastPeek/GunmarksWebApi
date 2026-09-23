using Microsoft.AspNetCore.Mvc;
using GunmarksWebApi.DTOs;
using GunmarksWebApi.Services;

namespace GunmarksWebApi.Controllers
{
    // [ApiController] включает правила API:
    // - автоматическая валидация модели
    // - автоматическая привязка параметров из query/route/body
    // - если модель невалидна — вернётся 400 Bad Request
    [ApiController]

    // [Route] задаёт базовый URL.
    // "[controller]" — это подстановка имени класса без суффикса "Controller".
    // TanksController → /api/tanks
    [Route("api/[controller]")]
    public class TanksController : ControllerBase
    {
        private readonly ITankService _tankService;

        // DI-контейнер передаст ITankService в конструктор
        public TanksController(ITankService tankService)
        {
            _tankService = tankService;
        }

        // ============================================================
        // GET /api/tanks
        // Получить список танков с фильтрацией, сортировкой, пагинацией
        // ============================================================
        // Пример запроса:
        //   GET /api/tanks?level=10&sortOrder=mark3_desc&pageNumber=1&pageSize=20
        //
        // [FromQuery] означает: параметр читается из query-строки.
        // ASP.NET Core сам сопоставит имена в URL с именами свойств TankFilterDto.
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<TankDto>>> GetTanks([FromQuery] TankFilterDto filter)
        {
            var result = await _tankService.GetTanksAsync(filter);

            // Ok() возвращает 200 OK с JSON в теле
            return Ok(result);
        }

        // ============================================================
        // GET /api/tanks/{id}
        // Получить один танк по Id
        // ============================================================
        // Пример запроса:
        //   GET /api/tanks/5
        //
        // {id} в маршруте — это часть URL.
        // ASP.NET Core подставит значение из URL в параметр метода.
        // ============================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<TankDto>> GetTankById(int id)
        {
            var tank = await _tankService.GetByIdAsync(id);

            // Если танк не найден — возвращаем 404 Not Found
            if (tank == null)
                return NotFound();

            // Иначе — 200 OK с танком
            return Ok(tank);
        }

        // ============================================================
        // POST /api/tanks
        // Создать новый танк
        // ============================================================
        // [FromBody] — параметр читается из тела запроса (JSON).
        // [ApiController] сам десериализует JSON в CreateTankDto.
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<TankDto>> CreateTank([FromBody] CreateTankDto dto)
        {
            var created = await _tankService.CreateAsync(dto);

            // CreatedAtAction возвращает 201 Created и заголовок Location
            // с URL нового ресурса: /api/tanks/{id}
            return CreatedAtAction(
                nameof(GetTankById),   // имя метода для генерации URL
                new { id = created.Id }, // параметры для URL
                created);              // тело ответа
        }

        // ============================================================
        // PUT /api/tanks/{id}
        // Обновить существующий танк
        // ============================================================
        [HttpPut("{id}")]
        public async Task<ActionResult<TankDto>> UpdateTank(int id, [FromBody] UpdateTankDto dto)
        {
            var updated = await _tankService.UpdateAsync(id, dto);

            // Если танк не найден — 404
            if (updated == null)
                return NotFound();

            // Иначе — 200 OK с обновлённым танком
            return Ok(updated);
        }

        // ============================================================
        // DELETE /api/tanks/{id}
        // Удалить танк
        // ============================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTank(int id)
        {
            var success = await _tankService.DeleteAsync(id);

            // Если не найден — 404
            if (!success)
                return NotFound();

            // Иначе — 204 No Content (удалено, тело не нужно)
            return NoContent();
        }
    }
}