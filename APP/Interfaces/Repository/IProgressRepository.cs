using APP.Entities;
namespace APP.Interfaces.Repository;
public interface IProgressRepository
{
    Task<IEnumerable<Progress>> GetAll();
    Task<Progress> GetById(Guid id);
    Task<IEnumerable<Progress>> GetByDate(DateTime date);
    Task<IEnumerable<Progress>> GetByDateRange(DateTime startDate, DateTime endDate);
    Task<Progress> Create(Progress progress);
    Task<Progress> Update(Progress progress);
    Task<Progress> Delete(Guid id);
}