using BankServer.Interfaces;
using BankServer.Models;
using BankSerializer;
using BankServer.Services;
using BankServer.Response;
using BankServer.Request;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


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
                    var request = bankSerializer.DeSerializeXML<BaseRequest<(InvoiceModel, string, decimal)>>(GetRequest());

                    if(request.Path == "transaction")
                    {
                        response = await transactionService.Transaction(transactions, invoices, request.Data.Item1, request.Data.Item2, request.Data.Item3);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<TransactionModel>>(response));
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