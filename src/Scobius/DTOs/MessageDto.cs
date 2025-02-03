namespace Scobius.DTOs;

public record SendMessageDto
{
    public Guid ChatId { get; set; }
    public Guid? RepliedTo { get; set; }
    public string Message { get; set; } = string.Empty;
    public JointMediaDto? JointMedia { get; set; }

}


public record MessageDto
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string SenderId { get; set; } = null!;
    public Guid? RepliedTo { get; set; }
    public string Message { get; set; } = string.Empty;
    public JointMediaDto? JointMedia { get; set; }
    public bool Seen { get; set; }
    public DateTime SendAt { get; set; }

}

public record JointMediaDto
{
    public string Content { get; set; } = null!;
    public TypeMedia ContentType { get; set; }
}
