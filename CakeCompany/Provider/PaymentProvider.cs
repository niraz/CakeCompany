using CakeCompany.Contract.Models;
using CakeCompany.Contract.Provider;
using CakeCompany.Factory;
using CakeCompany.Models;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class PaymentProvider : IPaymentProvider
{
    private readonly ILogger<PaymentProvider> _logger;
    public PaymentProvider(ILogger<PaymentProvider> logger)
    {
        _logger = logger;
    }
    public IPaymentIn Process(Order order)
    {
        _logger.LogInformation($"Processing payment for client name {order.ClientName} with order with Id {order.Id}.");
        return PaymentTypeFactory.Create(order.ClientName);
       
    }
}
/* Legacy Code
 internal class PaymentProvider
{
    public PaymentIn Process(Order order)
    {
        if (order.ClientName.Contains("Important"))
        {
            return new PaymentIn
            {
                HasCreditLimit = false,
                IsSuccessful = true
            };
        }

        return new PaymentIn
        {
            HasCreditLimit = true,
            IsSuccessful = true
        };
    }
}
*/