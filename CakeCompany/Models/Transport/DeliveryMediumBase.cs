using CakeCompany.Contract.Models;

namespace CakeCompany.Models.Transport
{
    public abstract class DeliveryMediumBase : IDeliveryMedium
    {
        public virtual bool Deliver(List<Product> products)
        {
            return true;
        }
    }
}
