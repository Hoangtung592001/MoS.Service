using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.AddressServices.SetAddressService;

namespace MoS.Implementations.AddressImplementations
{
    public class SetAddressImplementation : ISetAddress
    {
        private readonly IApplicationDbContext _repository;
        public SetAddressImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<bool> Set(Credential credential, Address address)
        {
            _repository.Addresses.Add(
                    new DatabaseDefinition.Models.Address
                    {
                        Id = Guid.NewGuid(),
                        UserId = credential.Id,
                        AddressLine = address.AddressLine,
                        Longitude = address.Longitude,
                        Latitude = address.Latitude,
                        Telephone = address.Telephone,
                        FullName = address.Fullname,
                        Distance = address.Distance
                    }
                );

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
