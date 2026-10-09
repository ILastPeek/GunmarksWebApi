using GunmarksWebApi.Domain.Entities;

namespace GunmarksWebApi.Repositories.Abstract
{
    public interface INationRepository
    {
        IQueryable<Nation> GetAll();
        Task<Nation?> GetByIdAsync(int id);
    }
}