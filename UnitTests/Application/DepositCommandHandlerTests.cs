//using BankingSystem.Application.Commands.Transactions;
//using BankingSystem.Application.Interfaces;
//using Moq;
//using Xunit;

//namespace BankingSystem.UnitTests.Application.Commands
//{
//    public class DepositCommandHandlerTests
//    {
//        [Fact]
//        public async Task Handle_ValidCommand_ShouldReturnTrue()
//        {
//            // Arrange
//            var transactionServiceMock = new Mock<ITransactionService>();
//            transactionServiceMock.Setup(s => s.DepositAsync(It.IsAny<Guid>(), It.IsAny<decimal>())).ReturnsAsync(true);

//            var handler = new DepositHandler(transactionServiceMock.Object);
//            var command = new DepositCommand { AccountId = , Amount = 100 };

//            // Act
//            var result = await handler.Handle(command, default);

//            // Assert
//            Assert.True(result);
//            transactionServiceMock.Verify(s => s.DepositAsync(command.AccountId, command.Amount), Times.Once);
//        }

//        [Fact]
//        public async Task Handle_InvalidCommand_ShouldReturnFalse()
//        {
//            // Arrange
//            var transactionServiceMock = new Mock<ITransactionService>();
//            transactionServiceMock.Setup(s => s.DepositAsync(It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync(false);

//            var handler = new DepositHandler(transactionServiceMock.Object);
//            var command = new DepositCommand { AccountId = -1, Amount = 100 };

//            // Act
//            var result = await handler.Handle(command, default);

//            // Assert
//            Assert.False(result);
//        }
//    }
//}
