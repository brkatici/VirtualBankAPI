using NUnit.Framework;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using RestfulBankApi.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RestfulBankApi.DTOs;
using Microsoft.Extensions.Configuration;

namespace RestfulBankApi.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<RandomIBANSeeder> _mockRandomIBANSeeder;
        private AccountService _accountService;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockRandomIBANSeeder = new Mock<RandomIBANSeeder>();

            _accountService = new AccountService(
                context: null, // Pass null as AppDbContext is not needed for these tests
                dailyLimits: null, // Pass null as DailyLimits is not needed for these tests
                _mockAccountRepository.Object,
                _mockMapper.Object,
                _mockConfiguration.Object,
                _mockRandomIBANSeeder.Object
            );
        }

        [Test]
        public async Task CreateAccountAsync_WhenMinimumBalanceMet_CreatesAccount()
        {
            // Arrange
            var model = new AccountCreationModel(); // Set up your model
            var userId = 1;

            var createdAccount = new Account(); // Set up a created account

            _mockMapper.Setup(mapper => mapper.Map<Account>(model)).Returns(createdAccount);
            _mockRandomIBANSeeder.Setup(seeder => seeder.GenerateRandomIBAN()).Returns("testIBAN");
            _mockConfiguration.Setup(conf => conf.GetValue<decimal>("MinimumBalance")).Returns(100);

            _mockAccountRepository.Setup(repo => repo.CreateAccountAsync(createdAccount));

            // Act
            var result = await _accountService.CreateAccountAsync(model, userId);

            // Assert
            Assert.AreEqual(createdAccount, result);
            // Add more assertions based on different scenarios and conditions
        }

        [Test]
        public async Task GetUserAccountsAsync_ReturnsUserAccounts()
        {
            // Arrange
            var userId = "1";
            var accounts = new List<Account>(); // Set up a list of accounts

            _mockAccountRepository.Setup(repo => repo.GetUserAccountsAsync(int.Parse(userId))).ReturnsAsync(accounts);

            var accountDtos = new List<AccountDto>(); // Set up a list of AccountDto

            _mockMapper.Setup(mapper => mapper.Map<List<AccountDto>>(accounts)).Returns(accountDtos);

            // Act
            var result = await _accountService.GetUserAccountsAsync(userId);

            // Assert
            Assert.AreEqual(accountDtos, result);
            // Add more assertions based on different scenarios and conditions
        }

        // Write more test methods to cover various scenarios for the AccountService methods

        [TearDown]
        public void TearDown()
        {
            // Dispose or clean up resources if needed
        }
    }
}
