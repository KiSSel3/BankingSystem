using BankServer.Interfaces;
using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class InvoiceService : IInvoiceService
    {
        public void AddInvoice(IRepository<InvoiceModel> invoices, InvoiceModel invoice)
        {
            invoices.Add(invoice);
        }

        public IEnumerable<InvoiceModel> GetAllUserInvoice(IRepository<InvoiceModel> invoices, UserModel user)
        {
            return invoices.GetAll().Where(item => item.InvoiceUser.Equals(user));
        }
    }
}
