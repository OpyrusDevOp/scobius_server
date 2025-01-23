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
    public String SenderId { get; set; } = null!;
    [Required]
    public String MessageContent { get; set; } = null!;
    [ForeignKey(nameof(Media))]
    public Guid? jointMedia { get; set; }

    public Chat Chat;
    public User Sender;
    public Media Media;
}
