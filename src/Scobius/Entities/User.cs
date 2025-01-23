using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scobius.Entities;

public class User
{
    [Key]
    [Required]
    public string Id { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreateAt { get; set; }
    public DateTime LastSeen { get; set; }
}
