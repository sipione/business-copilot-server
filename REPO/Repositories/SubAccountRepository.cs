using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;

public class SubAccountRepository : ISubAccountRepository
{
    private readonly ApplicationDbContext _context;

    public SubAccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SubAccounts>> GetAll()
    {
        return await _context.SubAccounts.ToListAsync();
    }
    
    public async Task<SubAccounts> GetById(Guid id)
    {
        return await _context.SubAccounts.FindAsync(id);
    }

    public async Task<SubAccounts> GetByEmail(string email)
    {
        return await _context.SubAccounts.FirstOrDefaultAsync(s => s.Email == email);
    }
    
    public async Task<IEnumerable<SubAccounts>> GetByUser(Guid userId)
    {
        return await _context.SubAccounts.Where(s => s.UserId == userId).ToListAsync();
    }
    
    public async Task<SubAccounts> Create(SubAccounts subAccount)
    {
        _context.SubAccounts.Add(subAccount);
        await _context.SaveChangesAsync();
        return subAccount;
    }
    
    public async Task<SubAccounts> Update(SubAccounts subAccount)
    {
        _context.Entry(subAccount).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return subAccount;
    }
    
    public async Task<SubAccounts> Delete(Guid id)
    {
        var subAccount = await _context.SubAccounts.FindAsync(id);
        if (subAccount == null)
        {
            return null;
        }
    
        _context.SubAccounts.Remove(subAccount);
        await _context.SaveChangesAsync();
        return subAccount;
    }
}