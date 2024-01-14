using RestfulBankApi.DTOs;
using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(AccountCreationModel model, int userId);
        Task<List<AccountDto>> GetUserAccountsAsync(string userId);
        Task ResetDailyLimits();
    }
}
