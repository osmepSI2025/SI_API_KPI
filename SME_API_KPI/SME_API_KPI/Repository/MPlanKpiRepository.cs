using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanKpiRepository
    {
        private readonly KPIDBContext _context;

        public MPlanKpiRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanKpi>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanKpis.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpi>();
            }
        }

        public async Task<MPlanKpi?> GetByIdAsync(int? xplanId,int? xkpiid)
        {
            try
            {
                return await _context.MPlanKpis.FirstOrDefaultAsync(e=>e.PlanId == xplanId && e.Kpiid == xkpiid);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpi entity)
        {
            try
            {
                _context.MPlanKpis.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpi entity)
        {
            try
            {
                _context.MPlanKpis.Update(entity);
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
                var entity = await _context.MPlanKpis.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanKpis.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanKpi>> GetAllAsyncSearch_MPlanKpi(searchMPlanKpiModels searchModel)
        {
            try
            {
                var query = _context.MPlanKpis
                    .Include(x => x.TPlanKpilists)
                    //.ThenInclude(y => y.TPlanKpidivisions)
                    .AsQueryable();

                if (searchModel.Planid != 0)
                {
                    query = query.Where(bu => bu.PlanId == searchModel.Planid);
                }
                // Add more filters if needed, e.g. for dimensionid

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<MPlanKpi>();
            }
        }


       

    }
}
