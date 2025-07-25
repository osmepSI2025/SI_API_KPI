using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MKpiSystemAssignRepository
    {
        private readonly KPIDBContext _context;

        public MKpiSystemAssignRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MKpiSystemAssign>> GetAllAsync()
        {
            try
            {
                return await _context.MKpiSystemAssigns
                    .Include(x => x.TKpiSystemAssignDivisions)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<MKpiSystemAssign>();
            }
        }

        public async Task<MKpiSystemAssign?> GetByIdAsync(string kpiid,string planid)
        {
            try
            {
                return await _context.MKpiSystemAssigns
                    .Include(x => x.TKpiSystemAssignDivisions)
                    .FirstOrDefaultAsync(x => x.KpiId == kpiid && x.PlanId == planid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MKpiSystemAssign entity)
        {
            try
            {
                _context.MKpiSystemAssigns.Add(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MKpiSystemAssign entity)
        {
            try
            {
                _context.MKpiSystemAssigns.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.MKpiSystemAssigns.FindAsync(id);
                if (entity == null) return false;
                _context.MKpiSystemAssigns.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<MKpiSystemAssign>> GetAllAsyncSearch_MPlanKpiAssign(searchMPlanKpiAssignModels searchModel)
        {
            try
            {
                var query = _context.MKpiSystemAssigns
                     .Include(x => x.TKpiSystemAssignDivisions).AsQueryable();



                if (searchModel.planid != "" && searchModel.planid != null)
                {
                    query = query.Where(bu =>
                        bu.PlanId == searchModel.planid
                    );
                }
                if (searchModel.kpiid != "" && searchModel.kpiid != null)
                {
                    query = query.Where(bu =>
                            bu.KpiId == searchModel.kpiid);
                }

           


                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}