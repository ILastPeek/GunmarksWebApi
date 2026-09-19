using GunmarksWebApi.Domain.Entities;

namespace GunmarksWebApi.Repositories.Abstract
{
    public interface ITankTypeRepository
    {
        Task<IEnumerable<TankType>> GetAllAsync();
        Task<TankType> GetByIdAsync(int id);
    }
}