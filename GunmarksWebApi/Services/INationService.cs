using GunmarksWebApi.Domain.Entities;
using GunmarksWebApi.DTOs;

namespace GunmarksWebApi.Services
{
    public interface INationService
    {
        Task<List<NationDto>> GetNationDtosAsync();

    }
}
