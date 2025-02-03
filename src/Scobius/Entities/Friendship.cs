using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scobius.Entities;

[PrimaryKey("UserAId", "UserBId")]
public class Friendship
{
    [ForeignKey(nameof(UserA))]
    public string UserAId { get; set; }
    [ForeignKey(nameof(UserB))]
    public string UserBId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreateAt { get; set; }

    public virtual User UserA { get; set; }
    public virtual User UserB { get; set; }
}
