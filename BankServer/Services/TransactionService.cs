using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankServer.Services
{
    public class TransactionService : ITransactionService
    {
        public InvoiceModel GetInvoice(IRepository<InvoiceModel> invoices, string number)
        {
            throw new NotImplementedException();
        }

        public bool IsInvoiceExist(IRepository<InvoiceModel> invoices, string number)
        {
            return invoices.GetAll().Any(item => item.Number == number);
        }

        public TransactionModel Transaction(ulong id, InvoiceModel recipient, InvoiceModel sender, decimal amount)
        {
            recipient.Balanse -= amount;
            sender.Balanse += amount;

            return new TransactionModel(id, recipient, sender, amount);
        }
    }
}
