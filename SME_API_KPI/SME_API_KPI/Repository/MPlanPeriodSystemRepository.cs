using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanPeriodRepository
    {
        private readonly KPIDBContext _context;

        public MPlanPeriodRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanPeriod>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanPeriods.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanPeriod>();
            }
        }

        public async Task<MPlanPeriod?> GetByIdAsync(int xyear,string xplanId,string xuserId)
        {
            try
            {
                return await _context.MPlanPeriods.FirstOrDefaultAsync(e => e.PlanYear == xyear && e.Planid == xplanId && e.Userid == xuserId);
              //  return await _context.MPlanPeriods.FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanPeriod entity)
        {
            try
            {
                _context.MPlanPeriods.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanPeriod entity)
        {
            try
            {
                _context.MPlanPeriods.Update(entity);
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
                var entity = await _context.MPlanPeriods.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanPeriods.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanPeriod>> GetAllAsyncSearch_MPlanPeriod(searchMPlanPeriodModels searchModel)
        {
            try
            {
                var query = _context.MPlanPeriods.AsQueryable();



                if (!string.IsNullOrEmpty(searchModel.PlanTypeId))
                {
                    query = query.Where(bu =>
                        bu.PlanTypeId==searchModel.PlanTypeId
                    );
                }
                if (searchModel.PlanYear != 0 && searchModel.PlanYear != 0)
                {
                    query = query.Where(bu =>
                            bu.PlanYear == searchModel.PlanYear);
                }
                if (searchModel.PeriodId != 0 && searchModel.PeriodId != 0)
                {
                    query = query.Where(bu =>
                            bu.PeriodId == searchModel.PeriodId);
                }
                //// Apply pagination
                //if (searchModel.page != 0 && searchModel.pageSize != 0)
                //{
                //    int skip = (searchModel.page - 1) * searchModel.pageSize;
                //    query = query.Skip(skip).Take(searchModel.pageSize);
                //}


                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
