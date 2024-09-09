using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;

public class StakeholderRepository : IStakeholderRepository
{
    private readonly ApplicationDbContext _context;

    public StakeholderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Stakeholder>> GetAll()
    {
        return await _context.Stakeholders.ToListAsync();
    }

    public async Task<Stakeholder> GetById(Guid id)
    {
        return await _context.Stakeholders.FindAsync(id);
    }
    
    public async Task<Stakeholder> GetByName(string name)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.Name == name);
    }
    
    public async Task<Stakeholder> GetByUser(Guid userId)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.User.Id == userId);
    }
    
    public async Task<Stakeholder> GetByEmail(string email)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<Stakeholder> Create(Stakeholder stakeholder)
    {
        _context.Stakeholders.Add(stakeholder);
        await _context.SaveChangesAsync();
        return stakeholder;
    }

    public async Task<Stakeholder> Update(Stakeholder stakeholder)
    {
        _context.Entry(stakeholder).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return stakeholder;
    }

    public async Task<Stakeholder> Delete(Guid id)
    {
        var stakeholder = await _context.Stakeholders.FindAsync(id);
        if (stakeholder == null)
        {
            return null;
        }

        _context.Stakeholders.Remove(stakeholder);
        await _context.SaveChangesAsync();
        return stakeholder;
    }
}