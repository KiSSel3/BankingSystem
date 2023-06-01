using BankServer.Models;
using BankServer.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Interfaces
{
    public interface IInvoiceService
    {
        public Task<BaseResponse<IEnumerable<InvoiceModel>>> GetUserInvoices(IInvoiceRepository invoices, UserModel user);

        public Task<BaseResponse<IEnumerable<InvoiceModel>>> AddInvoice(IInvoiceRepository invoices, IUserRepository users, UserModel user, IBaseGenerator generatorNumberInvoice);

        public Task<BaseResponse<IEnumerable<InvoiceModel>>> DeleteInvoice(IInvoiceRepository invoices, InvoiceModel invoice, UserModel user, IBaseGenerator generatorNumberInvoice);

    }
}