using Microsoft.EntityFrameworkCore;
using GunmarksWebApi.Data;
using GunmarksWebApi.Domain.Entities;
using GunmarksWebApi.Repositories.Abstract;

namespace GunmarksWebApi.Repositories.EntityFramework
{
    public class EFNationRepository : INationRepository
    {
        private readonly AppDbContext _context;

        public EFNationRepository(AppDbContext context)
        {
            _context = context;
        }

        public  IQueryable<Nation> GetAll()
        {
            return _context.Nations;
        }

        public async Task<Nation?> GetByIdAsync(int id)
        {
            return await _context.Nations.FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}