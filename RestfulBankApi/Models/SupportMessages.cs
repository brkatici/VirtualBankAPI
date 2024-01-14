namespace RestfulBankApi.Models
{
    public class SupportMessages
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; } 
        public DateTime MessageReceivedAt { get; set; }
        public string Category { get; set; }
        public bool Active { get; set; }
        
    }
}
