using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankServer.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<TransactionModel>
    {
        public Task<List<TransactionModel>> GetBySender(InvoiceModel sender);
        public Task<List<TransactionModel>> GetByRecipient(InvoiceModel recipient);
    }
}
