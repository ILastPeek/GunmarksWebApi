using GunmarksWebApi.Domain.Entities;
using GunmarksWebApi.DTOs;
using GunmarksWebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GunmarksWebApi.Services
{
    public class NationService : INationService
    {
        private readonly INationRepository _nationRepository;

        //подтягиваю бд
        public NationService(INationRepository nationRepository)
        {
            _nationRepository = nationRepository;
        }

        //мапинг
        private static NationDto MapToDto(Nation nation)
        {
            return new NationDto
            {
                Id = nation.Id,
                Name = nation.Name,
                CssClass = nation.CssClass
            };
        }

        //возвращаю список дто наций
        public async Task<List<NationDto>> GetNationDtosAsync()
        {
            var nations = await _nationRepository.GetAll().ToListAsync();
            return nations.Select(n => MapToDto(n)).ToList();
        }
    }
}
