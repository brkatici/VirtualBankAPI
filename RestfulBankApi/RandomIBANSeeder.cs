namespace RestfulBankApi
{
    public class RandomIBANSeeder
    {
        public string GenerateRandomIBAN()
        {
            // TR ile başlayan rastgele bir IBAN oluştur
            Random random = new Random();
            const string countryCode = "TR";
            string iban = countryCode;

            // Rastgele 18 rakam oluşturarak IBAN'ın kalan kısmını oluştur
            for (int i = 0; i < 18; i++)
            {
                iban += random.Next(0, 10); // 0 ile 9 arasında rastgele bir rakam ekle
            }

            return iban;
        }
    }
}
