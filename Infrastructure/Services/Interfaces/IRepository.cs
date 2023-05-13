using Core.Entities;

namespace Infrastructure.Services.Interfaces;

public interface IRepository
{
    public User GetUser(long id);
    public User GetUser(string name);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(User user);
    public void CreateUser(string name, string passwordHash); 
    public void UpdateUser(User user, UserModificatedFields select, string field);
}