using BankServer.Interfaces;
using Domain.Models;
using Domain.Response;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankServer.Services
{
    public class TransactionService : ITransactionService
    {
        public async Task<BaseResponse<IEnumerable<TransactionModel>>> History(ITransactionRepository transactions, InvoiceModel sender)
        {
            try
            {
                var history = await transactions.GetBySender(sender);

                if(history is not null)
                {
                    return new BaseResponse<IEnumerable<TransactionModel>> (true, history);
                }

                return new BaseResponse<IEnumerable<TransactionModel>> (false, null);
            }
            catch
            {
                return new BaseResponse<IEnumerable<TransactionModel>>(false, null);
            }
        }

        public async Task<BaseResponse<TransactionModel>> Transaction(ITransactionRepository transactions, IInvoiceRepository invoices, InvoiceModel sender, string numberRecipient, decimal amount)
        {
            try
            {
                var recipient = await invoices.GetByNumber(numberRecipient);
                
                if(recipient is not null)
                {
                    sender.Balanse -= amount;
                    await invoices.Update(sender);

                    recipient.Balanse += amount;
                    await invoices.Update(recipient);

                    var newTransaction = new TransactionModel(recipient, sender, amount);

                    await transactions.Create(newTransaction);

                    return new BaseResponse<TransactionModel>(true, newTransaction);
                }

                return new BaseResponse<TransactionModel>(false, null);
            }
            catch
            {
                return new BaseResponse<TransactionModel>(false, null);
            }
        }
    }
}