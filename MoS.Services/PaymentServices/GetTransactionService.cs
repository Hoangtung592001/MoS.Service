using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.PaymentServices
{
    public class GetTransactionService
    {
        private readonly IGetTransaction _repository;

        public class PaymentOption
        {
            public Guid Id { get; set; }
            public string CardNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string NameOnCard { get; set; }
            public int PaymentOptionTypeDescriptionId { get; set; }
            //public PaymentOptionTypeDescription PaymentOptionTypeDescription { get; set; }
        }

        public class TransactionTypeDescription
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Transaction
        {
            public Guid Id { get; set; }
            public Guid PaymentOptionId { get; set; }
            public int TransactionTypeDescriptionId { get; set; }
            public double Amount { get; set; }
            public PaymentOption PaymentOption { get; set; }
            public TransactionTypeDescription TransactionTypeDescription { get; set; }
        }

        public interface IGetTransaction
        {
            IEnumerable<Transaction> Get(Credential credential);
        }

        public IEnumerable<Transaction> Get(Credential credential)
        {
            return _repository.Get(credential);
        }
    }
}
