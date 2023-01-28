using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.ExceptionServices
{
    public class ExceptionService
    {
        private readonly IException _repository;

        public ExceptionService(IException repository)
        {
            _repository = repository;
        }

        public class Exception
        {
            public Guid Id { get; set; }
            public string ExceptionMessageType { get; set; }
            public string Description { get; set; }
        }

        public interface IException {
            Exception Get(Guid exceptionId);
        }

        public Exception Get(Guid exceptionId)
        {
            return _repository.Get(exceptionId);
        }
    }
}
