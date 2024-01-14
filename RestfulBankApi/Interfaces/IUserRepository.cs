using RestfulBankApi.Models;

namespace RestfulBankApi.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();
    }
}
