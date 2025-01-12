using APP.Entities;
namespace APP.Interfaces.Repository;
public interface IStakeholderRepository
{
    Task<IEnumerable<Stakeholder>> GetAll(Guid userId);
    Task<Stakeholder> GetById(Guid id, Guid userId);
    Task<Stakeholder> GetByName(string name, Guid userId);
    Task<Stakeholder> GetByUser(Guid userId);
    Task<Stakeholder> GetByEmail(string email, Guid userId);
    Task<Stakeholder> Create(Stakeholder stakeholder);
    Task<Stakeholder> Update(Stakeholder stakeholder);
    Task<bool?> Delete(Guid id);
}