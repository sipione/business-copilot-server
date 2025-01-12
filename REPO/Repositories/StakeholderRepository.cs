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

    public async Task<IEnumerable<Stakeholder>> GetAll(Guid userId)
    {
        return await _context.Stakeholders.Where(s => s.UserId == userId).ToListAsync();
    }

    public async Task<Stakeholder?> GetById(Guid id, Guid userId){
        return await _context.Stakeholders.FirstOrDefaultAsync(stakeholder => stakeholder.Id == id && stakeholder.UserId == userId);
    }
    
    public async Task<Stakeholder?> GetByName(string name, Guid userId)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.Name == name && s.UserId == userId);
    }
    
    public async Task<Stakeholder> GetByUser(Guid userId)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.UserId == userId);
    }
    
    public async Task<Stakeholder?> GetByEmail(string email, Guid userId)
    {
        return await _context.Stakeholders.FirstOrDefaultAsync(s => s.Email == email && s.UserId == userId);
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

    public async Task<bool?> Delete(Guid id)
    {
        var stakeholder = await _context.Stakeholders.FindAsync(id);
        if (stakeholder == null)
        {
            return null;
        }

        _context.Stakeholders.Remove(stakeholder);
        await _context.SaveChangesAsync();
        return true;
    }
}