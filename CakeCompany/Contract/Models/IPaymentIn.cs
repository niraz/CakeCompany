namespace CakeCompany.Contract.Models
{
    public interface IPaymentIn
    {
        bool HasCreditLimit { get; }
        bool IsSuccessful { get; set; }
    }
}