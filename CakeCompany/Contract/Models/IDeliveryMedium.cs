using CakeCompany.Models;

namespace CakeCompany.Contract.Models
{
    public interface IDeliveryMedium
    {
        bool Deliver(List<Product> products);
    }
}
