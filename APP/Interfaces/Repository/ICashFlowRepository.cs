using APP.Entities;
namespace APP.Interfaces.Repository;
public interface ICashFlowRepository
{
    Task<IEnumerable<IncomeCashFlow>> GetIncomeCashFlows();
    Task<IEnumerable<ExpenseCashFlow>> GetExpenseCashFlows();
    Task<IncomeCashFlow?> GetIncomeCashFlowById(Guid id);
    Task<ExpenseCashFlow?> GetExpenseCashFlowById(Guid id);
    Task<IncomeCashFlow> CreateIncomeCashFlow(IncomeCashFlow cashFlow);
    Task<ExpenseCashFlow> CreateExpenseCashFlow(ExpenseCashFlow cashFlow);
    Task<IncomeCashFlow> UpdateIncomeCashFlow(IncomeCashFlow cashFlow);
    Task<ExpenseCashFlow> UpdateExpenseCashFlow(ExpenseCashFlow cashFlow);
    Task<bool> DeleteIncomeCashFlow(Guid id);
    Task<bool> DeleteExpenseCashFlow(Guid id);
}