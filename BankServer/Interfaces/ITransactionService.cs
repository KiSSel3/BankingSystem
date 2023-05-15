using BankServer.Models;
using BankServer.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BankServer.Interfaces
{
    public interface ITransactionService
    {
        public Task<BaseResponse<TransactionModel>> Transaction(ITransactionRepository transactions, IInvoiceRepository invoices, InvoiceModel sender, string numberRecipient, decimal amount, IGeneratorId generatorId);
    }
}