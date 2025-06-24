using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanKpiAssignRepository
    {
        private readonly KPIDBContext _context;

        public MPlanKpiAssignRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanKpiAssign>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanKpiAssigns.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiAssign>();
            }
        }

        public async Task<MPlanKpiAssign?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MPlanKpiAssigns.FirstOrDefaultAsync(e=>e.Planid == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpiAssign entity)
        {
            try
            {
                _context.MPlanKpiAssigns.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpiAssign entity)
        {
            try
            {
                _context.MPlanKpiAssigns.Update(entity);
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
                var entity = await _context.MPlanKpiAssigns.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanKpiAssigns.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanKpiAssign>> GetAllAsyncSearch_MPlanKpiAssign(searchMPlanKpiAssignModels searchModel)
        {
            try
            {
                var query = _context.MPlanKpiAssigns.AsQueryable();



                if (searchModel.Planid != 0 && searchModel.Planid != 0)
                {
                    query = query.Where(bu =>
                        bu.Planid == searchModel.Planid
                    );
                }
                if (searchModel.Kpiid != 0 && searchModel.Kpiid != 0)
                {
                    query = query.Where(bu =>
                            bu.Kpiid == searchModel.Kpiid);
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
