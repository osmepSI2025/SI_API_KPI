// SME_API_KPI/Repository/MPlanBudgetYearRepository.cs
// using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SME_API_KPI.Entities;
public class MPlanBudgetYearRepository 
{
    private readonly KPIDBContext _context;

    public MPlanBudgetYearRepository(KPIDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MPlanBudgetYear>> GetAllAsync()
    {
        try
        {
            return await _context.MPlanBudgetYears.ToListAsync();
        }
        catch (Exception)
        {
            return Enumerable.Empty<MPlanBudgetYear>();
        }
    }

    public async Task<MPlanBudgetYear?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.MPlanBudgetYears.FirstOrDefaultAsync(e => e.Year == id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddAsync(MPlanBudgetYear entity)
    {
        try
        {
            _context.MPlanBudgetYears.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(MPlanBudgetYear entity)
    {
        try
        {
            _context.MPlanBudgetYears.Update(entity);
            await _context.SaveChangesAsync();
            return true;
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
            var entity = await _context.MPlanBudgetYears.FindAsync(id);
            if (entity == null) return false;
            _context.MPlanBudgetYears.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
