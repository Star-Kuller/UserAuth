using Core.Entities;

namespace Infrastructure.Services.Interfaces;

public interface IRepository
{
    public User GetUser(long id);
    public User GetUser(string name);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(User user);
    public User CreateUser(string name, string passwordHash); 
    public User UpdateUser(User user, UserModificatedFields select, string field);
    public IEnumerable<Hobby> GetAllHobbies();
    public Hobby GetHobby(string name);
    public Hobby AddHobby(string name);
    public int DeleteHobby(string name);
    
}