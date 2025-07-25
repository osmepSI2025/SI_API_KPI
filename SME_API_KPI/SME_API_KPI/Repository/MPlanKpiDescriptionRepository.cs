using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanKpiDescriptionRepository
    {
        private readonly KPIDBContext _context;

        public MPlanKpiDescriptionRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanKpiDescription>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanKpiDescriptions.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanKpiDescription>();
            }
        }

        public async Task<MPlanKpiDescription?> GetByIdAsync(string id,string kpiid)
        {
            try
            {
                return await _context.MPlanKpiDescriptions.FirstOrDefaultAsync(e=>e.Planid == id && e.Kpiid ==kpiid);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanKpiDescription entity)
        {
            try
            {
                _context.MPlanKpiDescriptions.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanKpiDescription entity)
        {
            try
            {
                _context.MPlanKpiDescriptions.Update(entity);
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
                var entity = await _context.MPlanKpiDescriptions.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanKpiDescriptions.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanKpiDescription>> GetAllAsyncSearch_MPlanKpiDescription(searchMPlanKpiDescriptionModels searchModel)
        {
            try
            {
                var query = _context.MPlanKpiDescriptions.AsQueryable();



                if (searchModel.Planid != "" && searchModel.Planid != null)
                {
                    query = query.Where(bu =>
                        bu.Planid == searchModel.Planid
                    );
                }
                if (searchModel.Kpiid != "" && searchModel.Kpiid != null)
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
