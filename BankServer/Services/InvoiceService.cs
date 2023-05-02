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
        public IEnumerable<InvoiceModel> GetAllUserInvoice(UserModel user, IEnumerable<InvoiceModel> allInvoices)
        {
            return allInvoices.Where(item => item.InvoiceUser == user);
        }

        public decimal GetInvoiceBalance(InvoiceModel invoice)
        {
            return invoice.Balanse;
        }

        public string GetInvoiceNumber(InvoiceModel invoice)
        {
            return invoice.Number;
        }

        public UserModel GetInvoiceUser(InvoiceModel invoice)
        {
            return invoice.InvoiceUser;
        }
    }
}
