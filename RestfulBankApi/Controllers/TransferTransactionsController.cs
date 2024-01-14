using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using RestfulBankApi.Services;

namespace RestfulBankApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferTransactionsController : ControllerBase
    {


        private readonly ITransferService _transferService;

        public TransferTransactionsController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost("TransferBetweenUserAccounts")]
        public async Task<IActionResult> TransferBetweenUserAccounts(string receiverIBAN, string senderIBAN, decimal amount)
        {
            var userId = HttpContext.User.FindFirst("UserId")?.Value;
            var result = "";

            if (userId != null && int.TryParse(userId, out int senderUserId))
            {
                result= await _transferService.TransferBetweenUserAccountsAsync(senderIBAN, receiverIBAN, amount, senderUserId);
            }
            else
            {
                return NotFound("Cannot read user data.");
            }            
            return Ok(result);
        }

        [HttpPost("TransferToAnotherUser")]
        public async Task<IActionResult> TransferToAnotherUser(string receiverIBAN, string senderIBAN, decimal amount)
        {

            var userId = HttpContext.User.FindFirst("UserId")?.Value;
            var result = "";

            if (userId != null && int.TryParse(userId, out int senderUserId))
            {
                result = await _transferService.TransferToAnotherUserAsync(senderIBAN, receiverIBAN, amount, senderUserId);
            }
            else
            {
                return NotFound("Cannot read user data.");
            }
            return Ok(result);
        }
        //private readonly AppDbContext _context;
        //private readonly IMapper _mapper;
        //public TransferTransaction transfer = new TransferTransaction();    
        //public TransferTransactionsController(AppDbContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        //private bool AccountExists(string userId, string iban)
        //{
        //    return _context.Accounts.Any(a => a.UserId.ToString() == userId && a.IBAN == iban);
        //}

        //private IActionResult HandleTransaction(string senderIBAN, string receiverIBAN, decimal amount)
        //{
        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var userId = HttpContext.User.FindFirst("UserId")?.Value;
        //            var senderAccounts = _context.Accounts.Where(a => a.UserId.ToString() == userId).ToList();
        //            var receiverAccount = _context.Accounts.FirstOrDefault(a => a.IBAN == receiverIBAN);

        //            bool ableToTransfer = AccountExists(userId, senderIBAN) && receiverAccount != null;

        //            if (ableToTransfer)
        //            {
        //                var sourceAccount = senderAccounts.FirstOrDefault(f => f.IBAN == senderIBAN);

        //                if (sourceAccount.Balance >= amount)
        //                {
        //                    var transfer = new TransferTransaction
        //                    {
        //                        SourceIBAN = senderIBAN,
        //                        TargetIBAN = receiverIBAN,
        //                        TransactionType = "HAVALE",
        //                        Amount = amount
        //                    };

        //                    _context.TransferTransactions.Add(transfer);

        //                    sourceAccount.Balance -= amount;
        //                    receiverAccount.Balance += amount;

        //                    _context.SaveChanges();
        //                    transaction.Commit();
        //                    return Ok("Transfer Successful");
        //                }
        //                else
        //                {
        //                    return BadRequest("Insufficient balance");
        //                }
        //            }
        //            else
        //            {
        //                return BadRequest("One or both accounts not found");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            return StatusCode(500, "An error occurred while processing the request.");
        //        }
        //    }
        //}

        //[HttpPost("HesaplarArasıAktarım")]
        //public IActionResult Deposit(string AliciIBAN, string GondericiIBAN, decimal Miktar)
        //{
        //    return HandleTransaction(GondericiIBAN, AliciIBAN, Miktar);
        //}

        //[HttpPost("BaşkaHesabaAktarım")]
        //public IActionResult Withdraw(string AliciIBAN, string GondericiIBAN, decimal Miktar)
        //{
        //    return HandleTransaction(GondericiIBAN, AliciIBAN, Miktar);
        //}


    }
}
