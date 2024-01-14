using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulBankApi.Data;
using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Interfaces;
using static RestfulBankApi.Interfaces.IAccountTransactionService;
namespace RestfulBankApi.Controllers
{
    [Authorize(Policy = "AuditorPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuditorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAccountTransactionService _accountTransactionService;

        public AuditorController(AppDbContext context, IAccountTransactionService accountTransactionService)
        {
            _context = context;
            _accountTransactionService = accountTransactionService;
        }

        [HttpGet("account-transactions")]
        public async Task<IActionResult> GetAccountTransactions()
        {
            var transactions = await _context.AccountTransactions
                .Where(t => t.Status == "Daily Limit Exceeded" || t.Status == "Pending Approval")
                .ToListAsync();

            return Ok(transactions);
        }
        [HttpPut("update-transaction-status/{transactionId}")]
        public async Task<IActionResult> UpdateTransactionStatus(int transactionId, bool isApproved)
        {
            var transaction = await _context.AccountTransactions.FindAsync(transactionId);

            if (transaction == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(transaction.AccountId);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            try
            {
                var result = await _accountTransactionService.WithDrawAuditorControl(account, transaction.Amount, isApproved);

                if (result != "Transaction successful")
                {
                    return BadRequest(result);
                }

                transaction.Status = isApproved ? "Approved" : "Declined"; // Assuming 'status' refers to 'Approved' or 'Declined'
                _context.AccountTransactions.Update(transaction);
                await _context.SaveChangesAsync();

                return Ok(transaction);
            }
            catch (AccountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InsufficientFundsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the transaction");
            }
        }


        [HttpGet("transfer-transactions")]
        public async Task<IActionResult> GetTransferTransactions()
        {
            var transferTransactions = await _context.TransferTransactions.ToListAsync();

            return Ok(transferTransactions);
        }
    }

}
