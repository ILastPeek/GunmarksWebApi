using GunmarksWebApi.DTOs;

namespace GunmarksWebApi.Services
{
    public interface ITankService
    {
        // Read
        Task<PaginatedResultDto<TankDto>> GetTanksAsync(TankFilterDto filter);
        Task<TankDto?> GetByIdAsync(int id);

        // Create
        Task<TankDto> CreateAsync(CreateTankDto dto);

        // Update
        Task<TankDto?> UpdateAsync(int id, UpdateTankDto dto);

        // Delete
        Task<bool> DeleteAsync(int id);
    }
}