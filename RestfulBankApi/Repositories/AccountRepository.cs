using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using Microsoft.EntityFrameworkCore; // Ensure you've imported this namespace
using System.Linq;
using System.Threading.Tasks;
namespace RestfulBankApi.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountByUserIdAndIBANAsync(int userId, string IBAN)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserId == userId && a.IBAN == IBAN);
        }
        public async Task<Account> GetAccountByUserIBANAsync(string IBAN)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.IBAN == IBAN);
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }



        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetUserAccountsAsync(int userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
