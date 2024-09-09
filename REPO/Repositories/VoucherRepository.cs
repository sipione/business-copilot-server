using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;
public class VoucherRepository : IVoucherRepository
{
    private readonly ApplicationDbContext _context;

    public VoucherRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Voucher>> GetAll()
    {
        return await _context.Vouchers.ToListAsync();
    }
    
    public async Task<Voucher> GetById(Guid id)
    {
        return await _context.Vouchers.FindAsync(id);
    }
    
    public async Task<IEnumerable<Voucher>> GetByUser(Guid userId)
    {
        return await _context.Vouchers.Where(v => v.User.Id == userId).ToListAsync();
    }
    
    public async Task<IEnumerable<Voucher>> GetByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Vouchers.Where(v => v.CreatedAt >= startDate && v.CreatedAt <= endDate).ToListAsync();
    }
    
    public async Task<Voucher> Create(Voucher voucher)
    {
        _context.Vouchers.Add(voucher);
        await _context.SaveChangesAsync();
        return voucher;
    }
    
    public async Task<Voucher> Update(Voucher voucher)
    {
        _context.Vouchers.Update(voucher);
        await _context.SaveChangesAsync();
        return voucher;
    }
    
    public async Task<Voucher> Delete(Guid id)
    {
        var voucher = await _context.Vouchers.FindAsync(id);
        if (voucher == null)
        {
            return null;
        }
    
        _context.Vouchers.Remove(voucher);
        await _context.SaveChangesAsync();
        return voucher;
    }
}