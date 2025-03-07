using APP.Entities;
using APP.Enums;
namespace APP.Interfaces.Repository;
public interface IContractRepository
{
    Task<IEnumerable<Contract>> GetAll(Guid ownerId);
    Task<Contract?> GetById(Guid id);
    Task<IEnumerable<Contract>> GetByStakeholder(Guid stakeholderId);
    Task<IEnumerable<Contract>> GetByUser(Guid userId);
    Task<IEnumerable<Contract>> GetByStatus(ContractStatus status);
    Task<IEnumerable<Contract>> GetByDate(DateTime date);
    Task<IEnumerable<Contract>> GetByDateRange(DateTime startDate, DateTime endDate);
    Task<Contract> Create(Contract contract);
    Task<Contract> Update(Contract contract);
    Task<Contract> Delete(Guid id);
}