namespace server.DTOs.PromptHistory
{
    public class PromptHistoryReadDto
    {
        public int Id { get; set; }
        public string PromptText { get; set; } = string.Empty;
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}