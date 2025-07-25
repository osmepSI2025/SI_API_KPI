using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MExportEvalSystemRepository
    {
        private readonly KPIDBContext _context;

        public MExportEvalSystemRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MExportEval>> GetAllAsync(searchMExportEvalModels models)
        {
            try
            {
                var query = _context.MExportEvals.AsQueryable();

                // Example: If MExportEval has properties named PlanId and PeriodId
                if (!string.IsNullOrEmpty(models.planID))
                {
                    query = query.Where(x => x.PlanId == models.planID);
                }
                
                return await query.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MExportEval>();
            }
        }
        public async Task<MExportEval?> GetByIdAsync(string? planId,int? seq)
        {
            try
            {
                return await _context.MExportEvals.FirstOrDefaultAsync(e => e.PlanId == planId && e.Seq == seq);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MExportEval entity)
        {
            try
            {
                _context.MExportEvals.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MExportEval entity)
        {
            try
            {
                _context.MExportEvals.Update(entity);
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
                var entity = await _context.MExportEvals.FindAsync(id);
                if (entity == null) return false;
                _context.MExportEvals.Remove(entity);
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
