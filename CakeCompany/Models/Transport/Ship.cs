using CakeCompany.Contract.Models;

namespace CakeCompany.Models.Transport;

public class Ship: DeliveryMediumBase
{
    // Override the default behaviour if required
    public override bool Deliver(List<Product> products)
    {
        // Write business logic for Deliver by Ship
        return true;
    }
}