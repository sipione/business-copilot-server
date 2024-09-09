using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;
public class CashFlowRepository : ICashFlowRepository
{
    private readonly ApplicationDbContext _context;

    public CashFlowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CashFlow>> GetAll()
    {
        return await _context.CashFlows.ToListAsync();
    }
    
    public async Task<CashFlow> GetById(Guid id)
    {
        return await _context.CashFlows.FindAsync(id);
    }
    
    public async Task<IEnumerable<CashFlow>> GetByUser(Guid userId)
    {
        return await _context.CashFlows.Where(c => c.User.Id == userId).ToListAsync();
    }
    
    public async Task<IEnumerable<CashFlow>> GetByDate(DateTime date)
    {
        return await _context.CashFlows.Where(c => c.TransactionDate == date).ToListAsync();
    }
    
    public async Task<IEnumerable<CashFlow>> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.CashFlows.Where(c => c.TransactionDate >= startDate && c.TransactionDate <= endDate).ToListAsync();
    }
    
    public async Task<CashFlow> Create(CashFlow cashFlow)
    {
        _context.CashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    
    public async Task<CashFlow> Update(CashFlow cashFlow)
    {
        _context.CashFlows.Update(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    
    public async Task<CashFlow> Delete(Guid id)
    {
        var cashFlow = await _context.CashFlows.FindAsync(id);
        if (cashFlow == null)
        {
            return null;
        }
    
        _context.CashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
}