using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanweightRepository
    {
        private readonly KPIDBContext _context;

        public MPlanweightRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanweight>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanweights.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanweight>();
            }
        }

        public async Task<MPlanweight?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.MPlanweights.FirstOrDefaultAsync(e=>e.Planid == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanweight entity)
        {
            try
            {
                _context.MPlanweights.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanweight entity)
        {
            try
            {
                _context.MPlanweights.Update(entity);
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
                var entity = await _context.MPlanweights.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanweights.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanweight>> GetAllAsyncSearch_MPlanweight(searchMPlanweightModels searchModel)
        {
            try
            {
                var query = _context.MPlanweights.AsQueryable();



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
