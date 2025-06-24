using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanKpiListRepository
    {
        private readonly KPIDBContext _context;

        public MPlanKpiListRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanKpiList>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanKpiLists.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiList>();
            }
        }

        public async Task<MPlanKpiList?> GetByIdAsync(int? xplanid,int? xyear,string xname,string xplantypeid)
        {
            try
            {
                return await _context.MPlanKpiLists.FirstOrDefaultAsync(e=>e.Planid == xplanid
                && e.Planyear == xyear && e.Plantitle == xname
                && e.PlanTypeid == xplantypeid
                );
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpiList entity)
        {
            try
            {
                _context.MPlanKpiLists.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpiList entity)
        {
            try
            {
                _context.MPlanKpiLists.Update(entity);
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
                var entity = await _context.MPlanKpiLists.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanKpiLists.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanKpiList>> GetAllAsyncSearch_MPlanKpiList(searchMPlanKpiListModels searchModel)
        {
            try
            {
                var query = _context.MPlanKpiLists.AsQueryable();



                if (searchModel.Planyear != 0 && searchModel.Planyear != 0)
                {
                    query = query.Where(bu =>
                        bu.Planyear == searchModel.Planyear
                    );
                }
                if (searchModel.PlanTypeid !="" && searchModel.PlanTypeid != null)
                {
                    query = query.Where(bu =>
                            bu.PlanTypeid == searchModel.PlanTypeid);
                }
                if (searchModel.Name != "" && searchModel.Name != null)
                {
                    query = query.Where(bu =>
                            bu.Plantitle == searchModel.Name);
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
