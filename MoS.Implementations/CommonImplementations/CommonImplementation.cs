using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoS.DatabaseDefinition.Contexts;
using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MoS.Services.CommonServices.CommonService;

namespace MoS.Implementations.CommonImplementations
{
    public class CommonImplementation : ICommon
    {
        private readonly IApplicationDbContext _repository;

        public CommonImplementation()
        {}

        public CommonImplementation(IApplicationDbContext repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckAuthorExist(Guid authorId)
        {
            var author = (await _repository.Authors.Where(a => a.Id.Equals(authorId)).ToListAsync()).FirstOrDefault();

            if (author != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckPublisherExist(Guid publisherId)
        {
            var publisher = (await _repository.Publishers.Where(a => a.Id.Equals(publisherId)).ToListAsync()).FirstOrDefault();

            if (publisher != null)
            {
                return true;
            }

            return false;
        }

        public Credential GetCredential(ClaimsPrincipal User)
        {
            var userId = new Guid(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            var credential = new Credential
            {
                Id = userId
            };

            return credential;
        }

        public decimal SellOfPrice(double originalPrice, double sellOffRate)
        {
            return Convert.ToDecimal(originalPrice * (100 - sellOffRate) / 100);
        }

        public void DeleteItem(dynamic item, Guid deletedBy)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            item.DeletedBy = deletedBy;
        }

        public string GetFileName(string fileName, string userId)
        {
            return userId + "-" + Guid.NewGuid() + "-" + fileName;
        }
    }
}
