using APP.Entities;
namespace APP.Interfaces.Repository;
public interface ICashFlowRepository
{
    Task<IEnumerable<CashFlow>> GetAll();
    Task<CashFlow> GetById(Guid id);
    Task<IEnumerable<CashFlow>> GetByUser(Guid userId);
    Task<IEnumerable<CashFlow>> GetByDate(DateTime date);
    Task<IEnumerable<CashFlow>> GetByDateRange(DateTime startDate, DateTime endDate);
    Task<CashFlow> Create(CashFlow cashFlow);
    Task<CashFlow> Update(CashFlow cashFlow);
    Task<CashFlow> Delete(Guid id);
}