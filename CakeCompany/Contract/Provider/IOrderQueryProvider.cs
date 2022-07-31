using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface IOrderQueryProvider
    {
        Order[] GetLatestOrders();
    }
}