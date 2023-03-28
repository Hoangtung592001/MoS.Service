using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.Role;
using static MoS.Services.UserServices.CheckAccountService;

namespace MoS.Implementations.AuthorImplementations
{
    public class CheckAccountImplementation : ICheckAccount
    {
        private readonly IApplicationDbContext _db;

        public CheckAccountImplementation(IApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckIsAdmin(Credential credential)
        {
            var user = _db.Users.Where(u => u.Id.Equals(credential.Id) && u.IsDeleted == false).FirstOrDefault();

            return user.RoleId == (int) RoleIDs.Admin;
        }
    }
}
