using APP.Entities;
namespace APP.Interfaces.Repository;
public interface ICashFlowRepository
{
    Task<IEnumerable<IncomeCashFlow>> GetIncomeCashFlows(Guid userId);
    Task<IEnumerable<ExpenseCashFlow>> GetExpenseCashFlows(Guid userId);
    Task<IncomeCashFlow?> GetIncomeCashFlowById(Guid id, Guid userId);
    Task<ExpenseCashFlow?> GetExpenseCashFlowById(Guid id, Guid userId);
    Task<IncomeCashFlow> CreateIncomeCashFlow(IncomeCashFlow cashFlow);
    Task<ExpenseCashFlow> CreateExpenseCashFlow(ExpenseCashFlow cashFlow);
    Task<IncomeCashFlow> UpdateIncomeCashFlow(IncomeCashFlow cashFlow);
    Task<ExpenseCashFlow> UpdateExpenseCashFlow(ExpenseCashFlow cashFlow);
    Task<bool> DeleteIncomeCashFlow(Guid id);
    Task<bool> DeleteExpenseCashFlow(Guid id);
}