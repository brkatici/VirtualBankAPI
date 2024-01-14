using Microsoft.EntityFrameworkCore.Storage;
using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface ITransferRepository
    {
        Task AddTransferAsync(TransferTransaction transfer);
        IDbContextTransaction BeginTransaction();
        // Other methods as needed...
    }
}
