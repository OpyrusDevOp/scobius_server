using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scobius.Entities;

[PrimaryKey("Id")]
public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Chat))]
    public Guid ChatId { get; set; }
    [ForeignKey(nameof(Sender))]
    public string SenderId { get; set; } = null!;
    [Required]
    public string MessageContent { get; set; } = null!;
    [ForeignKey(nameof(RepliedTo))]
    public Guid? MessageRepliedTo { get; set; }
    [ForeignKey(nameof(Media))]
    public Guid? JointMedia { get; set; }
    public bool Seen { get; set; }
    public DateTime SendAt { get; set; }

    public virtual Chat Chat { get; set; }
    public virtual User Sender { get; set; }
    public virtual Media Media { get; set; }
    public virtual Message RepliedTo { get; set; }

}
