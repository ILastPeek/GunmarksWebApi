using GunmarksWebApi.DTOs;
using GunmarksWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GunmarksWebApi.Controllers
{
    [ApiController]
    [Route("api/nations")]
    public class NationsController: ControllerBase
    {
        private readonly INationService _nationService;  

        public NationsController(INationService nationService)
        {
            _nationService = nationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NationDto>>> GetAllNation()
        {
            var result = await _nationService.GetNationDtosAsync();

            return Ok(result);
        }
    }
}
