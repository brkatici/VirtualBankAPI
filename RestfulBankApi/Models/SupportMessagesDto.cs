namespace RestfulBankApi.Models
{
    public class SupportMessagesDto
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime MessageReceivedAt { get; set; }
        public string Category { get; set; }
    }
}
