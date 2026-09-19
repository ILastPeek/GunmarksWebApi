using Burn.Domain.Entities;

namespace Burn.Repositories.Abstract
{
    public interface INationRepository
    {
        Task<IEnumerable<Nation>> GetAllAsync();
        Task<Nation> GetByIdAsync(int id);
    }
}