using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanResultRepository
    {
        private readonly KPIDBContext _context;

        public MPlanResultRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanResult>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanResults.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanResult>();
            }
        }

        public async Task<MPlanResult?> GetByIdAsync(int xplanid,int? xkpiid,string xperiodid,string xassignid,int? xpoint,string xresult)
        {
            try
            {
                return await _context.MPlanResults.FirstOrDefaultAsync(
                    e=>e.Planid == xplanid
                && e.Kpiid ==xkpiid
                && e.Periodid == xperiodid
                && e.Assignid == xassignid
                && e.Point == xpoint
                && e.Result == xresult

                );
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanResult entity)
        {
            try
            {
                _context.MPlanResults.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanResult entity)
        {
            try
            {
                _context.MPlanResults.Update(entity);
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
                var entity = await _context.MPlanResults.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanResults.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanResult>> GetAllAsyncSearch_MPlanResult(searchMPlanResultModels searchModel)
        {
            try
            {
                var query = _context.MPlanResults.AsQueryable();



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
