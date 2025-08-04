namespace server.DTOs.PromptHistory
{
    public class PromptHistoryCreateDto
    {
        public string PromptText { get; set; } = string.Empty;
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}