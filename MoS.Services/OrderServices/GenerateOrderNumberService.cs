using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.OrderServices
{
    public class GenerateOrderNumberService
    {
        private readonly IGenerateOrderNumber _repository;

        public GenerateOrderNumberService(IGenerateOrderNumber repository)
        {
            _repository = repository;
        }

        public interface IGenerateOrderNumber
        {
            string Generate();
        }

        public string Generate()
        {
            return _repository.Generate();
        }
    }
}
