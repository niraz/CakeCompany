using CakeCompany.Contract.Models;
using CakeCompany.Models.Enum;
using CakeCompany.Models.Payment;

namespace CakeCompany.Factory
{
    public static class PaymentTypeFactory
    {
        private static Lazy<Dictionary<PaymentType, IPaymentIn>> paymentTypes;

        static PaymentTypeFactory()
        {
            paymentTypes = new Lazy<Dictionary<PaymentType, IPaymentIn>>(() => LoadPaymentTypes());
        }

        private static Dictionary<PaymentType, IPaymentIn> LoadPaymentTypes()
        {
            Dictionary<PaymentType, IPaymentIn> paymentTypeDictionary = new()
            {
                { PaymentType.Ordinary, new PaymentIn() },
                { PaymentType.Important, new ImportantPaymentIn() },
            };

            return paymentTypeDictionary;
        }

        public static IPaymentIn Create(string clientName)
        {
            var paymentType = GetPaymentType(clientName);
            return paymentTypes.Value[paymentType];
        }
        private static PaymentType GetPaymentType(string clientName)
        {
            return clientName.Contains("Important") ? PaymentType.Important : PaymentType.Ordinary;
        }
    }
}
