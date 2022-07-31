using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface IOrderMutationProvider
    {
        void UpdateOrders(Order[] orders);
    }
}