using CakeCompany.Contract.Models;

namespace CakeCompany.Models.Payment;

public class ImportantPaymentIn : IPaymentIn
{
    public bool IsSuccessful { get; set; }
    public bool HasCreditLimit => false;
}
