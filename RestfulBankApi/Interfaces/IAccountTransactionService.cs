using Microsoft.AspNetCore.Mvc;
using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IAccountTransactionService
    {
        Task<String> DepositAsync(string IBAN, decimal Amount , int userId);
        Task<String> WithDrawAsync(string IBAN, decimal Amount, int userId);
        Task<string> WithDrawAuditorControl(Account account, decimal Amount, bool isApproved);
        Task<AccountTransaction> CreateAccountTransactionAsync(Account account, decimal Amount, string transactionType);
        public class AccountNotFoundException : Exception
        {
            public AccountNotFoundException(string message) : base(message)
            {
            }
        }

        public class InsufficientFundsException : Exception
        {
            public InsufficientFundsException(string message) : base(message)
            {
            }
        }
    }
}
