using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestfulBankApi.Data;
using RestfulBankApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestfulBankApi.Authentication
{
    public class JwtAuthenticationManager
    {
        private readonly string key;
        private readonly AppDbContext _context;
        private readonly PasswordHasher _passwordHasher;
        private BinaryReader userId;

        public JwtAuthenticationManager(string key, AppDbContext context, PasswordHasher passwordHasher)
        {
            this.key = key;

            this._context = context;
            _passwordHasher = passwordHasher;
        }

        public string Authenticate(string username, string password, List<User> users)
        {
            var user = users.FirstOrDefault(u => u.UserName == username);

            if (user != null && user.isActive!=false)
            {
                // Kullanıcı varsa, kullanıcının kaydedilmiş şifresini hash'leyerek doğrulama yapılır
                var verificationResult = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

                if (verificationResult == PasswordVerificationResult.Success)
                {
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId",user.UserId.ToString())
                        }),
                        //set duration of token here
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(tokenKey),
                            SecurityAlgorithms.HmacSha256Signature) //setting sha256 algorithm
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return tokenHandler.WriteToken(token);
                }
            }

            // Kullanıcı yok veya şifre doğrulaması başarısız olduysa null döndürülür
            return null;           
        }
    }
}
