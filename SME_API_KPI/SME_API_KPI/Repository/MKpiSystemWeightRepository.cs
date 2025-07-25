using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
using SME_API_KPI.Models;

namespace SME_API_KPI.Repository
{
    public class MKpiSystemWeightRepository
    {
        private readonly KPIDBContext _context;

        public MKpiSystemWeightRepository(KPIDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MKpiSystemWeight>> GetAllAsync()
        {
            try
            {
                return await _context.MKpiSystemWeights
                    .Include(x => x.TKpiSystemWeights)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return Enumerable.Empty<MKpiSystemWeight>();
            }
        }

        public async Task<MKpiSystemWeight?> GetByIdAsync(string  planid,string kpiid)
        {
            try
            {
                return await _context.MKpiSystemWeights
                    .Include(x => x.TKpiSystemWeights)
                    .FirstOrDefaultAsync(x => x.Planid == planid && x.KpiId == kpiid);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddAsync(MKpiSystemWeight entity)
        {
            try
            {
                _context.MKpiSystemWeights.Add(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MKpiSystemWeight entity)
        {
            try
            {
                _context.MKpiSystemWeights.Update(entity);
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
                var entity = await _context.MKpiSystemWeights.FindAsync(id);
                if (entity == null) return false;
                _context.MKpiSystemWeights.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<MKpiSystemWeight>> GetAllAsyncSearch_MPlanweight(searchMPlanweightModels searchModel)
        {
            try
            {
                var query = _context.MKpiSystemWeights
                    .Include(x => x.TKpiSystemWeights)
                    .AsQueryable();



                if (searchModel.Planid != "" && searchModel.Planid != null)
                {
                    query = query.Where(bu =>
                        bu.Planid == searchModel.Planid
                    );
                }
                if (searchModel.Kpiid != "" && searchModel.Kpiid != null)
                {
                    query = query.Where(bu =>
                            bu.KpiId == searchModel.Kpiid);
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