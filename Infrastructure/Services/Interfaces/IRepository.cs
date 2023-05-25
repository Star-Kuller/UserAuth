using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.Interfaces;

public interface IRepository
{
    public User GetUser(long id);
    public User GetUser(string name);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(User user);
    public Task<User> CreateUser(string name, string passwordHash); 
    public Task<User> UpdateUser(User user, UserModificatedFields select, string field);
    public IEnumerable<Hobby> GetAllHobbies();
    public Hobby GetHobby(string name);
    public Task<Hobby> AddHobby(string name);
    public Task<Status> DeleteHobby(string name);
    
}