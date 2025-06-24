using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;

namespace SME_API_KPI.Repository
{
    public class MKpiTypeRepository 
    {
        private readonly KPIDBContext _context;

        public MKpiTypeRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MKpiType>> GetAllAsync()
        {
            return await _context.MKpiTypes.ToListAsync();
        }

        public async Task<MKpiType?> GetByIdAsync(int masterid)
        {
            return await _context.MKpiTypes.FirstOrDefaultAsync(e => e.Masterid == masterid);
        }

        public async Task<bool> AddAsync(MKpiType entity)
        {
            try
            {
                await _context.MKpiTypes.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Optionally log the exception
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MKpiType entity)
        {
            _context.MKpiTypes.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MKpiTypes.FindAsync(id);
            if (entity == null) return false;
            _context.MKpiTypes.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
