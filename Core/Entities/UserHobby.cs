using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class UserHobby
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public long HobbyId { get; set; }
    public Hobby Hobby { get; set; } = null!;
}