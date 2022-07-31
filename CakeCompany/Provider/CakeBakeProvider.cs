using CakeCompany.Contract.Provider;
using CakeCompany.Factory;
using CakeCompany.Models;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class CakeBakeProvider : ICakeBakeProvider
{
    private readonly ILogger<CakeBakeProvider> _logger;
    public CakeBakeProvider(ILogger<CakeBakeProvider> logger)
    {
        _logger = logger;
    }
    public Product Bake(Order order)
    {
        _logger.LogInformation($"Creating cake {order.Name} for order with Id {order.Id}.");
        return new()
        {
            Cake = CreateCakeFactory.Create( order.Name),
            Id = Guid.NewGuid(),
            Quantity = order.Quantity
        };
    }
    
}
/*
 * internal class CakeProvider
{
    public DateTime Check(Order order)
    {
        if (order.Name == Cake.Chocolate)
        {
            return DateTime.Now.Add(TimeSpan.FromMinutes(30));
        }

        if (order.Name == Cake.RedVelvet)
        {
            return DateTime.Now.Add(TimeSpan.FromMinutes(60));
        }

        return DateTime.Now.Add(TimeSpan.FromHours(15));
    }

    public Product Bake(Order order)
    {
        if (order.Name == Cake.Chocolate)
        {
            return new()
            {
                Cake = Cake.Chocolate,
                Id = new Guid(),
                Quantity = order.Quantity
            };
        }

        if (order.Name == Cake.RedVelvet)
        {
            return new()
            {
                Cake = Cake.RedVelvet,
                Id = new Guid(),
                Quantity = order.Quantity
            };
        }

        return new()
        {
            Cake = Cake.Vanilla,
            Id = new Guid(),
            Quantity = order.Quantity
        }; ;
    }
}
 */