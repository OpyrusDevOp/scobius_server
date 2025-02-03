using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scobius.Entities;


public class FriendshipRequest
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Sender))]
    public string SenderId { get; set; } = null!;
    [ForeignKey(nameof(Receiver))]
    public string ReceiverId { get; set; } = null!;
    public FriendshipRequestStatus Status { get; set; } = FriendshipRequestStatus.Pending;
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreateAt { get; set; }

    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}


