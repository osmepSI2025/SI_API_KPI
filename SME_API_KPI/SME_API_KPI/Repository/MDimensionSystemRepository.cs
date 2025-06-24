using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SME_API_KPI.Repository
{
    public class MDimensionSystemRepository
    {
        private readonly KPIDBContext _context;

        public MDimensionSystemRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MDimensionSystem>> GetAllAsync()
        {
            try
            {
                return await _context.MDimensionSystems.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MDimensionSystem>();
            }
        }

        public async Task<MDimensionSystem?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MDimensionSystems.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MDimensionSystem entity)
        {
            try
            {
                _context.MDimensionSystems.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MDimensionSystem entity)
        {
            try
            {
                _context.MDimensionSystems.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.MDimensionSystems.FindAsync(id);
                if (entity == null) return false;
                _context.MDimensionSystems.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
