using Core.Entities;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class Repository : IRepository
{
    private readonly AppDbContext _myDbContext;

    public Repository(AppDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public User? GetUser(string name)
    {
        return _myDbContext.Users.FirstOrDefault(u => u.Name == name);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _myDbContext.Users;
    }

    public void DeleteUser(User user)
    {
        _myDbContext.Users.Remove(user);
    }

    public async Task<User> CreateUser(string name, string passwordHash)
    {
        _myDbContext.Users.Add(new User {Name = name, PasswordHash = passwordHash});
        await _myDbContext.SaveChangesAsync();
        return _myDbContext.Users.FirstOrDefault(u => u.Name == name); 
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
                var hobby = _myDbContext.Hobbies.FirstOrDefault(h => h.Name == field);
                modUser.Hobbies.Add(hobby);
                break;
            case UserModificatedFields.HobbyRemove:
                var removeHobby = _myDbContext.Hobbies.FirstOrDefault(h => h.Name == field);
                modUser.Hobbies.Remove(removeHobby);
                break;
        }

        _myDbContext.Update(modUser);
        await _myDbContext.SaveChangesAsync();
        return modUser;
    }
}