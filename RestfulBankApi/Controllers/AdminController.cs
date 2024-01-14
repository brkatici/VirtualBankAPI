using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Data;
using RestfulBankApi.DTOs;

namespace RestfulBankApi.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

            [HttpGet("user-accounts")]
            public async Task<IActionResult> GetUserAccounts()
            {

                var accounts = _context.Accounts.ToList();
                
                return Ok(accounts);
            }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = _context.Users.ToList();

            return Ok(users);
        }

        [HttpPut("update-account/{accountId}")]
        public async Task<IActionResult> UpdateAccountStatus(int accountId, [FromBody] bool isActive)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return NotFound(); // Hesap bulunamazsa NotFound döndür
            }

            account.Active = isActive;
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return Ok(account); // Güncellenmiş hesabı döndür
        }
        [HttpPut("update-account")]
        public async Task<IActionResult> UpdateAccountStatus(int userId,bool isActive, [FromBody] string role)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound(); // Hesap bulunamazsa NotFound döndür
            }

            user.isActive = isActive;
            user.Role = role;   
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(user); // Güncellenmiş hesabı döndür
        }

        [HttpGet("need-support")]
        public async Task<IActionResult> SupportMessages()
        {

            var accounts = _context.SupportMessages.ToList();

            return Ok(accounts);
        }
    }
}
