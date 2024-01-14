namespace RestfulBankApi.Models
{
    public class ApplicationUserLogs
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LogMessage { get; set; }
        public DateTime Date { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string UserIp { get; set; }
    }
}
