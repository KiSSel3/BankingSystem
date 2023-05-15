using BankServer.Interfaces;
using BankServer.Models;
using BankServer.Response;
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
        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> AddInvoice(IInvoiceRepository invoices, UserModel user, IGeneratorNumberInvoice generatorNumberInvoice, IGeneratorId generatorId)
        {
            try
            {
                await invoices.Create(new InvoiceModel(user, generatorId.Next(), generatorNumberInvoice.Next()));
                return new BaseResponse<IEnumerable<InvoiceModel>>(true, await invoices.GetByUser(user));
            }
            catch
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> DeleteInvoice(IInvoiceRepository invoices, InvoiceModel invoice, UserModel user)
        {
            try
            {
                await invoices.Delete(invoice);
                return new BaseResponse<IEnumerable<InvoiceModel>>(true, await invoices.GetByUser(user));
            }
            catch
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> GetUserInvoices(IInvoiceRepository invoices, UserModel user)
        {
            try
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(true, await invoices.GetByUser(user));
            }
            catch
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
            }
        }
    }
}
