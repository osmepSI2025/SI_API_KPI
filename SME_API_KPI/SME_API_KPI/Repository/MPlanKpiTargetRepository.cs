using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanKpiTargetRepository
    {
        private readonly KPIDBContext _context;

        public MPlanKpiTargetRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanKpiTarget>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanKpiTargets
                    .Include(x => x.TPlanTargetDetails) // Eagerly load TKpiTargets
                    .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiTarget>();
            }
        }

        public async Task<MPlanKpiTarget?> GetByIdAsync(string? xplanId,string? xkpiid)
        {
            try
            {
                return await _context.MPlanKpiTargets.FirstOrDefaultAsync(e=>e.PlanId == xplanId && e.KpiId == xkpiid);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpiTarget entity)
        {
            try
            {
                _context.MPlanKpiTargets.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpiTarget entity)
        {
            try
            {
                _context.MPlanKpiTargets.Update(entity);
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
                var entity = await _context.MPlanKpiTargets.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanKpiTargets.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanKpiTarget>> GetAllAsyncSearch_MPlanKpiTarget(searchMPlanKpiTargetModels searchModel)
        {
            try
            {
                var query = _context.MPlanKpiTargets
                    .Include(x => x.TPlanTargetDetails) // Eagerly load TKpiTargets
                    .AsQueryable();

                if (searchModel.Planid != null && searchModel.Kpiid != "")
                {
                    query = query.Where(bu => bu.PlanId == searchModel.Planid);
                }
                if (searchModel.Kpiid != null && searchModel.Kpiid != "")
                {
                    query = query.Where(bu => bu.KpiId == searchModel.Kpiid);
                }

                // Pagination (uncomment if needed)
                // if (searchModel.page != 0 && searchModel.pageSize != 0)
                // {
                //     int skip = (searchModel.page - 1) * searchModel.pageSize;
                //     query = query.Skip(skip).Take(searchModel.pageSize);
                // }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
