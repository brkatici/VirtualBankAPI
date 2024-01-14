using RestfulBankApi.Data;
using RestfulBankApi.Models;

namespace RestfulBankApi
{


    public interface ICreditScore
    {
        string CheckCreditScore(int customerId);
    }

    public class CreditScoreService : ICreditScore
    {
        private readonly Random _random;
        private readonly AppDbContext _context;
        public CreditScoreService(AppDbContext context)
        {
            _random = new Random();
            _context = context;
        }

        public int CalculateRandomCreditScore()
        {
            // Rastgele bir kredi skoru üretmek için örnek bir metot
            return _random.Next(300, 850); // Genellikle kredi skoru 300 ile 850 arasında olur
        }

        public string CheckCreditScore(int customerId)
        {
            var customerAccounts = _context.Accounts.Where(u => u.UserId == customerId).ToList();
            var checkActiveLoans = _context.Credits.Where(c => c.UserId == customerId && c.Active == true).ToList();

            if (checkActiveLoans.Any())
            {
                return "Mevcut kredi ödemeniz bulunduğu için şu anda kredi öneremiyoruz";
            }

            var loanOffers = _context.Loans.ToList(); // Retrieve loan offers from the database

            foreach (var account in customerAccounts)
            {
                foreach (var loanOffer in loanOffers)
                {
                    switch (loanOffer.OfferCode)
                    {
                        case "IHTIYAC200":
                            if (account.Active && account.Balance > 15000 && account.AccountType == "Savings" && account.InsuranceCoverage > 50000)
                            {
                                return "Savings hesabınız ve yüksek sigorta kapsamınız nedeniyle uygun bir kredi önerimiz var! %2,79 faiz oranıyla 200.000 ₺ nakit kredi imkanı. Başvurmak için (IHTIYAC200)";
                            }
                            break;

                        case "YATIRIM20":
                            if (account.Active && account.Balance > 5000 && account.AccountType == "Investment")
                            {
                                return "Yüksek yatırım bakiyesi nedeniyle özel bir kredi fırsatı! %0 faizli 6 ay vade 20.000 tl yatırım kredisi imkanı. Başvurmak için (YATIRIM20)";
                            }
                            break;

                            // Add more cases for other loan offers as needed
                    }
                }
            }

            return "Üzgünüz, şu anda kredi öneremiyoruz.";
        }

    }
}
