using Core.Entities;

namespace Infrastructure.Services.Interfaces;

public interface IAccounts
{
    public User Auth(string name, string password);
    public User Registration(string name, string password, string? surname, string? number);
}