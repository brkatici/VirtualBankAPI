using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IAccountTransactionRepository
    {
        Task CreateAccountTransactionAsync(AccountTransaction transaction);
    }
}
