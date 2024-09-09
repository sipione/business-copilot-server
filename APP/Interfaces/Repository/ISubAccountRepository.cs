using APP.Entities;
namespace APP.Interfaces.Repository;
public interface ISubAccountRepository
{
    Task<IEnumerable<SubAccounts>> GetAll();
    Task<SubAccounts> GetById(Guid id);
    Task<IEnumerable<SubAccounts>> GetByUser(Guid UserId);
    Task<SubAccounts> Create(SubAccounts subAccount);
    Task<SubAccounts> Update(SubAccounts subAccount);
    Task<SubAccounts> Delete(Guid id);
}