using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulBankApi.Data;
using RestfulBankApi.Models;

namespace RestfulBankApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ICreditScore _creditScoreService;
        private readonly AppDbContext _context;

        public LoanController(ICreditScore creditScoreService,AppDbContext context)
        {
            _creditScoreService = creditScoreService;
            _context = context;
        }
        [HttpGet("GetLoanOffers")]
        public IActionResult GetLoanOffers()
        {
            var customerId = HttpContext.User.FindFirst("UserId")?.Value;
            int customerIdtoInt = int.Parse(customerId);    
            // Müşteri kredi skoru kontrol edilir
            string isCreditScoreGood = _creditScoreService.CheckCreditScore(customerIdtoInt);

            return Ok(isCreditScoreGood);
        }


        [HttpPost("ApplyForLoan")]
        public IActionResult ApplyForLoan(string BasvuruKodu, string IBAN)
        {
            var customerId = HttpContext.User.FindFirst("UserId")?.Value;
            if (!int.TryParse(customerId, out int customerIdtoInt))
            {
                return BadRequest("Invalid customer ID.");
            }

            var account = _context.Accounts.FirstOrDefault(acc => acc.IBAN == IBAN && acc.UserId == customerIdtoInt);

            if (account == null)
            {
                return BadRequest("Account ID is not valid for the current customer.");
            }

            var loanOffer = _context.Loans.SingleOrDefault(l => l.OfferCode == BasvuruKodu);

            if (loanOffer == null)
            {
                return BadRequest("Invalid loan offer code.");
            }

            var checkActiveLoans = _context.Credits.Any(c => c.UserId == customerIdtoInt && c.Active);

            if (checkActiveLoans)
            {
                return BadRequest("There is an active loan application.");
            }

            var credit = new Credit
            {
                UserId = customerIdtoInt,
                IBAN = IBAN,
                LoanId = loanOffer.Id,
                Active = false
            };

            _context.Credits.Add(credit);
            _context.SaveChanges();

            return Ok($"{BasvuruKodu} loan application has been submitted.");
        }




    }
}
