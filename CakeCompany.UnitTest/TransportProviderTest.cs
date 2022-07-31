using CakeCompany.Models;
using CakeCompany.Models.Cakes;
using CakeCompany.Models.Transport;
using CakeCompany.Provider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class TransportProviderTest
    {
        private Mock<ILogger<TransportProvider>> mockLogger;
        private TransportProvider transportProvider;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<TransportProvider>>();
        }
        [Test]
        public void CheckForAvailability_return_VAN_When_Quantity_IsLessThan_1000()
        {
            //Arrange
            Product chocolateCakeProduct = new()
            {
                Cake = new ChocolateCake(),
                Id = Guid.NewGuid(),
                Quantity = 998
            };
            Product redVelvetCakeProduct = new()
            {
                Cake = new RedVelvetCake(),
                Id = Guid.NewGuid(),
                Quantity =  1
            };
            var productList = new List<Product>() { chocolateCakeProduct , redVelvetCakeProduct };
            transportProvider = new TransportProvider(mockLogger.Object);
            //Act
            var result = transportProvider.CheckForAvailability(productList);
            //Assert

            Assert.True(result.GetType() == typeof(Van));
        }
        [Test]
        public void CheckForAvailability_return_TRUCK_When_Quantity_IsBetween_1000_And_5000()
        {
            //Arrange
            Product chocolateCakeProduct = new()
            {
                Cake = new ChocolateCake(),
                Id = Guid.NewGuid(),
                Quantity = 4998
            };
            Product redVelvetCakeProduct = new()
            {
                Cake = new RedVelvetCake(),
                Id = Guid.NewGuid(),
                Quantity = 1
            };
            var productList = new List<Product>() { chocolateCakeProduct, redVelvetCakeProduct };
            transportProvider = new TransportProvider(mockLogger.Object);
            //Act
            var result = transportProvider.CheckForAvailability(productList);
            //Assert

            Assert.True(result.GetType() == typeof(Truck));
        }
        [Test]
        public void CheckForAvailability_return_SHIP_When_Quantity_IsMoreThanOrEqualTo_5000()
        {
            //Arrange
            Product chocolateCakeProduct = new()
            {
                Cake = new ChocolateCake(),
                Id = Guid.NewGuid(),
                Quantity = 4999
            };
            Product redVelvetCakeProduct = new()
            {
                Cake = new RedVelvetCake(),
                Id = Guid.NewGuid(),
                Quantity = 5000
            };
            var productList = new List<Product>() { chocolateCakeProduct, redVelvetCakeProduct };
            transportProvider = new TransportProvider(mockLogger.Object);
            //Act
            var result = transportProvider.CheckForAvailability(productList);
            //Assert

            Assert.True(result.GetType() == typeof(Ship));
        }
    }
}
