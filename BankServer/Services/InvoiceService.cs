using BankServer.Generators;
using BankServer.Interfaces;
using BankServer.Repository;
using Domain.Models;
using Domain.Response;

namespace BankServer.Services
{
    public class InvoiceService : IInvoiceService
    {
        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> AddInvoice(IInvoiceRepository invoices, UserModel user, IBaseGenerator numberGenerator)
        {
            try
            {
                await invoices.Create(new InvoiceModel(user, numberGenerator.GetNextValue()));
                return new BaseResponse<IEnumerable<InvoiceModel>>(true, await invoices.GetByUser(user));
            }
            catch
            {
                return new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
            }
        }

        public async Task<BaseResponse<InvoiceModel>> UpdateInvoice(IInvoiceRepository invoices, InvoiceModel invoice)
        {
            try
            {
                var wantedInvoice = await invoices.GetById(invoice.Id);

                if (wantedInvoice is not null)
                {
                    return new BaseResponse<InvoiceModel>(true, wantedInvoice);
                }

                return new BaseResponse<InvoiceModel>(false, null);
            }
            catch
            {
                return new BaseResponse<InvoiceModel>(false, null);
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