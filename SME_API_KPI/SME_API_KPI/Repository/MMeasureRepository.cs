using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SME_API_KPI.Repository
{
    public class MMeasureRepository
    {
        private readonly KPIDBContext _context;

        public MMeasureRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MMeasure>> GetAllAsync()
        {
            try
            {
                return await _context.MMeasures.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<MMeasure?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MMeasures.FirstOrDefaultAsync(e=>e.Masterid == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MMeasure entity)
        {
            try
            {
                _context.MMeasures.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MMeasure entity)
        {
            try
            {
                _context.MMeasures.Update(entity);
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
                var entity = await _context.MMeasures.FindAsync(id);
                if (entity == null) return false;
                _context.MMeasures.Remove(entity);
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
