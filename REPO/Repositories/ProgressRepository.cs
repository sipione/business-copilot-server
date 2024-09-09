using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;

public class ProgressRepository : IProgressRepository
{
    private readonly ApplicationDbContext _context;

    public ProgressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Progress>> GetAll()
    {
        return await _context.Progresses.ToListAsync();
    }

    public async Task<Progress> GetById(Guid id)
    {
        return await _context.Progresses.FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<IEnumerable<Progress>> GetByDate(DateTime date)
    {
        return await _context.Progresses.Where(p => p.CreatedAt.Date == date.Date).ToListAsync();
    }
    
    public async Task<IEnumerable<Progress>> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Progresses.Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate).ToListAsync();
    }

    public async Task<Progress> Create(Progress progress)
    {
        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync();
        return progress;
    }

    public async Task<Progress> Update(Progress progress)
    {
        _context.Progresses.Update(progress);
        await _context.SaveChangesAsync();
        return progress;
    }

    public async Task<Progress> Delete(Guid id)
    {
        var progress = await GetById(id);
        _context.Progresses.Remove(progress);
        await _context.SaveChangesAsync();
        return progress;
    }
}