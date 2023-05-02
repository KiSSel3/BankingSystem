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
        public IEnumerable<InvoiceModel> GetAllUserInvoice(UserModel user, IEnumerable<InvoiceModel> allInvoices);
        public string GetInvoiceNumber(InvoiceModel invoice);
        public decimal GetInvoiceBalance(InvoiceModel invoice);
        public UserModel GetInvoiceUser(InvoiceModel invoice);

    }
}
