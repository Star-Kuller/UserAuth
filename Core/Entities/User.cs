using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entites;

public class User
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public string? Number { get; set; }
    public string? PasswordHash { get; set; }
}