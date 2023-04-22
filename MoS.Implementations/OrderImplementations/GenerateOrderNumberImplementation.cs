using MoS.DatabaseDefinition.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.OrderServices.GenerateOrderNumberService;

namespace MoS.Implementations.OrderImplementations
{
    public class GenerateOrderNumberImplementation : IGenerateOrderNumber
    {
        private readonly IApplicationDbContext _db;
        private const string prefix = "ABE";
        public GenerateOrderNumberImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public string Generate()
        {
            var orderNumber = _db.Orders.Count() + 1;

            var orderNumberString = orderNumber.ToString();

            while(orderNumberString.Length < 6)
            {
                orderNumberString = '0' + orderNumberString;
            }

            return prefix + orderNumberString;
        }
    }
}
