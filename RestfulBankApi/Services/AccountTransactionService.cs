using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using RestfulBankApi.Repositories;
using static RestfulBankApi.Interfaces.IAccountTransactionService;

namespace RestfulBankApi.Services
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly AppDbContext _context;
        public AccountTransactionService(IAccountRepository accountRepository, AppDbContext context , IAccountTransactionRepository accountTransactionRepository)
        {
            _accountRepository = accountRepository;
            _context = context;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<String> DepositAsync(string IBAN, decimal Amount, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var account = await _accountRepository.GetAccountByUserIdAndIBANAsync(userId, IBAN); // Kullanıcının hesabını bul


                if (account == null)
                    return ("Account not found.");

                account.Balance += Amount;
                await _accountRepository.UpdateAccountAsync(account);

                var transactionLog = await CreateAccountTransactionAsync(account, Amount, "Deposit");
                await transaction.CommitAsync(); // Commit the transaction
                return ("Transaction successful");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback the transaction in case of an exception
                throw; // Re-throw the exception for handling at a higher level
            }
        }

        public async Task<string> WithDrawAsync(string IBAN, decimal Amount, int userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var account = await _accountRepository.GetAccountByUserIdAndIBANAsync(userId, IBAN);

                if (account == null)
                    throw new AccountNotFoundException("Account not found.");

                if (Amount > account.Balance)
                    throw new InsufficientFundsException("Insufficient funds.");

                var transactionLog = await CreateAccountTransactionAsync(account, Amount, "Withdrawal");

                switch (transactionLog.Status)
                {
                    case "Pending Approval":
                        await transaction.CommitAsync();
                        return "Pending Approval because your withdrawal amount is over 50k";
                    case "Daily Limit Exceeded":
                        await transaction.CommitAsync();
                        return "Daily Limit Exceeded";
                    default:
                        account.Balance -= Amount;
                        account.DailyWithdrawalLimit -= Amount;
                        await _accountRepository.UpdateAccountAsync(account);

                        await transaction.CommitAsync(); // Commit the transaction
                        return "Transaction successful";
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback the transaction in case of an exception
                throw; // Re-throw the exception for handling at a higher level
            }
        }



        public async Task<string> WithDrawAuditorControl(Account account,decimal Amount,bool isApproved)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (account == null)
                    throw new AccountNotFoundException("Account not found.");

                if (Amount > account.Balance)
                    throw new InsufficientFundsException("Insufficient funds.");

                if (isApproved)
                {
                    account.Balance -= Amount;
                    await _accountRepository.UpdateAccountAsync(account);
                    await transaction.CommitAsync(); // Commit the transaction
                    return "Transaction successful";
                }
                else
                {
                    return "Account transaction has been declined";
                }             
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback the transaction in case of an exception
                throw; // Re-throw the exception for handling at a higher level
            }
        }




        public async Task<AccountTransaction> CreateAccountTransactionAsync(Account account, decimal Amount, string transactionType)
        {
            try
            {

                var transaction = new AccountTransaction
                {
                    Amount = Amount,
                    TransactionDate = DateTime.Now,
                    AccountId = account.Id,
                    TransactionType = transactionType
                };

                if (transactionType == "Withdrawal" && Amount > 50000)
                {
                    transaction.Status = "Pending Approval";
                }
                else if (transactionType == "Withdrawal" && account.DailyWithdrawalLimit < Amount)
                {
                    transaction.Status = "Daily Limit Exceeded";
                }
                else
                {
                    transaction.Status = "Processed";
                }

                await _accountTransactionRepository.CreateAccountTransactionAsync(transaction); // Save transaction with status

                return transaction; // Return the created transaction log
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new Exception("An error occurred while processing the transaction.");
            }
        }


        //public async Task<AccountTransaction> CreateAccountTransactionAsync(Account account, decimal Amount, string transactionType)
        //{
        //    try
        //    {     
        //        if (account == null)
        //            throw new AccountNotFoundException("Account not found.");

        //        var transaction = new AccountTransaction
        //        {
        //            Amount = Amount,
        //            TransactionDate = DateTime.Now,
        //            AccountId = account.Id,
        //            TransactionType = transactionType
        //        };
        //        if (transactionType == "Withdrawal" && Amount > 50000)
        //        {
        //            transaction.Status = "Pending Approval";
        //        }else if(transactionType == "Withdrawal" && account.DailyWithdrawalLimit < Amount)
        //        {
        //            transaction.Status = "Daily Limit Exceeded";
        //        }
        //        else
        //        {
        //            transaction.Status = "Processed";
        //        }

        //        await _accountTransactionRepository.CreateAccountTransactionAsync(transaction);

        //        return transaction; // Return the created transaction log
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception
        //        throw new Exception("An error occurred while processing the transaction.");
        //    }
        //}
    }
}
