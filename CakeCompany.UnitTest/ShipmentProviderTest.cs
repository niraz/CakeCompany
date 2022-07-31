using CakeCompany.Provider;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using CakeCompany.Models;
using CakeCompany.Models.Cakes;
using CakeCompany.Models.Transport;
using CakeCompany.Contract.Provider;
using CakeCompany.Models.Payment;
using CakeCompany.Models.Enum;

namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class  ShipmentProviderTest
    {
        private IShipmentProvider shipmentProvider;
        private  Mock<ILogger<ShipmentProvider>> mockLogger;
        private  Mock<IOrderQueryProvider> mockOrderQueryProvider;
        private  Mock<ICakeEstimatedBakeTimeProvider> mockCakeEstimatedBakeTimeProvider;
        private  Mock<ICakeBakeProvider> mockCakeBakeProvider;
        private  Mock<IPaymentProvider> mockPaymentProvider;
        private  Mock<ITransportProvider> mockTransportProvider;
        private DateTime chocolateCakeOrderDeliveryCheckTime;
        private DateTime redVelvetCakeOrderDeliveryCheckTime;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<ShipmentProvider>>();
            mockOrderQueryProvider = new();
            mockCakeEstimatedBakeTimeProvider = new();
            mockCakeBakeProvider = new();
            mockPaymentProvider = new();
            mockTransportProvider = new();
            chocolateCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(30);
            redVelvetCakeOrderDeliveryCheckTime = DateTime.Now.AddMinutes(60);
        }

        [Test]
        public void GetShipment_DoesNot_Execute_CheckDeliveryTime_When_OrderQueryProvider_Return_EmptyArray()
        {
            //Arrange
            mockOrderQueryProvider.Setup(x => x.GetLatestOrders()).Returns(Array.Empty<Order>);
            shipmentProvider = new ShipmentProvider(mockLogger.Object,
               mockOrderQueryProvider.Object,
               mockCakeEstimatedBakeTimeProvider.Object,
               mockCakeBakeProvider.Object,
               mockPaymentProvider.Object,
               mockTransportProvider.Object
               );
            //Act
            shipmentProvider.GetShipment();
            //Assert
            mockOrderQueryProvider.Verify(x => x.GetLatestOrders(), Times.Once);
            mockCakeEstimatedBakeTimeProvider.Verify(x => x.GetEstimatedBakeTime(It.IsAny<Order>()), Times.Never);
            mockCakeBakeProvider.Verify(x => x.Bake(It.IsAny<Order>()), Times.Never);
            mockPaymentProvider.Verify(x => x.Process(It.IsAny<Order>()), Times.Never);
            mockTransportProvider.Verify(x => x.CheckForAvailability(It.IsAny<List<Product>>()), Times.Never);
            mockLogger.VerifyLogWasCalled(LogLevel.Information, "GetLatestOrders of OrderQueryProvider return empty result.");
        }
        [Test]
        public void GetShipment_Deliver_AllCakeOrders_Successfully_When_Payment_IsSuccessful()
        {
            //Arrange
            Order chocolateCakeOrder = new("CakeBox", chocolateCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.Chocolate, 100.25);
            Order redVelvetCakeOrder = new("ImportantCakeShop", redVelvetCakeOrderDeliveryCheckTime.AddMinutes(1), 1, Cake.RedVelvet, 220.25);
            PaymentIn paymentOfChocolateCake = new() { IsSuccessful = true };
            ImportantPaymentIn paymentOfRedVelvetCake = new() { IsSuccessful = true };
            Product chocolateCakeProduct = new()
            {
                Cake = new ChocolateCake(),
                Id = Guid.NewGuid(),
                Quantity = chocolateCakeOrder.Quantity
            };
            Product redVelvetCakeProduct = new()
            {
                Cake = new RedVelvetCake(),
                Id = Guid.NewGuid(),
                Quantity = redVelvetCakeOrder.Quantity
            };
            var fakeOrder = new Order[]
        {
            chocolateCakeOrder,
            redVelvetCakeOrder
        };
            mockOrderQueryProvider.Setup(x => x.GetLatestOrders()).Returns(fakeOrder);
            mockCakeEstimatedBakeTimeProvider.Setup(x => x.GetEstimatedBakeTime(chocolateCakeOrder)).Returns(chocolateCakeOrderDeliveryCheckTime);
            mockCakeEstimatedBakeTimeProvider.Setup(x => x.GetEstimatedBakeTime(redVelvetCakeOrder)).Returns(redVelvetCakeOrderDeliveryCheckTime);
            mockPaymentProvider.Setup(x => x.Process(chocolateCakeOrder)).Returns(paymentOfChocolateCake);
            mockPaymentProvider.Setup(x => x.Process(redVelvetCakeOrder)).Returns(paymentOfRedVelvetCake);
            mockCakeBakeProvider.Setup(x => x.Bake(chocolateCakeOrder)).Returns(chocolateCakeProduct);
            mockCakeBakeProvider.Setup(x => x.Bake(redVelvetCakeOrder)).Returns(redVelvetCakeProduct);
            mockTransportProvider.Setup(x => x.CheckForAvailability(new List<Product> { chocolateCakeProduct, redVelvetCakeProduct })).Returns(new Ship());
            shipmentProvider = new ShipmentProvider(mockLogger.Object,
               mockOrderQueryProvider.Object,
               mockCakeEstimatedBakeTimeProvider.Object,
               mockCakeBakeProvider.Object,
               mockPaymentProvider.Object,
               mockTransportProvider.Object
               );
            //Act
            shipmentProvider.GetShipment();
            //Assert
            mockCakeEstimatedBakeTimeProvider.Verify(x => x.GetEstimatedBakeTime(chocolateCakeOrder), Times.Exactly(1));
            mockCakeEstimatedBakeTimeProvider.Verify(x => x.GetEstimatedBakeTime(redVelvetCakeOrder), Times.Exactly(1));
            mockPaymentProvider.Verify(x => x.Process(chocolateCakeOrder), Times.Exactly(1));
            mockPaymentProvider.Verify(x => x.Process(redVelvetCakeOrder), Times.Exactly(1));
            mockCakeBakeProvider.Verify(x => x.Bake(chocolateCakeOrder), Times.Exactly(1));
            mockCakeBakeProvider.Verify(x => x.Bake(redVelvetCakeOrder), Times.Exactly(1));
            mockTransportProvider.Verify(x => x.CheckForAvailability(new List<Product> { chocolateCakeProduct,redVelvetCakeProduct }), Times.Exactly(1));
           
        }

        [Test]
        public void GetShipment_DoesNot_Bake_CakeOrder_When_Payment_Unsuccessful()
        {
            //Arrange

            Order redVelvetCakeOrder = new("ImportantCakeShop", redVelvetCakeOrderDeliveryCheckTime.AddMinutes(2), 1, Cake.RedVelvet, 220.25);
            ImportantPaymentIn paymentOfRedVelvetCakeUnsuccessful = new() { IsSuccessful = false };
            Product redVelvetCakeProduct = new()
            {
                Cake = new RedVelvetCake(),
                Id = new Guid(),
                Quantity = redVelvetCakeOrder.Quantity
            };
            var fakeOrder = new Order[]
        {
            redVelvetCakeOrder
        };
            mockOrderQueryProvider.Setup(x => x.GetLatestOrders()).Returns(fakeOrder);
            mockCakeEstimatedBakeTimeProvider.Setup(x => x.GetEstimatedBakeTime(redVelvetCakeOrder)).Returns(redVelvetCakeOrderDeliveryCheckTime);
            mockPaymentProvider.Setup(x => x.Process(redVelvetCakeOrder)).Returns(paymentOfRedVelvetCakeUnsuccessful);
            shipmentProvider = new ShipmentProvider(mockLogger.Object,
               mockOrderQueryProvider.Object,
               mockCakeEstimatedBakeTimeProvider.Object,
               mockCakeBakeProvider.Object,
               mockPaymentProvider.Object,
               mockTransportProvider.Object
               );
            //Act
            shipmentProvider.GetShipment();
            //Assert
            mockCakeEstimatedBakeTimeProvider.Verify(x => x.GetEstimatedBakeTime(redVelvetCakeOrder), Times.Exactly(1));
            mockPaymentProvider.Verify(x => x.Process(redVelvetCakeOrder), Times.Exactly(1));
            mockCakeBakeProvider.Verify(x => x.Bake(redVelvetCakeOrder), Times.Never);
            mockLogger.VerifyLogWasCalled(LogLevel.Information, "There is no cake to be deliverd.");
        }

        [Test]
        public void GetShipment_DoesNot_ProcessPayment_When_EstimatedDeliveryTime_IsLessThan_DeliveryCheckTime()
        {
            //Arrange

            Order redVelvetCakeOrder = new("ImportantCakeShop", redVelvetCakeOrderDeliveryCheckTime.AddMinutes(-1), 1, Cake.RedVelvet, 220.25);
           
            var sampleOrder = new Order[]
        {
            redVelvetCakeOrder
        };
            mockOrderQueryProvider.Setup(x => x.GetLatestOrders()).Returns(sampleOrder);
            mockCakeEstimatedBakeTimeProvider.Setup(x => x.GetEstimatedBakeTime(redVelvetCakeOrder)).Returns(redVelvetCakeOrderDeliveryCheckTime);
            shipmentProvider = new ShipmentProvider(mockLogger.Object,
               mockOrderQueryProvider.Object,
               mockCakeEstimatedBakeTimeProvider.Object,
               mockCakeBakeProvider.Object,
               mockPaymentProvider.Object,
               mockTransportProvider.Object
               );
            //Act
            shipmentProvider.GetShipment();
            //Assert
            mockCakeEstimatedBakeTimeProvider.Verify(x => x.GetEstimatedBakeTime(It.IsAny<Order>()), Times.Exactly(1));
            mockPaymentProvider.Verify(x => x.Process(redVelvetCakeOrder), Times.Never);
            mockCakeBakeProvider.Verify(x => x.Bake(redVelvetCakeOrder), Times.Never);
            mockLogger.VerifyLogWasCalled(LogLevel.Information, "There is no cake to be deliverd.");
        }
    }
}
