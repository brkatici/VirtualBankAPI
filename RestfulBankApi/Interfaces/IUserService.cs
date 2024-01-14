using RestfulBankApi.DTOs;
using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IUserService
    {
        Task<string> AddUserAsync(UserDto model);
        Task<string> ResetPasswordAsync(string email, string newPassword, string newPasswordAgain);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
    }
}
