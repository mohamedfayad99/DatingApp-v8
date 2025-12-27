namespace API.Dtos;
public class MessageDto
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string RecipientName { get; set; }
    public string? Content { get; set; }
    public DateTime DateSent { get; set; }
    public DateTime? DateRead { get; set; }
}

