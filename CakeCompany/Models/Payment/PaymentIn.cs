using CakeCompany.Contract.Models;

namespace CakeCompany.Models.Payment;

public class PaymentIn : IPaymentIn
{
    public bool IsSuccessful { get; set; }
    public bool HasCreditLimit => true;
}
