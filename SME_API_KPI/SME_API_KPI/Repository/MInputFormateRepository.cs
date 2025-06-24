using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SME_API_KPI.Repository
{
    public class MInputFormateRepository
    {
        private readonly KPIDBContext _context;

        public MInputFormateRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MInputFormate>> GetAllAsync()
        {
            try
            {
                return await _context.MInputFormates.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MInputFormate>();
            }
        }

        public async Task<MInputFormate?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MInputFormates.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MInputFormate entity)
        {
            try
            {
                _context.MInputFormates.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MInputFormate entity)
        {
            try
            {
                _context.MInputFormates.Update(entity);
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
                var entity = await _context.MInputFormates.FindAsync(id);
                if (entity == null) return false;
                _context.MInputFormates.Remove(entity);
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
