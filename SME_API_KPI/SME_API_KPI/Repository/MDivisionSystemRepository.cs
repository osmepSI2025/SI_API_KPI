using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SME_API_KPI.Repository
{
    public class MDivisionRepository
    {
        private readonly KPIDBContext _context;

        public MDivisionRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MDivision>> GetAllAsync()
        {
            try
            {
                return await _context.MDivisions.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MDivision>();
            }
        }

        public async Task<MDivision?> GetByIdAsync(string id)
        {
            try
            {
                return await _context.MDivisions.FirstOrDefaultAsync(x => x.Divisionid == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MDivision entity)
        {
            try
            {
                _context.MDivisions.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MDivision entity)
        {
            try
            {
                _context.MDivisions.Update(entity);
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
                var entity = await _context.MDivisions.FindAsync(id);
                if (entity == null) return false;
                _context.MDivisions.Remove(entity);
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
