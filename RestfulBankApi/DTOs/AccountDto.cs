namespace RestfulBankApi.DTOs
{
    public class AccountDto
    {
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreationDate { get; set; }
        public string? AccountType { get; set; }
        public decimal? DailyWithdrawalLimit { get; set; }
        public decimal? DailyTransferLimit { get; set; }
        public bool AutomaticPayments { get; set; }
        public decimal? InsuranceCoverage { get; set; }
    }
}
