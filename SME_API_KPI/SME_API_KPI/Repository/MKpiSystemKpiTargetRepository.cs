using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MKpiSystemKpiTargetRepository
    {
        private readonly KPIDBContext _context;

        public MKpiSystemKpiTargetRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MKpiSystemKpiTarget>> GetAllAsync()
        {
            return await _context.MKpiSystemKpiTargets
                .Include(x => x.TKpiSystemKpiTargets)
                .ThenInclude(t => t.TKpiSystemKpiTargetLevels)
                .ToListAsync();
        }

        public async Task<MKpiSystemKpiTarget?> GetByIdAsync(string? kpiId,string? planId)
        {
            return await _context.MKpiSystemKpiTargets
                .Include(x => x.TKpiSystemKpiTargets)
                .ThenInclude(t => t.TKpiSystemKpiTargetLevels)
                .FirstOrDefaultAsync(x => x.PlanId == planId && x.KpiId==kpiId);
        }

        public async Task<bool> AddAsync(MKpiSystemKpiTarget entity)
        {
            _context.MKpiSystemKpiTargets.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(MKpiSystemKpiTarget entity)
        {
            _context.MKpiSystemKpiTargets.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MKpiSystemKpiTargets.FindAsync(id);
            if (entity == null) return false;
            _context.MKpiSystemKpiTargets.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MKpiSystemKpiTarget>> GetAllAsyncSearch_MPlanKpiTarget(searchMPlanKpiTargetModels searchModel)
        {
            try
            {
                var query = _context.MKpiSystemKpiTargets
                    .Include(x => x.TKpiSystemKpiTargets)
                        .ThenInclude(t => t.TKpiSystemKpiTargetLevels)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchModel.Planid))
                {
                    query = query.Where(bu => bu.PlanId == searchModel.Planid);
                }
                if (!string.IsNullOrEmpty(searchModel.Kpiid))
                {
                    query = query.Where(bu => bu.KpiId == searchModel.Kpiid);
                }

                // Pagination (uncomment if needed)
                // if (searchModel.page > 0 && searchModel.pageSize > 0)
                // {
                //     int skip = (searchModel.page - 1) * searchModel.pageSize;
                //     query = query.Skip(skip).Take(searchModel.pageSize);
                // }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<MKpiSystemKpiTarget>();
            }
        }
    }
}