using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scobius.Entities;

public class Frienship
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [ForeignKey(nameof(UserA))]
    public string UserAId { get; set; }
    [ForeignKey(nameof(UserB))]
    public string UserBId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreateAt { get; set; }

    public User UserA;
    public User UserB;
}
