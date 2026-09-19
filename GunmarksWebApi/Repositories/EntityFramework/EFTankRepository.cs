using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Data;
using GunmarksWebApi.Domain.Entities;
using GunmarksWebApi.Repositories.Abstract;

namespace GunmarksWebApi.Repositories.EntityFramework
{
    public class EFTankRepository : ITankRepository
    {
        private readonly AppDbContext _context;

        // DI-контейнер передаст AppDbContext автоматически
        public EFTankRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Tank> GetAll()
        {
            // Include — подгружаем связанные Nation и TankType одним SQL-запросом.
            // Без Include EF сделает отдельный запрос на каждый танк (проблема N+1).
            return _context.Tanks
                .Include(t => t.Nation)
                .Include(t => t.TankType);
        }

        public async Task<Tank> GetByIdAsync(int id)
        {
            return await _context.Tanks
                .Include(t => t.Nation)
                .Include(t => t.TankType)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Tank entity)
        {
            await _context.Tanks.AddAsync(entity);
        }

        public void Update(Tank entity)
        {
            _context.Tanks.Update(entity);
        }

        public void Delete(Tank entity)
        {
            _context.Tanks.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}