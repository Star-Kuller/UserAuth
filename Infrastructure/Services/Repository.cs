using Core.Entities;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class Repository : IRepository
{
    private readonly AppDbContext _myDbContext;

    public Repository(AppDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public User? GetUser(long id)
    {
        return _myDbContext.Users.Include(u => u.Hobbies).FirstOrDefault(u => u.Id == id);
    }
    
    public User? GetUser(string name)
    {
        return _myDbContext.Users.Include(u => u.Hobbies).FirstOrDefault(u => u.Name == name);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _myDbContext.Users.Include(u => u.Hobbies);
    }

    public async void DeleteUser(User user)
    {
        _myDbContext.Users.Remove(user);
        await _myDbContext.SaveChangesAsync();
    }

    public async Task<User> CreateUser(string name, string passwordHash)
    {
        _myDbContext.Users.Add(new User {Name = name, PasswordHash = passwordHash});
        _myDbContext.SaveChanges();
        return _myDbContext.Users.Include(u => u.Hobbies).FirstOrDefault(u => u.Name == name); 
    }

    public async Task<User> UpdateUser(User user, UserModificatedFields select, string field)
    {
        var modUser = await _myDbContext.Users.FindAsync(user.Id);
        if (modUser is null)
            return modUser;
        switch (select)
        {
            case UserModificatedFields.Name:
                modUser.Name = field;
                break;
            case UserModificatedFields.Surname:
                modUser.Surname = field;
                break;
            case UserModificatedFields.Number:
                modUser.Number = field;
                break;
            case UserModificatedFields.HobbyAdd:
                var addHobby = _myDbContext.Hobbies.Include(h => h.Users).FirstOrDefault(h => h.Name == field);
                if(addHobby is null)
                    return modUser;
                if (modUser.Hobbies is null)
                    modUser.Hobbies = new List<Hobby>();
                modUser.Hobbies.Add(addHobby);
                break;
            case UserModificatedFields.HobbyRemove:
                var removeHobby = _myDbContext.Hobbies.Include(h => h.Users).FirstOrDefault(h => h.Name == field);
                if(removeHobby is null)
                    return modUser;
                if (modUser.Hobbies is null)
                    modUser.Hobbies = new List<Hobby>();
                modUser.Hobbies.Remove(removeHobby);
                break;
        }

        _myDbContext.Update(modUser);
        await _myDbContext.SaveChangesAsync();
        return modUser;
    }

    public IEnumerable<Hobby> GetAllHobbies()
    {
        return _myDbContext.Hobbies.Include(h => h.Users);
    }

    public Hobby GetHobby(string name)
    {
        return _myDbContext.Hobbies.Include(h => h.Users).FirstOrDefault(h => h.Name == name);
    }

    public async Task<Hobby> AddHobby(string name)
    {
        if (_myDbContext.Hobbies.Contains(new Hobby { Name = name }))
            return _myDbContext.Hobbies.FirstOrDefault(h => h.Name == name);
        var newHobby = new Hobby { Name = name };
        _myDbContext.Hobbies.Add(newHobby);
        await _myDbContext.SaveChangesAsync();
        return newHobby; 
    }

    public async Task<Status> DeleteHobby(string name)
    {
        var hobby = _myDbContext.Hobbies.Include(h => h.Users).FirstOrDefault(h => h.Name == name);
        if(hobby is null)
            return Status.NotFound;
        _myDbContext.Hobbies.Remove(hobby);
        await _myDbContext.SaveChangesAsync();
        return Status.Success;
    }
}