using CakeCompany.Models;
using CakeCompany.Models.Enum;
using CakeCompany.Provider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class OrderQueryProviderTest
    {
        private Mock<ILogger<OrderQueryProvider>> mockLogger;
        private OrderQueryProvider orderQueryProvider;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<OrderQueryProvider>>();
        }
        [Test]
        public void GetLatestOrders_return_CorrectOrders()
        {
            //Arrange
            var expectedCakeOrders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.Chocolate, 120.25),
                    new("ImportantCakeShop", DateTime.Now, 2, Cake.RedVelvet, 120.25)
                };
            orderQueryProvider = new OrderQueryProvider(mockLogger.Object);
            //Act
            var orders = orderQueryProvider.GetLatestOrders();
            //Assert

            Assert.AreEqual(orders.Count(), expectedCakeOrders.Count());
            mockLogger.VerifyLogWasCalled(LogLevel.Information,"Starting to execute GetLatestOrders.");
        }

    }
    
}
