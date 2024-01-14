using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;

namespace RestfulBankApi.Repositories
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly AppDbContext _context;

        public AccountTransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccountTransactionAsync(AccountTransaction transaction)
        {
            _context.AccountTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
        // Implement other methods as needed
    }

}
