using APP.Entities;
namespace APP.Interfaces.Repository;
public interface IVoucherRepository
{
    Task<IEnumerable<Voucher>> GetAll();
    Task<Voucher> GetById(Guid id);
    Task<IEnumerable<Voucher>> GetByUser(Guid userId);
    Task<IEnumerable<Voucher>> GetByDateRange(DateTime startDate, DateTime endDate);
    Task<Voucher> Create(Voucher voucher);
    Task<Voucher> Update(Voucher voucher);
    Task<Voucher> Delete(Guid id);
}