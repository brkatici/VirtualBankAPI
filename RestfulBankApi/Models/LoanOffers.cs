namespace RestfulBankApi.Models
{
    public class LoanOffers
    {
        public int Id { get; set; }
        public string OfferCode { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTermMonths { get; set; }
        public bool Active { get; set; }
    }
}
