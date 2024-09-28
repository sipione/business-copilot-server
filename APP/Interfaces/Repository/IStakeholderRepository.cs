using APP.Entities;
namespace APP.Interfaces.Repository;
public interface IStakeholderRepository
{
    Task<IEnumerable<Stakeholder>> GetAll();
    Task<Stakeholder> GetById(Guid id);
    Task<Stakeholder> GetByName(string name);
    Task<Stakeholder> GetByUser(Guid userId);
    Task<Stakeholder> GetByEmail(string email);
    Task<Stakeholder> Create(Stakeholder stakeholder);
    Task<Stakeholder> Update(Stakeholder stakeholder);
    Task<bool?> Delete(Guid id);
}