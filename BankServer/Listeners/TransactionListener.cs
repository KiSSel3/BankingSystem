using BankSerializer;
using BankServer.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Domain.Request;
using Domain.Response;
using System.Net.Sockets;


namespace BankServer.Listeners
{
    public class TransactionListener : BaseListener
    {
        private ITransactionRepository transactions;
        private IInvoiceRepository invoices;
        private ITransactionService transactionService;

        public Serializer bankSerializer;

        public TransactionListener(int port, IEncoderService encoderService, ITransactionRepository _transactions, IInvoiceRepository _invoices, ITransactionService _transactionService) : base(port, encoderService)
        {
            transactions = _transactions;
            invoices = _invoices;
            transactionService = _transactionService;

            bankSerializer = new();
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                BaseResponse<TransactionModel> response;

                stream = client.GetStream();

                try
                {
                    var request = bankSerializer.DeSerializeXML<BaseRequest<InvoiceModel>>(GetRequest());
                    request.Data = await invoices.Normalization(request.Data);

                    if (request.Path == "transaction")
                    {
                        var newRequest = bankSerializer.DeSerializeXML<BaseRequest<(string, decimal)>>(GetRequest());

                        response = await transactionService.Transaction(transactions, invoices, request.Data, newRequest.Data.Item1, newRequest.Data.Item2);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<TransactionModel>>(response));
                    }
                    else if (request.Path == "historyBySender")
                    {
                        try
                        {
                            var newResponse = await transactionService.HistoryBySender(transactions, request.Data);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>(newResponse));
                        }
                        catch
                        {
                            var newResponse = new BaseResponse<IEnumerable<TransactionModel>>(false, null);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>(newResponse));
                        }
                    }
                    else if (request.Path == "historyByUser")
                    {
                        try
                        {
                            var newRequest = bankSerializer.DeSerializeXML<BaseRequest<UserModel>>(GetRequest());

                            var newResponse = await transactionService.HistoryByUser(transactions, newRequest.Data);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>(newResponse));
                        }
                        catch
                        {
                            var newResponse = new BaseResponse<IEnumerable<TransactionModel>>(false, null);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>(newResponse));
                        }
                    }
                    else
                    {
                        response = new BaseResponse<TransactionModel>(false, null);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<TransactionModel>>(response));
                    }
                }
                catch
                {
                    response = new BaseResponse<TransactionModel>(false, null);
                    await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<TransactionModel>>(response));
                }
                finally
                {
                    stream = null;
                }
            }
        }
    }
}