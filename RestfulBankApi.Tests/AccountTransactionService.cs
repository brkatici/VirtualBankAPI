using NUnit.Framework;
using Moq;
using RestfulBankApi.Models;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Services;
using System.Threading.Tasks;
using RestfulBankApi.Data;
using static RestfulBankApi.Interfaces.IAccountTransactionService;

namespace RestfulBankApi.Tests
{
    [TestFixture]
    public class AccountTransactionServiceTests
    {
        private Mock<IAccountRepository> _mockAccountRepository;
        private Mock<IAccountTransactionRepository> _mockAccountTransactionRepository;
        private Mock<AppDbContext> _mockDbContext;
        private IAccountTransactionService _accountTransactionService;

        [SetUp]
        public void Setup()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockAccountTransactionRepository = new Mock<IAccountTransactionRepository>();
            _mockDbContext = new Mock<AppDbContext>();

            _accountTransactionService = new AccountTransactionService(
                _mockAccountRepository.Object,
                _mockDbContext.Object,
                _mockAccountTransactionRepository.Object
            );
        }

        [Test]
        public async Task DepositAsync_WhenAccountNotFound_ReturnsAccountNotFoundMessage()
        {
            // Arrange
            _mockAccountRepository.Setup(repo => repo.GetAccountByUserIdAndIBANAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((Account)null);

            // Act
            var result = await _accountTransactionService.DepositAsync("testIBAN", 100, 1);

            // Assert
            Assert.AreEqual("Account not found.", result);
        }

        [Test]
        public async Task WithDrawAsync_WhenInsufficientFunds_ThrowsInsufficientFundsException()
        {
            // Arrange
            var mockAccount = new Account { Balance = 100 };
            _mockAccountRepository.Setup(repo => repo.GetAccountByUserIdAndIBANAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(mockAccount);

            // Act and Assert
            Assert.ThrowsAsync<InsufficientFundsException>(async () =>
            {
                await _accountTransactionService.WithDrawAsync("testIBAN", 200, 1);
            });
        }

        // Add more tests for different scenarios, like successful transactions, pending approvals, etc.
    }
}
