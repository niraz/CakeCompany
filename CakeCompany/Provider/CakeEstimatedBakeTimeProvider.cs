using CakeCompany.Contract.Provider;
using CakeCompany.Factory;
using CakeCompany.Models;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class CakeEstimatedBakeTimeProvider : ICakeEstimatedBakeTimeProvider
{
    private readonly ILogger<CakeEstimatedBakeTimeProvider> _logger;
    public CakeEstimatedBakeTimeProvider(ILogger<CakeEstimatedBakeTimeProvider> logger)
    {
        _logger = logger;
    }
    public DateTime GetEstimatedBakeTime(Order order)
    {
        _logger.LogInformation($"Cake bake time for OrderId {order.Id} for cake type {order.Name}.");
        return CheckCakeEstimatedBakeTimeFactory.GetCakeEstimatedBakeTime(order.Name);
    }

}
/*
 internal class CakeProvider
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
 **/