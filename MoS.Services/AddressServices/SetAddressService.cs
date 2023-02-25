using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.AddressServices
{
    public class SetAddressService
    {
        private readonly ISetAddress _repository;

        public SetAddressService(ISetAddress repository)
        {
            _repository = repository;
        }

        public class Address
        {
            public string Fullname { get; set; }
            public string AddressLine { get; set; }
            public string Telephone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public double Distance { get; set; }
        }

        public interface ISetAddress
        {
            Task Set(Credential credential, Address address, Action<Guid> onSuccess, Action onFail);
        }

        public async Task Set(Credential credential, Address address, Action<Guid> onSuccess, Action onFail)
        {
            await _repository.Set(credential, address, onSuccess, onFail);
        }
    }
}
