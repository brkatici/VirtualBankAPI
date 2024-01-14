using Microsoft.VisualBasic;
using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using RestfulBankApi.Repositories;
using RestfulBankApi.DTOs;
namespace RestfulBankApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly DailyLimits _dailyLimits;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly RandomIBANSeeder _randomIBANSeeder;

        public AccountService(AppDbContext context,DailyLimits dailyLimits, IAccountRepository accountRepository, IMapper mapper, IConfiguration configuration, RandomIBANSeeder randomIBANSeeder)
        {
            _context = context;
            _dailyLimits = dailyLimits;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _configuration = configuration;
            _randomIBANSeeder = randomIBANSeeder;
        }

        private Account CreateAccountFromModel(AccountCreationModel model,int userId)
        {
            var account = _mapper.Map<Account>(model);
            account.IBAN = _randomIBANSeeder.GenerateRandomIBAN();
            account.DailyTransferLimit = 50000;
            account.DailyWithdrawalLimit = 20000;
            account.CreationDate = DateTime.Now;
            account.UserId = userId;

            return account;
        }

        private bool IsMinimumBalanceSatisfied(Account account)
        {
            decimal minimumBalance = _configuration.GetValue<decimal>("MinimumBalance");
            return account.Balance >= minimumBalance;
        }
        public async Task<Account> CreateAccountAsync(AccountCreationModel model, int userId)
        {
            var account = CreateAccountFromModel(model, userId);
            if (!IsMinimumBalanceSatisfied(account))
            {
                throw new ArgumentException("Minimum balance requirement is not met.");
            }

            await _accountRepository.CreateAccountAsync(account);
            return account;
        }

        public async Task<List<AccountDto>> GetUserAccountsAsync(string userId)
        {
            var accounts = await _accountRepository.GetUserAccountsAsync(int.Parse(userId));
            var accountDtos = _mapper.Map<List<AccountDto>>(accounts);
            return accountDtos;
        }

        public async Task ResetDailyLimits()
        {
            var allAccounts = await _context.Accounts.ToListAsync();

            foreach (var account in allAccounts)
            {
                // Günlük çekim ve transfer limitlerini varsayılan değerlere döndür
                account.DailyWithdrawalLimit = _dailyLimits.DefaultDailyWithdrawalLimit ;
                account.DailyTransferLimit = _dailyLimits.DefaultDailyTransferLimit;
            }

            await _context.SaveChangesAsync();
        }
    }
}
