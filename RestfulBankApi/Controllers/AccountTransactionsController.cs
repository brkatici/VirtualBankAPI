using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Data;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using Swashbuckle.AspNetCore.Filters;


namespace RestfulBankApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionsController : ControllerBase
    {

        private readonly IAccountTransactionService _transactionService;

        public AccountTransactionsController(IAccountTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(string IBAN, decimal Amount)
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId")?.Value;
            if (userIdClaim != null && int.TryParse(userIdClaim, out int userId))
            {
                var result = await _transactionService.DepositAsync(IBAN, Amount, userId);

                if (result == "Transaction successful")
                    return Ok(result);
                else if (result == "Account not found.")
                    return NotFound(result);
                else
                    return BadRequest(result);
            }
            else
            {
                return NotFound("Cannot read user data.");  
            } 
            
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(string IBAN, decimal Amount)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst("UserId")?.Value;
                if (userIdClaim != null && int.TryParse(userIdClaim, out int userId))
                {
                    var result = await _transactionService.WithDrawAsync(IBAN, Amount, userId);
                    return result switch
                    {
                        "Transaction successful" => Ok(result),
                        "Account not found." => NotFound(result),
                        _ => BadRequest(result),
                    };
                }
                else
                {
                    return NotFound("Cannot read user data.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your requirements
                return StatusCode(500, ex.Message);
            }
        }
    }
}






