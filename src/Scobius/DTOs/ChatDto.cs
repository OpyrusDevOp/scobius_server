namespace Scobius.DTOs;

public record ChatDto
{
    public Guid Id { get; set; }
    public string InterlocutorId { get; set; } = null!;
    public string InterlocutorName { get; set; } = null!;
    public string? InterlocutorPicture { get; set; }
    public LastMessageDto? LastMessage { get; set; }
    public DateTime CreateAt { get; set; }
}

public record LastMessageDto
{
    public required string Message { get; set; }
    public DateTime LastMessageSendTime { get; set; }

}
