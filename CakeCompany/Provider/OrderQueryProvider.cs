using CakeCompany.Contract.Provider;
using CakeCompany.Models;
using CakeCompany.Models.Enum;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class OrderQueryProvider : IOrderQueryProvider
{
    private readonly ILogger<OrderQueryProvider> _logger;
    public OrderQueryProvider(ILogger<OrderQueryProvider> logger)
    {
        _logger = logger;
    }
    public Order[] GetLatestOrders()
    {
        _logger.LogInformation($"Starting to execute GetLatestOrders.");
        return new Order[]
        {
            new("CakeBox", DateTime.Now, 1, Cake.Chocolate, 120.25),
            new("ImportantCakeShop", DateTime.Now, 2, Cake.RedVelvet, 120.25)
        };
    }

}


