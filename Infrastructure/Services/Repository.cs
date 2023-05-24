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

    public User? GetUser(long id)
    {
        return _myDbContext.Users.FirstOrDefault(u => u.Id == id);
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
        _myDbContext.SaveChanges();
    }

    public User CreateUser(string name, string passwordHash)
    {
        _myDbContext.Users.Add(new User {Name = name, PasswordHash = passwordHash});
        _myDbContext.SaveChanges();
        return _myDbContext.Users.FirstOrDefault(u => u.Name == name); 
    }

    public User UpdateUser(User user, UserModificatedFields select, string field)
    {
        var modUser = _myDbContext.Users.Find(user.Id);
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
                if(hobby is null)
                    return modUser;
                if (modUser.Hobbies is null)
                    modUser.Hobbies = new List<UserHobby>();
                //modUser.Hobbies.Add(hobby);
                break;
            case UserModificatedFields.HobbyRemove:
                var removeHobby = _myDbContext.Hobbies.FirstOrDefault(h => h.Name == field);
                if(removeHobby is null)
                    return modUser;
                if (modUser.Hobbies is null)
                    modUser.Hobbies = new List<UserHobby>();
                //modUser.Hobbies.Remove(removeHobby);
                break;
        }

        _myDbContext.Update(modUser);
        _myDbContext.SaveChanges();
        return modUser;
    }

    public IEnumerable<Hobby> GetAllHobbies()
    {
        return _myDbContext.Hobbies;
    }

    public Hobby GetHobby(string name)
    {
        return _myDbContext.Hobbies.FirstOrDefault(h => h.Name == name);
    }

    public Hobby AddHobby(string name)
    {
        //_myDbContext.Hobbies.Add(new Hobby {Name = name, Users = new List<User>()});
        _myDbContext.SaveChanges();
        return _myDbContext.Hobbies.FirstOrDefault(h => h.Name == name); 
    }

    public void DeleteHobby(string name)
    {
        var hobby = _myDbContext.Hobbies.FirstOrDefault(h => h.Name == name);
        if(hobby is null)
            return;
        _myDbContext.Hobbies.Remove(hobby);
        _myDbContext.SaveChanges();
    }
}