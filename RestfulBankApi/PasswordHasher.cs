using Microsoft.AspNetCore.Identity;

namespace RestfulBankApi
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            // Şifreyi hash'lemek için gerekli işlemler burada yapılır
            // Örnek bir hash algoritması kullanalım (bu örnek amaçlıdır, gerçek uygulamalarda daha güçlü algoritmalar kullanılmalıdır)
            return BCrypt.Net.BCrypt.HashPassword(password); // Örnek olarak BCrypt kullanımı
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            // Hashlenmiş şifrenin verilen şifreyle eşleşip eşleşmediği kontrol edilir
            if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}
