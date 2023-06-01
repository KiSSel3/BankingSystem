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
        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> AddInvoice(IInvoiceRepository invoices, IUserRepository users, UserModel user, IBaseGenerator numberGenerator)
        {
            try
            {
                var dbUser = await users.GetById(user.Id);

                await invoices.Create(new InvoiceModel(dbUser, numberGenerator.GetNextValue()));
                return new BaseResponse<IEnumerable<InvoiceModel>>(true, await invoices.GetByUser(user));
            }
            catch
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> DeleteInvoice(IInvoiceRepository invoices, InvoiceModel invoice, UserModel user, IBaseGenerator generatorNumberInvoice)
        {
            try
            {
                var dbInvoice = await invoices.GetById(invoice.Id);

                generatorNumberInvoice.AddFreeItem(invoice.Number);


                await invoices.Delete(dbInvoice);
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