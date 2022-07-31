using CakeCompany.Contract.Models;
using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface ITransportProvider
    {
        IDeliveryMedium CheckForAvailability(List<Product> products);
    }
}