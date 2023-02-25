using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.AddressServices.GetAddressService;

namespace MoS.Implementations.AddressImplementations
{
    public class GetAddressImplementation : IGetAddress
    {
        private readonly IApplicationDbContext _repository;
        public GetAddressImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public IEnumerable<Address> Get(Credential credential)
        {
            var data = _repository
                .Addresses
                .Where(address => address.UserId == credential.Id && address.IsDeleted == false)
                .Select(
                    address => new Address
                    {
                        Id = address.Id,
                        FullName = address.FullName,
                        AddressLine = address.AddressLine,
                        Longitude = address.Longitude,
                        Latitude = address.Latitude,
                        Distance = address.Distance,
                        Telephone = address.Telephone
                    }
                ).AsEnumerable();

            return data;
        }

        public Address GetById(Credential credential, Guid addressId)
        {
            var data = _repository
                        .Addresses
                        .Where(address =>
                            address.UserId.Equals(credential.Id) &&
                            address.Id.Equals(addressId) &&
                            address.IsDeleted == false)
                        .Select(
                            address => new Address
                            {
                                Id = address.Id,
                                FullName = address.FullName,
                                AddressLine = address.AddressLine,
                                Longitude = address.Longitude,
                                Latitude = address.Latitude,
                                Distance = address.Distance,
                                Telephone = address.Telephone
                            }
                        ).FirstOrDefault();

            return data;
        }
    }
}
