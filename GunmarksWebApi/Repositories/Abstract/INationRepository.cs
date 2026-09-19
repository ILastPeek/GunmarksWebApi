using GunmarksWebApi.Domain.Entities;

namespace GunmarksWebApi.Repositories.Abstract
{
    public interface INationRepository
    {
        Task<IEnumerable<Nation>> GetAllAsync();
        Task<Nation> GetByIdAsync(int id);
    }
}