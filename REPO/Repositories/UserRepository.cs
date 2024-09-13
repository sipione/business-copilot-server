using APP.Entities;
using APP.Interfaces.Repository;
using REPO.Data;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context){
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAll(){
        IEnumerable<User> users = await _context.Users.ToListAsync();
        return users;
    }
    public async Task<User> GetById(Guid id){
        User user = await _context.Users.FindAsync(id);
        return user;
    }
    public async Task<User> GetByEmail(string email){
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<User> Create(User user){
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User> Update(User user){
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User> Delete(Guid id){
        var user = await _context.Users.FindAsync(id);
        if(user == null){
            throw new Exception("User not found");
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}