using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.ShippingServices.ShippingService;

namespace MoS.Implementations.ShippingImplementations
{
    public class ShippingImplementation : IShipping
    {
        private const double SHIPPING_FEE_LEVEL_1 = 0.015;
        private const double SHIPPING_FEE_LEVEL_2 = 0.01;
        private const double SHIPPING_FEE_LEVEL_3 = 0.006667;
        private const double SHIPPING_FEE_LEVEL_4 = 0.004444;
        private const double SHIPPING_DISTANCE_LEVEL_1 = 100;
        private const double SHIPPING_DISTANCE_LEVEL_2 = 200;
        private const double SHIPPING_DISTANCE_LEVEL_3 = 400;

        private readonly IApplicationDbContext _db;

        public ShippingImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public double Get(Guid addressId)
        {
            var address = _db.Addresses.Where(a => a.Id.Equals(addressId) && a.IsDeleted == false).FirstOrDefault();
            double shippingFee = 0;

            if (address == null)
            {
                return 0;
            }

            var distance = address.Distance / 1000;
            
            if (distance < 0)
            {
                return 0;
            }

            if (distance < SHIPPING_DISTANCE_LEVEL_1)
            {
                shippingFee = distance * SHIPPING_FEE_LEVEL_1;
            }
            else if (distance >= SHIPPING_DISTANCE_LEVEL_1 && distance <= SHIPPING_DISTANCE_LEVEL_2)
            {
                shippingFee = distance* SHIPPING_FEE_LEVEL_2;
            }
            else if (distance >= SHIPPING_DISTANCE_LEVEL_2 && distance <= SHIPPING_DISTANCE_LEVEL_3)
            {
                shippingFee = distance* SHIPPING_FEE_LEVEL_3;
            }
            else
            {
                shippingFee = distance* SHIPPING_FEE_LEVEL_4;
            }

            return Math.Round(shippingFee, 2);
        }
    }
}
