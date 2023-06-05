using BankServer.Interfaces;
using Domain.Models;
using Domain.Response;

namespace BankServer.Services
{
    public class TransactionService : ITransactionService
    {
        public async Task<BaseResponse<IEnumerable<TransactionModel>>> HistoryBySender(ITransactionRepository transactions, InvoiceModel sender)
        {
            try
            {
                return new BaseResponse<IEnumerable<TransactionModel>>(true, await transactions.GetBySender(sender));
            }
            catch
            {
                return new BaseResponse<IEnumerable<TransactionModel>>(false, null);
            }
        }

        public async Task<BaseResponse<IEnumerable<TransactionModel>>> HistoryByUser(ITransactionRepository transactions, UserModel user)
        {
            try
            {
                return new BaseResponse<IEnumerable<TransactionModel>>(true, await transactions.GetByUser(user));
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

                if (recipient is not null)
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