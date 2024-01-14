namespace RestfulBankApi.Interfaces
{
    public interface ITransferService
    {
        Task<bool> IsAccountOwnedByUserAsync(string iban, int userId);
        Task<string> TransferBetweenUserAccountsAsync(string senderIBAN, string receiverIBAN, decimal amount, int senderUserId);
        Task<string> TransferToAnotherUserAsync(string senderIBAN, string receiverIBAN, decimal amount, int senderUserId);
    }
}
