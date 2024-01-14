using NUnit.Framework;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using RestfulBankApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RestfulBankApi.DTOs;

namespace RestfulBankApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<PasswordHasher> _mockPasswordHasher;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockPasswordHasher = new Mock<PasswordHasher>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockMapper.Object,
                _mockPasswordHasher.Object
            );
        }

        [Test]
        public async Task AddUserAsync_WhenValidUserDto_CreatesUserSuccessfully()
        {
            // Arrange
            var userDto = new UserDto(); // Set up your UserDto model

            var user = new User(); // Set up a user object

            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(It.IsAny<string>())).Returns("hashedPassword");
            _mockMapper.Setup(mapper => mapper.Map<User>(userDto)).Returns(user);

            _mockUserRepository.Setup(repo => repo.AddUserAsync(user));

            // Act
            var result = await _userService.AddUserAsync(userDto);

            // Assert
            Assert.AreEqual("Account created successfully.", result);
            // Add more assertions based on different scenarios and conditions
        }

        [Test]
        public async Task ResetPasswordAsync_WhenPasswordsMatch_ResetsPasswordSuccessfully()
        {
            // Arrange
            var email = "test@example.com";
            var newPassword = "newPassword";
            var newPasswordAgain = "newPassword";

            var user = new User(); // Set up a user object

            _mockUserRepository.Setup(repo => repo.GetUserByEmailAsync(email)).ReturnsAsync(user);

            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(newPassword)).Returns("hashedPassword");

            _mockUserRepository.Setup(repo => repo.UpdateUserAsync(user));

            // Act
            var result = await _userService.ResetPasswordAsync(email, newPassword, newPasswordAgain);

            // Assert
            Assert.AreEqual("Password reset successfully.", result);
            // Add more assertions based on different scenarios and conditions
        }

        // Write more test methods to cover various scenarios for the UserService methods

        [TearDown]
        public void TearDown()
        {
            // Dispose or clean up resources if needed
        }
    }
}
