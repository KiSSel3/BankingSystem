using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Interfaces
{
    public interface ITransactionService
    {
        public Task<BaseResponse<TransactionModel>> CreateTransaction(InvoiceModel invoice, string number, decimal amount, string ipAdress, int port);
        public Task<BaseResponse<IEnumerable<TransactionModel>>> GetTransactionsByInvoice(InvoiceModel invoice, string ipAdress, int port);
        public Task<BaseResponse<IEnumerable<TransactionModel>>> GetTransactionsByUser(UserModel user, string ipAdress, int port);
    }
}
