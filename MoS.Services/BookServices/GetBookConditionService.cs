using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BookServices
{
    public class GetBookConditionService
    {
        private readonly IGetBookCondition _repository;

        public GetBookConditionService(IGetBookCondition repository)
        {
            _repository = repository;
        }

        public class BookCondition
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public interface IGetBookCondition
        {
            IEnumerable<BookCondition> GetAll();
        }

        public IEnumerable<BookCondition> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
