namespace RestfulBankApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string IBAN { get; set; }    
        public decimal Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }

        // Yeni özellikler
        public string? AccountType { get; set; }
        public decimal? DailyWithdrawalLimit { get; set; }
        public decimal? DailyTransferLimit { get; set; }
        public bool AutomaticPayments { get; set; }
        public decimal? InsuranceCoverage { get; set; }
        public string? SpecialNotes { get; set; }

    }

}
