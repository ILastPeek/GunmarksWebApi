using Microsoft.EntityFrameworkCore;
using Burn.Data;
using Burn.Domain.Entities;
using Burn.Repositories.Abstract;

namespace Burn.Repositories.EntityFramework
{
    public class EFTankTypeRepository : ITankTypeRepository
    {
        private readonly AppDbContext _context;

        public EFTankTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TankType>> GetAllAsync()
        {
            return await _context.TankTypes
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<TankType> GetByIdAsync(int id)
        {
            return await _context.TankTypes.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}