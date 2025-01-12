using APP.Entities;
namespace APP.Interfaces.Repository;
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(Guid id);
    Task<User?> GetByIdComplete(Guid id);
    Task<User> GetByEmail(string email);
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task<User> Delete(Guid id);
}