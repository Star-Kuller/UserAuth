using Core.Entities;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class Accounts : IAccounts
{
    private IRepository _repository;

    public Accounts(IRepository repository)
    {
        _repository = repository;
    }

    public User Auth(string name, string password)
    {
        var user = _repository.GetUser(name);
        if (user is null)
            return null;
        if (user.PasswordHash == password.ComputeSHA256())
            return user;
        return null;
    }

    public async Task<User> Registration(string name, string password, string? surname, string? number)
    {
        var user = _repository.GetUser(name);
        if (user is not null)
            return null;
        user = await _repository.CreateUser(name, password.ComputeSHA256());
        if(surname is not null) 
            user = await _repository.UpdateUser(user, UserModificatedFields.Surname, surname);
        if(number is not null) 
            user = await _repository.UpdateUser(user, UserModificatedFields.Number, number);
        return user;
    }
}