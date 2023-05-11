using BankServer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IInvoiceService
    {
        public IEnumerable<InvoiceModel> GetAllUserInvoice(IRepository<InvoiceModel> invoices, UserModel user);

        public void AddInvoice(IRepository<InvoiceModel> invoices, InvoiceModel invoice);

    }
}
