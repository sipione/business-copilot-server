using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;
public class CashFlowRepository : ICashFlowRepository{
    private readonly ApplicationDbContext _context;
    public CashFlowRepository(ApplicationDbContext context){
        _context = context;
    }
    public async Task<IEnumerable<IncomeCashFlow>> GetIncomeCashFlows(Guid userId)
    {
        return await _context.IncomeCashFlows.Where(cf => cf.UserId == userId).ToListAsync();
    }
    public async Task<IEnumerable<ExpenseCashFlow>> GetExpenseCashFlows(Guid userId)
    {
        return await _context.ExpenseCashFlows.Where(cf => cf.UserId == userId).ToListAsync();
    }
    public async Task<IncomeCashFlow?> GetIncomeCashFlowById(Guid id, Guid userId)
    {
        return await _context.IncomeCashFlows.FirstOrDefaultAsync(cf => cf.Id == id && cf.UserId == userId);
    }
    public async Task<ExpenseCashFlow?> GetExpenseCashFlowById(Guid id, Guid userId)
    {
        return await _context.ExpenseCashFlows.FirstOrDefaultAsync(cf => cf.Id == id && cf.UserId == userId);
    }
    public async Task<IncomeCashFlow> CreateIncomeCashFlow(IncomeCashFlow cashFlow){
        _context.IncomeCashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    public async Task<ExpenseCashFlow> CreateExpenseCashFlow(ExpenseCashFlow cashFlow){
        _context.ExpenseCashFlows.Add(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    public async Task<IncomeCashFlow> UpdateIncomeCashFlow(IncomeCashFlow cashFlow){
        _context.IncomeCashFlows.Update(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    public async Task<ExpenseCashFlow> UpdateExpenseCashFlow(ExpenseCashFlow cashFlow){
        _context.ExpenseCashFlows.Update(cashFlow);
        await _context.SaveChangesAsync();
        return cashFlow;
    }
    public async Task<bool> DeleteIncomeCashFlow(Guid id){
        var cashFlow = await _context.IncomeCashFlows.FindAsync(id);
        if(cashFlow == null){
            return false;
        }
        _context.IncomeCashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteExpenseCashFlow(Guid id){
        var cashFlow = await _context.ExpenseCashFlows.FindAsync(id);
        if(cashFlow == null){
            return false;
        }
        _context.ExpenseCashFlows.Remove(cashFlow);
        await _context.SaveChangesAsync();
        return true;
    }
}