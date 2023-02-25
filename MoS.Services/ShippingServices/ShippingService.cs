using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.ShippingServices
{
    public class ShippingService
    {
        private readonly IShipping _repository;

        public ShippingService(IShipping repository)
        {
            _repository = repository;
        }

        public class ShippingRequest
        {
            public double Distance { get; set; }
        }

        public interface IShipping
        {
            double Get(double distance);
        }

        public double Get(double distance)
        {
            return _repository.Get(distance);
        }
    }
}
