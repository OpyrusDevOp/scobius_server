using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scobius.Entities;

public class Chat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [ForeignKey(nameof(UserA))]
    public string UserAId { get; set; } = null!;
    [ForeignKey(nameof(UserB))]
    public string UserBId { get; set; } = null!;
    public DateTime CreateAt { get; set; }

    public User UserA { get; set; }
    public User UserB { get; set; }
}
