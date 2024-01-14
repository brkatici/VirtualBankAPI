using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;

namespace RestfulBankApi.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IAccountRepository _accountRepository;

        public TransferService(ITransferRepository transferRepository, IAccountRepository accountRepository)
        {
            _transferRepository = transferRepository;
            _accountRepository = accountRepository;
        }
        public async Task<bool> IsAccountOwnedByUserAsync(string iban, int userId)
        {
            var account = await _accountRepository.GetAccountByUserIBANAsync(iban);

            // Hesap bulunamadıysa veya hesabın sahibi belirtilen kullanıcı değilse false döndür
            return account != null && account.UserId == userId;
        }
        public async Task<string> TransferBetweenUserAccountsAsync(string senderIBAN, string receiverIBAN, decimal amount, int senderUserId)
        {
            var senderAccount = await _accountRepository.GetAccountByUserIBANAsync(senderIBAN);
            var receiverAccount = await _accountRepository.GetAccountByUserIBANAsync(receiverIBAN);

            if (senderAccount == null || receiverAccount == null)
            {
                return "One or both accounts not found";
            }
            // Kullanıcının gönderen hesabın sahibi olduğunu kontrol etme
            if (senderAccount.UserId != senderUserId)
            {
                return "Unauthorized transaction"; // Gönderen hesap, kullanıcının hesaplarından biri değil
            }
            if (senderAccount.DailyTransferLimit < amount || receiverAccount.DailyTransferLimit < amount)
            {
                return "Daily transfer limit exceeded"; // Gönderen veya alıcı hesabın günlük transfer limiti aşıldı
            }

            if (senderAccount.Balance < amount)
            {
                return "Insufficient balance";
            }
            bool isReceiverAccountOwnedByUser = await IsAccountOwnedByUserAsync(receiverIBAN, senderUserId);
            if (!isReceiverAccountOwnedByUser)
            {
                return "You cannot transfer to this account.You don`t have any account using this IBAN"; // Alıcı hesap, kullanıcının hesaplarından biri değil
            }

            if (senderAccount.Balance < amount)
            {
                return "Insufficient balance";
            }

            using (var transaction = _transferRepository.BeginTransaction())
            {
                try
                {
                    var transfer = new TransferTransaction
                    {
                        SourceIBAN = senderIBAN,
                        TargetIBAN = receiverIBAN,
                        TransactionType = "HAVALE",
                        Amount = amount,
                        TransactionDate = DateTime.Now,

                    };

                    await _transferRepository.AddTransferAsync(transfer);

                    senderAccount.Balance -= amount;
                    receiverAccount.Balance += amount;

                    await _accountRepository.UpdateAccountAsync(senderAccount);
                    await _accountRepository.UpdateAccountAsync(receiverAccount);

                    await transaction.CommitAsync();
                    return "Transfer Successful";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return "An error occurred while processing the request.";
                }
            }
        }


        public async Task<string> TransferToAnotherUserAsync(string senderIBAN, string receiverIBAN, decimal amount, int senderUserId)
        {
            var senderAccount = await _accountRepository.GetAccountByUserIBANAsync(senderIBAN);
            var receiverAccount = await _accountRepository.GetAccountByUserIBANAsync(receiverIBAN);

            if (senderAccount == null || receiverAccount == null)
            {
                return "One or both accounts not found";
            }
            if (senderAccount.UserId != senderUserId)
            {
                return "Unauthorized transaction"; // Gönderen hesap, kullanıcının hesaplarından biri değil
            }
            if (senderAccount.DailyTransferLimit < amount || receiverAccount.DailyTransferLimit < amount)
            {
                return "Daily transfer limit exceeded"; // Gönderen veya alıcı hesabın günlük transfer limiti aşıldı
            }

            if (senderAccount.Balance < amount)
            {
                return "Insufficient balance";
            }

            using (var transaction = _transferRepository.BeginTransaction())
            {
                try
                {
                    var transfer = new TransferTransaction
                    {
                        SourceIBAN = senderIBAN,
                        TargetIBAN = receiverIBAN,
                        TransactionType = "HAVALE",
                        Amount = amount,
                        TransactionDate = DateTime.Now,
                        
                    };

                    await _transferRepository.AddTransferAsync(transfer);

                    senderAccount.Balance -= amount;
                    receiverAccount.Balance += amount;

                    await _accountRepository.UpdateAccountAsync(senderAccount);
                    await _accountRepository.UpdateAccountAsync(receiverAccount);

                    await transaction.CommitAsync();
                    return "Transfer Successful";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return "An error occurred while processing the request.";
                }
            }
        }
    }
}
