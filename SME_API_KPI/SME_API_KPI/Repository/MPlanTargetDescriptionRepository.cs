using SME_API_KPI.Entities;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MPlanTargetDescriptionRepository
    {
        private readonly KPIDBContext _context;

        public MPlanTargetDescriptionRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPlanTargetDescription>> GetAllAsync()
        {
            try
            {
                return await _context.MPlanTargetDescriptions.ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<MPlanTargetDescription>();
            }
        }

        public async Task<MPlanTargetDescription?> GetByIdAsync(int? id)
        {
            try
            {
                return await _context.MPlanTargetDescriptions.FirstOrDefaultAsync(e=>e.Planid == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MPlanTargetDescription entity)
        {
            try
            {
                _context.MPlanTargetDescriptions.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MPlanTargetDescription entity)
        {
            try
            {
                _context.MPlanTargetDescriptions.Update(entity);
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
                var entity = await _context.MPlanTargetDescriptions.FindAsync(id);
                if (entity == null) return false;
                _context.MPlanTargetDescriptions.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<MPlanTargetDescription>> GetAllAsyncSearch_MPlanTargetDescription(searchMPlanTargetDescriptionModels searchModel)
        {
            try
            {
                var query = _context.MPlanTargetDescriptions.AsQueryable();



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
