using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }
    public string? Number { get; set; }
    public required string PasswordHash { get; set; }
    
    public ICollection<Hobby> Hobbies { get; set; }
}