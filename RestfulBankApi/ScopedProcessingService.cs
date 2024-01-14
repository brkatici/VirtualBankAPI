using RestfulBankApi.Data;
using RestfulBankApi.Models;

namespace RestfulBankApi
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<ScopedProcessingService> _logger;
        private readonly AppDbContext _context;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task ProcessPayments()
        {
            _logger.LogInformation("Processing payments at: {time}", DateTimeOffset.Now);

            var accountsWithAutoPayments = _context.Accounts.ToList().Where(acc => acc.AutomaticPayments == true);

            foreach (var account in accountsWithAutoPayments)
            {

                var credit = _context.Credits.FirstOrDefault(c => c.UserId == account.UserId);
                if (credit != null)
                {
                    // Aylık ödeme miktarının hesaplanması (faiz oranı ve kredi miktarına göre)
                    var linkedAccount = _context.Accounts.FirstOrDefault(acc => acc.IBAN == credit.IBAN);
                    if (linkedAccount != null)
                    {
                        // Calculate monthly payment based on interest rate and loan amount
                        decimal monthlyPayment = CalculateMonthlyPayment(credit);

                        // Deduct monthly payment from the linked account's balance
                        if (linkedAccount.Balance >= monthlyPayment)
                        {
                            linkedAccount.Balance -= monthlyPayment;
                            _context.SaveChanges();
                        }
                        else
                        {
                            // Insufficient balance in the linked account
                            // Perform necessary actions like sending a warning message, canceling the transaction, etc.
                        }
                    }
                }
            }
        }

        private decimal CalculateMonthlyPayment(Credit credit)
        {
            // Monthly interest rate calculation
            var loan = _context.Loans.FirstOrDefault(l => l.Id == credit.LoanId);
            decimal monthlyInterestRate = loan.InterestRate / 12 / 100;

            // Number of payments (loan term in months)
            int numberOfPayments = loan.LoanTermMonths;

            // Monthly payment calculation using the formula for amortizing a loan
            decimal monthlyPayment = loan.LoanAmount * (monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), numberOfPayments)) /
                                    ((decimal)Math.Pow((double)(1 + monthlyInterestRate), numberOfPayments) - 1);

            return monthlyPayment;
        }

    }

}
