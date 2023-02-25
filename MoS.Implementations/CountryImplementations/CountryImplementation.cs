using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.CountryServices.CountryService;

namespace MoS.Implementations.CountryImplementations
{
    public class CountryImplementation : ICountry
    {
        private readonly IApplicationDbContext _db;

        public CountryImplementation(IApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Country> Get()
        {
            var data = _db.Countries.Select(c => new Country
            {
                Id = c.Id,
                CountryCode = c.CountryCode,
                CountryName = c.CountryName,
                PhoneCode = c.PhoneCode
            });

            return data;
        }
    }
}
