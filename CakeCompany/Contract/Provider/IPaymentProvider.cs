using CakeCompany.Contract.Models;
using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface IPaymentProvider
    {
        IPaymentIn Process(Order order);
    }
}