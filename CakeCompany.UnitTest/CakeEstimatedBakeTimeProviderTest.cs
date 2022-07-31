using CakeCompany.Contract.Provider;
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
    public class CakeEstimatedBakeTimeProviderTest
    {
        private Mock<ILogger<CakeEstimatedBakeTimeProvider>> mockLogger;
        private ICakeEstimatedBakeTimeProvider cakeEstimatedBakeTimeProvider;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<CakeEstimatedBakeTimeProvider>>();
        }
        [Test]
        public void GetEstimatedBakeTime_return_30Minutes_For_CakeOrder_Chocolate()
        {
            //Arrange
            DateTime expectedChocolateCakeOrderBakeTime = DateTime.Now.AddMinutes(30);
            Order chocolateCakeOrder = new("CakeBox", DateTime.Now, 1, Cake.Chocolate, 100.25);
            cakeEstimatedBakeTimeProvider = new CakeEstimatedBakeTimeProvider(mockLogger.Object);
            //Act
            var actualCakeOrderBakeTime = cakeEstimatedBakeTimeProvider.GetEstimatedBakeTime(chocolateCakeOrder);
            //Assert

            Assert.AreEqual(actualCakeOrderBakeTime.ToShortTimeString(), expectedChocolateCakeOrderBakeTime.ToShortTimeString());
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Cake bake time for OrderId {chocolateCakeOrder.Id} for cake type {chocolateCakeOrder.Name}.");
        }

        [Test]
        public void GetEstimatedBakeTime_return_60Minutes_For_CakeOrder_RedVelvet()
        {
            //Arrange
            DateTime expectedRedVelvetCakeBakeTime = DateTime.Now.AddMinutes(60);
            Order redVelvetCakeOrder = new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 100.25);
            cakeEstimatedBakeTimeProvider = new CakeEstimatedBakeTimeProvider(mockLogger.Object);
            //Act
            var actualCakeBakeTime = cakeEstimatedBakeTimeProvider.GetEstimatedBakeTime(redVelvetCakeOrder);
            //Assert

            Assert.AreEqual(actualCakeBakeTime.ToShortTimeString(), expectedRedVelvetCakeBakeTime.ToShortTimeString());
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Cake bake time for OrderId {redVelvetCakeOrder.Id} for cake type {redVelvetCakeOrder.Name}.");
        }
        [Test]
        public void GetEstimatedBakeTime_return_15Hours_For_CakeOrder_Vanilla()
        {
            //Arrange
            DateTime expectedVanillaCakeOrderBakeTime = DateTime.Now.Add(TimeSpan.FromHours(15));
            Order vanillaCakeOrder = new("CakeBox", DateTime.Now, 1, Cake.Vanilla, 100.25);
            cakeEstimatedBakeTimeProvider = new CakeEstimatedBakeTimeProvider(mockLogger.Object);
            //Act
            var actualCakeBakeTime = cakeEstimatedBakeTimeProvider.GetEstimatedBakeTime(vanillaCakeOrder);
            //Assert

            Assert.AreEqual(actualCakeBakeTime.ToShortTimeString(), expectedVanillaCakeOrderBakeTime.ToShortTimeString());
            mockLogger.VerifyLogWasCalled(LogLevel.Information, $"Cake bake time for OrderId {vanillaCakeOrder.Id} for cake type {vanillaCakeOrder.Name}.");
        }
    }
}
