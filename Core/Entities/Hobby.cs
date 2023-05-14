using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Hobby
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string Name { get; set; }
    
    public required ICollection<User> Users { get; set; }
}