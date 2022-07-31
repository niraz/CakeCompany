using CakeCompany.Models;

namespace CakeCompany.Contract.Provider
{
    public interface ICakeEstimatedBakeTimeProvider
    {
        DateTime GetEstimatedBakeTime(Order order);
    }
}