using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Models;
using RestfulBankApi.Interfaces;

namespace RestfulBankApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }
        public DbSet<SupportMessages> SupportMessages { get; set; }
        public DbSet<ApplicationUserLogs> ApplicationUserLogs { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<LoanOffers> Loans { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("RestfulBankDb"); // Your database connection string should go here
            }
        }
    }

}
