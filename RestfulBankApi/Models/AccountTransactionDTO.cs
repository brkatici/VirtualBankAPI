namespace RestfulBankApi.Models
{
    public class AccountTransactionDTO
    {
            public decimal Amount { get; set; }
            public string TransactionType { get; set; }
            public DateTime TransactionDate { get; set; }


    }
}
