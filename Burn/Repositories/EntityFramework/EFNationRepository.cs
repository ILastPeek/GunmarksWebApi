using Microsoft.EntityFrameworkCore;
using Burn.Data;
using Burn.Domain.Entities;
using Burn.Repositories.Abstract;

namespace Burn.Repositories.EntityFramework
{
    public class EFNationRepository : INationRepository
    {
        private readonly AppDbContext _context;

        public EFNationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nation>> GetAllAsync()
        {
            return await _context.Nations
                .OrderBy(n => n.Name) // сортировка по алфавиту
                .ToListAsync();
        }

        public async Task<Nation> GetByIdAsync(int id)
        {
            return await _context.Nations.FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}