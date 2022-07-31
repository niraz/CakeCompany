using CakeCompany.Contract.Models;
using CakeCompany.Models.Enum;
using CakeCompany.Models.Transport;

namespace CakeCompany.Factory
{
    public static class TransportVehicleFactory
    {
        private static Lazy<Dictionary<TransportationCapacity, IDeliveryMedium>> transportationVehicleTypes;

        static TransportVehicleFactory()
        {
            transportationVehicleTypes = new Lazy<Dictionary<TransportationCapacity, IDeliveryMedium>>(() => LoadTransportationVehicle());
        }

        private static Dictionary<TransportationCapacity, IDeliveryMedium> LoadTransportationVehicle()
        {
            Dictionary<TransportationCapacity, IDeliveryMedium> transportationVehicleDictionary = new()
            {
                { TransportationCapacity.LessThanThounsand, new Van() },
                { TransportationCapacity.LessThanFiveThousand, new Truck() },
                { TransportationCapacity.MoreThanOrEqualToFiveThousand, new Ship() },
            };

            return transportationVehicleDictionary;
        }

        public static IDeliveryMedium Create(double quantity)
        {
            var transportationCapacity = GetTransportationCapacity(quantity);
            return transportationVehicleTypes.Value[transportationCapacity];
        }
        private static TransportationCapacity GetTransportationCapacity(double quantity)
        {
            if (quantity < 1000)
            {
                return TransportationCapacity.LessThanThounsand;
            }
            if (quantity < 5000)
            {
                return TransportationCapacity.LessThanFiveThousand;
            }
            return TransportationCapacity.MoreThanOrEqualToFiveThousand;
        }
    }
}
