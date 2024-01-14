using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task<List<Account>> GetUserAccountsAsync(int userId);
        Task<Account> GetAccountByUserIdAndIBANAsync(int userId, string IBAN);
        Task<Account> GetAccountByUserIBANAsync(string IBAN);
        Task<Account> UpdateAccountAsync(Account account);

        
    }
}
