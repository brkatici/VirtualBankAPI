namespace RestfulBankApi.Models
{
    public class TransferTransaction
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; } // Kaynak hesap ID'si
        public int TargetAccountId { get; set; } // Hedef hesap ID'si
        public string SourceIBAN { get; set; } // Kaynak hesap ID'si
        public string TargetIBAN { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
