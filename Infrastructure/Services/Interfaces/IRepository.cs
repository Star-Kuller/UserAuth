using Core.Entities;

namespace Infrastructure.Services.Interfaces;

public interface IRepository
{
    public User GetUser(string name);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(User user);
    public Task<User> CreateUser(string name, string passwordHash); 
    public Task<User> UpdateUser(User user, UserModificatedFields select, string field);
}