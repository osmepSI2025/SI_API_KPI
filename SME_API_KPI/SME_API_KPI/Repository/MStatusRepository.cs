using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;

namespace SME_API_KPI.Repository
{
    public class MStatusRepository 
    {
        private readonly KPIDBContext _context;

        public MStatusRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MStatus>> GetAllAsync()
        {
            return await _context.MStatuses.ToListAsync();
        }

        public async Task<MStatus?> GetByIdAsync(int masterid)
        {
            return await _context.MStatuses.FirstOrDefaultAsync(e => e.Masterid == masterid);
        }

        public async Task<bool> AddAsync(MStatus entity)
        {
            try
            {
                await _context.MStatuses.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                // Optionally log the exception
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MStatus entity)
        {
            _context.MStatuses.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MStatuses.FindAsync(id);
            if (entity == null) return false;
            _context.MStatuses.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
