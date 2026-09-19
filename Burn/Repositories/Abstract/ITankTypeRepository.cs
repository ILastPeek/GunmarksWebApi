using Burn.Domain.Entities;

namespace Burn.Repositories.Abstract
{
    public interface ITankTypeRepository
    {
        Task<IEnumerable<TankType>> GetAllAsync();
        Task<TankType> GetByIdAsync(int id);
    }
}