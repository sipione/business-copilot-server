using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _context;
    public ContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Contract>> GetAll(Guid ownerId)
    {
        return await _context.Contracts.Where(contract => contract.UserId == ownerId).ToListAsync();
    }
    public async Task<Contract> GetById(Guid id)
    {
        return await _context.Contracts.FindAsync(id);
    }
    public async Task<IEnumerable<Contract>> GetByStakeholder(Guid stakeholderId)
    {
        return await _context.Contracts.Where(c => c.StakeholderId == stakeholderId).ToListAsync();
    }
    public async Task<IEnumerable<Contract>> GetByUser(Guid userId)
    {
        return await _context.Contracts.Where(c => c.UserId == userId).ToListAsync();
    }
    public async Task<IEnumerable<Contract>> GetByStatus(ContractStatus status)
    {
        return await _context.Contracts.Where(c => c.ContractStatus == status).ToListAsync();
    }
    public async Task<IEnumerable<Contract>> GetByDate(DateTime date)
    {
        return await _context.Contracts.Where(c => c.StartDate == date).ToListAsync();
    }
    public async Task<IEnumerable<Contract>> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Contracts.Where(c => c.StartDate >= startDate && c.StartDate <= endDate).ToListAsync();
    }
    public async Task<Contract> Create(Contract contract)
    {
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
        return contract;
    }
    public async Task<Contract> Update(Contract contract)
    {
        _context.Contracts.Update(contract);
        await _context.SaveChangesAsync();
        return contract;
    }
    public async Task<Contract> Delete(Guid id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
            return null;
        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return contract;
    }
}