using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.AddressServices
{
    public class GetAddressService
    {
        private readonly IGetAddress _repository;

        public GetAddressService(IGetAddress repository)
        {
            _repository = repository;
        }

        public class Country
        {
            public int Id { get; set; }
            public int PhoneCode { get; set; }
            public string CountryCode { get; set; }
            public string CountryName { get; set; }
        }

        public class Address
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public string AddressLine { get; set; }
            public string Telephone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public double Distance { get; set; }
            public int? CountryId { get; set; }
            public Country Country { get; set; }
        }

        public interface IGetAddress
        {
            IEnumerable<Address> Get(Credential credential);
            Address GetById(Credential credential, Guid addressId);
        }

        public IEnumerable<Address> Get(Credential credential)
        {
            return _repository.Get(credential);
        }

        public Address GetById(Credential credential, Guid addressId)
        {
            return _repository.GetById(credential, addressId);
        }
    }
}
