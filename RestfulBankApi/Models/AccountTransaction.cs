namespace RestfulBankApi.Models
{
    public class AccountTransaction
    {

        public int Id { get; set; }
        public int AccountId { get; set; } // Kaynak hesap ID'si
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        // Diğer işlem detayları eklenebilir                  

    }
}
