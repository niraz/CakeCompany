using CakeCompany.Models;
using CakeCompany.Models.Enum;
using CakeCompany.Models.Payment;
using CakeCompany.Provider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;


namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class PaymentProviderTest
    {
        private Mock<ILogger<PaymentProvider>> mockLogger;
        private PaymentProvider paymentProvider;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<PaymentProvider>>();
        }
        [Test]
        public void Process_return_ImportantPaymentIn_When_ClinetName_Contains_Important()
        {
            //Arrange
            DateTime chocolateCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(30);
            Order chocolateCakeOrder = new("CakeBoxImportant", chocolateCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.Chocolate, 100.25);
            paymentProvider = new PaymentProvider(mockLogger.Object);
            //Act
            var result = paymentProvider.Process(chocolateCakeOrder);
            //Assert

            Assert.True(result.GetType() == typeof(ImportantPaymentIn));
            Assert.False(result.HasCreditLimit);
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Processing payment for client name {chocolateCakeOrder.ClientName} with order with Id {chocolateCakeOrder.Id}.");
        }

        [Test]
        public void Process_return_PaymentIn_When_ClinetName_DoesNotContains_Important()
        {
            //Arrange
            DateTime chocolateCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(30);
            Order chocolateCakeOrder = new("CakeBox", chocolateCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.Chocolate, 100.25);
            paymentProvider = new PaymentProvider(mockLogger.Object);
            //Act
            var result = paymentProvider.Process(chocolateCakeOrder);
            //Assert

            Assert.True(result.GetType() == typeof(PaymentIn));
            Assert.True(result.HasCreditLimit);
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Processing payment for client name {chocolateCakeOrder.ClientName} with order with Id {chocolateCakeOrder.Id}.");
        }
    }
}
