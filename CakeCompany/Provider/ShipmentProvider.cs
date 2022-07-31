using CakeCompany.Contract.Provider;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class ShipmentProvider : IShipmentProvider
{
    private readonly ILogger<ShipmentProvider> _logger;
    private readonly IOrderQueryProvider _orderQueryProvider;
    private readonly ICakeEstimatedBakeTimeProvider _cakeEstimatedBakeTimeProvider;
    private readonly ICakeBakeProvider _cakeBakeProvider;
    private readonly IPaymentProvider _paymentProvider;
    private readonly ITransportProvider _transportProvider;
    public ShipmentProvider(ILogger<ShipmentProvider> logger,
        IOrderQueryProvider orderQueryProvider,
        ICakeEstimatedBakeTimeProvider cakeEstimatedBakeTimeProvider,
        ICakeBakeProvider cakeBakeProvider,
        IPaymentProvider paymentProvider,
        ITransportProvider transportProvider
        )
    {
        _logger = logger;
        _orderQueryProvider = orderQueryProvider;
        _cakeEstimatedBakeTimeProvider = cakeEstimatedBakeTimeProvider;
        _cakeBakeProvider = cakeBakeProvider;
        _paymentProvider = paymentProvider;
        _transportProvider = transportProvider;
    }
    public void GetShipment()
    {
        try
        {
            var orders = _orderQueryProvider.GetLatestOrders();
            if (!orders.Any())
            {
                _logger.LogInformation("GetLatestOrders of OrderQueryProvider return empty result.");
                return;
            }
            var products = orders.Where(x => _cakeEstimatedBakeTimeProvider.GetEstimatedBakeTime(x) <= x.EstimatedDeliveryTime
                                        && _paymentProvider.Process(x).IsSuccessful)
                             .Select(x => _cakeBakeProvider.Bake(x)).ToList();
            if (!products.Any())
            {
                _logger.LogInformation("There is no cake to be deliverd.");
                return;
            }
            var deliveryMedium = _transportProvider.CheckForAvailability(products);
            deliveryMedium.Deliver(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, $"Exception occured during the processing of GetShipment method");
            throw;
        }
    }
}
/* Legacy code 
 * internal class ShipmentProvider
{
    public void GetShipment()
    {
        //Call order to get new orders
        var orderProvider = new OrderProvider();

        var orders = orderProvider.GetLatestOrders();

        if (!orders.Any())
        {
            return;
        }

        var cancelledOrders = new List<Order>();
        var products = new List<Product>();

        foreach (var order in orders)
        {
            var cakeProvider = new CakeProvider();

            var estimatedBakeTime = cakeProvider.Check(order);

            if (estimatedBakeTime > order.EstimatedDeliveryTime)
            {
                cancelledOrders.Add(order);
                continue;
            }

            var payment = new PaymentProvider();

            if (!payment.Process(order).IsSuccessful)
            {
                cancelledOrders.Add(order);
                continue;
            }

            var product = cakeProvider.Bake(order);
            products.Add(product);
        }
            
        var transportProvider = new TransportProvider();

        var transport = transportProvider.CheckForAvailability(products);

        if (transport == "Van")
        {
            var van = new Van();
            van.Deliver(products);
        }

        if (transport == "Truck")
        {
            var truck = new Truck();
            truck.Deliver(products);
        }

        if (transport == "Ship")
        {
            var ship = new Ship();
            ship.Deliver(products);
        }
    }
}*/