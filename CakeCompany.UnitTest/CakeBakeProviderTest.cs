using CakeCompany.Models;
using CakeCompany.Models.Cakes;
using CakeCompany.Models.Enum;
using CakeCompany.Provider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;


namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class CakeBakeProviderTest
    {
        private Mock<ILogger<CakeBakeProvider>> mockLogger;
        private CakeBakeProvider cakeBakeProvider;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<CakeBakeProvider>>();
        }
        [Test]
        public void Bake_return_ChocolateCake_For_Order_Cake_Chocolate()
        {
            //Arrange
            DateTime chocolateCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(30);
            Order chocolateCakeOrder = new("CakeBox", chocolateCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.Chocolate, 100.25);
            cakeBakeProvider = new CakeBakeProvider(mockLogger.Object);
            //Act
            var product = cakeBakeProvider.Bake(chocolateCakeOrder);
            //Assert

            Assert.True(product.Cake.GetType() == typeof(ChocolateCake));
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Creating cake {chocolateCakeOrder.Name} for order with Id {chocolateCakeOrder.Id}.");
        }

        [Test]
        public void Bake_return_RedVelvetCake_For_Order_Cake_RedVelvet()
        {
            //Arrange
            DateTime redVelvetCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(60);
            Order redVelvetCakeOrder = new("CakeBox", redVelvetCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.RedVelvet, 100.25);
            cakeBakeProvider = new CakeBakeProvider(mockLogger.Object);
            //Act
            var product = cakeBakeProvider.Bake(redVelvetCakeOrder);
            //Assert

            Assert.True(product.Cake.GetType() == typeof(RedVelvetCake));
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Creating cake {redVelvetCakeOrder.Name} for order with Id {redVelvetCakeOrder.Id}.");
        }
        [Test]
        public void Bake_return_VanillaCake_For_Order_Cake_Vanilla()
        {
            //Arrange
            DateTime vanillaCakeOrderDeliveryCheckTime = DateTime.Now.AddHours(1);
            Order vanillaCakeOrder = new("CakeBox", vanillaCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.Vanilla, 100.25);
            cakeBakeProvider = new CakeBakeProvider(mockLogger.Object);
            //Act
            var product = cakeBakeProvider.Bake(vanillaCakeOrder);
            //Assert

            Assert.True(product.Cake.GetType() == typeof(VanillaCake));
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Creating cake {vanillaCakeOrder.Name} for order with Id {vanillaCakeOrder.Id}.");
        }
    }
}
