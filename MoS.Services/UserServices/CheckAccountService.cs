using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.UserServices
{
    public class CheckAccountService
    {
        private readonly ICheckAccount _repository;

        public CheckAccountService(ICheckAccount repository)
        {
            _repository = repository;
        }

        public interface ICheckAccount
        {
            bool CheckIsAdmin(Credential credential);
        }

        public bool CheckIsAdmin(Credential credential)
        {
            return _repository.CheckIsAdmin(credential);
        }
    }
}
