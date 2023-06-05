using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Interfaces
{
    public interface IInvoiceService
    {
        public Task<BaseResponse<IEnumerable<InvoiceModel>>> CreateInvoice(UserModel user, string ipAdress, int port);
        public Task<BaseResponse<IEnumerable<InvoiceModel>>> DeleteInvoice(UserModel user, InvoiceModel invoice, string ipAdress, int port);
        public Task<BaseResponse<IEnumerable<InvoiceModel>>> GetInvoices(UserModel user, string ipAdress, int port);
        public Task<BaseResponse<InvoiceModel>> UpdateInvoice(UserModel user, InvoiceModel invoice,  string ipAdress, int port);
    }
}
