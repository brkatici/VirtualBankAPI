namespace RestfulBankApi.Models
{
    public class Credit
    {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string IBAN { get; set; }  
            public int LoanId { get; set; } 
            public bool Active { get; set; }

    }
}
