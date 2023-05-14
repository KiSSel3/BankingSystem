using BankServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankServer.Interfaces
{
    public interface ITransactionService
    {
        public TransactionModel Transaction(ulong id, InvoiceModel recipient, InvoiceModel sender, decimal amount);
        public bool IsInvoiceExist(IRepository<InvoiceModel> invoices, string number);
        public InvoiceModel GetInvoice(IRepository<InvoiceModel> invoices, string number);
    }
}
