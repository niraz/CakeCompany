// See https://aka.ms/new-console-template for more information

using CakeCompany.Contract.Provider;
using CakeCompany.Provider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
//setup our DI
var serviceProvider = new ServiceCollection()
    .AddLogging()
    .AddSingleton<IShipmentProvider, ShipmentProvider>()
    .AddSingleton<ICakeBakeProvider, CakeBakeProvider>()
    .AddSingleton<ICakeEstimatedBakeTimeProvider, CakeEstimatedBakeTimeProvider>()
    .AddSingleton<IPaymentProvider, PaymentProvider>()
    .AddSingleton<ITransportProvider, TransportProvider>()
    .AddSingleton<IOrderQueryProvider, OrderQueryProvider>()
    .BuildServiceProvider();


var logger = serviceProvider.GetService<ILoggerFactory>()
    .CreateLogger<Program>();
logger.LogDebug("Starting application");

var shipmentProvider = serviceProvider.GetService<IShipmentProvider>();
shipmentProvider.GetShipment();

Console.WriteLine("Shipment Details...");
