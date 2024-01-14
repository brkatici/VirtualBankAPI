using AutoMapper;
using RestfulBankApi.DTOs;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;

namespace RestfulBankApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, PasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> AddUserAsync(UserDto model)
        {
            model.PasswordHash = _passwordHasher.HashPassword(model.PasswordHash);
            var user = _mapper.Map<User>(model);
            user.Role = "User";
            user.RegistrationDate = DateTime.Now;

            await _userRepository.AddUserAsync(user);
            return "Account created successfully.";
        }

        public async Task<string> ResetPasswordAsync(string email, string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain)
            {
                return "Passwords do not match";
            }

            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return "User not found";
            }

            // Hash the new password and save
            user.PasswordHash = _passwordHasher.HashPassword(newPassword);

            await _userRepository.UpdateUserAsync(user);
            return "Password reset successfully.";
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
