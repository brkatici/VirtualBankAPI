namespace RestfulBankApi.Models
{
    public class AccountCreationModel
    {
        public decimal Balance { get; set; }
        public string? AccountType { get; set; }
        public bool AutomaticPayments { get; set; }
        public decimal? InsuranceCoverage { get; set; }

    }
}
