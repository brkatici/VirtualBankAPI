using Microsoft.EntityFrameworkCore.Storage;
using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;

namespace RestfulBankApi.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly AppDbContext _context;

        public TransferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTransferAsync(TransferTransaction transfer)
        {
            _context.TransferTransactions.Add(transfer);
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        // Implement other methods as needed...
        // ...
    }
}
