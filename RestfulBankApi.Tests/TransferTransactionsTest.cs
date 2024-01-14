using NUnit.Framework;
using RestfulBankApi.Interfaces;
using RestfulBankApi.Models;
using RestfulBankApi.Services;
using System;
using System.Threading.Tasks;
using Moq;

namespace RestfulBankApi.Tests
{
    [TestFixture]
    public class TransferServiceTests
    {
        private Mock<ITransferRepository> _mockTransferRepository;
        private Mock<IAccountRepository> _mockAccountRepository;
        private ITransferService _transferService;

        [SetUp]
        public void Setup()
        {
            _mockTransferRepository = new Mock<ITransferRepository>();
            _mockAccountRepository = new Mock<IAccountRepository>();

            _transferService = new TransferService(_mockTransferRepository.Object, _mockAccountRepository.Object);
        }

        // Write test methods for IsAccountOwnedByUserAsync method
        [Test]
        public async Task IsAccountOwnedByUserAsync_WhenAccountOwned_ReturnsTrue()
        {
            // Arrange
            var iban = "testIBAN";
            var userId = 1;
            var account = new Account { UserId = userId };
            _mockAccountRepository.Setup(repo => repo.GetAccountByUserIBANAsync(iban))
                                  .ReturnsAsync(account);

            // Act
            var result = await _transferService.IsAccountOwnedByUserAsync(iban, userId);

            // Assert
            Assert.IsTrue(result);
        }

        // Write test methods for TransferBetweenUserAccountsAsync method
        [Test]
        public async Task TransferBetweenUserAccountsAsync_WhenValidTransfer_ReturnsSuccessMessage()
        {
            // Arrange
            var senderIBAN = "TR230891141217319427";
            var receiverIBAN = "TR481846516471270938";
            var amount = 100;
            var senderUserId = 1;

            var senderAccount = new Account { UserId = senderUserId, Balance = 200, DailyTransferLimit = 500 };
            var receiverAccount = new Account { UserId = 2, Balance = 300, DailyTransferLimit = 500 };

            _mockAccountRepository.Setup(repo => repo.GetAccountByUserIBANAsync(senderIBAN))
                                  .ReturnsAsync(senderAccount);

            _mockAccountRepository.Setup(repo => repo.GetAccountByUserIBANAsync(receiverIBAN))
                                  .ReturnsAsync(receiverAccount);

            // Act
            var result = await _transferService.TransferBetweenUserAccountsAsync(senderIBAN, receiverIBAN, amount, senderUserId);

            // Assert
            Assert.AreEqual("Transfer Successful", result);
            // Add more assertions based on different scenarios and conditions
        }

        // Write test methods for TransferToAnotherUserAsync method
        [Test]
        public async Task TransferToAnotherUserAsync_WhenValidTransfer_ReturnsSuccessMessage()
        {
            // Arrange
            // Similar setup as above for transfer method

            // Act
            var result = await _transferService.TransferToAnotherUserAsync("TR575202433939709203", "TR085895820074454270", 1200, 9);

            // Assert
            Assert.AreEqual("Transfer Successful", result);
            // Add more assertions based on different scenarios and conditions
        }

        // Write more test methods to cover various scenarios for the TransferService methods

        [TearDown]
        public void TearDown()
        {
            // Dispose or clean up resources if needed
        }
    }
}
