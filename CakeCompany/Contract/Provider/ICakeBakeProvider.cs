using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface ICakeBakeProvider
    {
        Product Bake(Order order);
    }
}