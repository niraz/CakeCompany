using CakeCompany.Contract.Models;
using CakeCompany.Contract.Provider;
using CakeCompany.Factory;
using CakeCompany.Models;
using CakeCompany.Models.Transport;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class TransportProvider : ITransportProvider
{
    private readonly ILogger<TransportProvider> _logger;
    public TransportProvider(ILogger<TransportProvider> logger)
    {
        _logger = logger;
    }
    public IDeliveryMedium CheckForAvailability(List<Product> products)
    {
        _logger.LogInformation($"Sum of quantity is {products.Sum(p => p.Quantity)}.");
        return TransportVehicleFactory.Create(products.Sum(p => p.Quantity));
    }
}
/*
Legacy Code
internal class TransportProvider
{
    public string CheckForAvailability(List<Product> products)
    {
        if (products.Sum(p => p.Quantity) < 1000)
        {
            return "Van";
        }

        if (products.Sum(p => p.Quantity) > 1000 && products.Sum(p => p.Quantity) < 5000)
        {
            return "Truck";
        }

        return "Ship";
    }
}
 */