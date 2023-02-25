using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.CountryServices
{
    public class CountryService
    {
        private readonly ICountry _repository;

        public CountryService(ICountry repository)
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

        public interface ICountry
        {
            IEnumerable<Country> Get();
        }

        public IEnumerable<Country> Get()
        {
            return _repository.Get();
        }
    }
}
