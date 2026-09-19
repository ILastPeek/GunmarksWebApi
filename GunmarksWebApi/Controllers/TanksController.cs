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
    }
}