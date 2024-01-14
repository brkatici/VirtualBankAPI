using System.Text.Json.Serialization;

namespace RestfulBankApi.Models
{
    public class User
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string Role {  get; set; }    

        public List<Account> Accounts { get; set; }
        public bool isActive { get; set; } 

    }

}
