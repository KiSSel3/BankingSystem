using BankClient.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Domain.Response;
using Domain.Request;
using BankSerializer;

using System.Net.Sockets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankClient.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private Serializer bankSerializer;

        public TransactionService(IEncoderService encoderService) : base(encoderService)
        {
            bankSerializer = new();
        }

        public async Task<BaseResponse<TransactionModel>> CreateTransaction(InvoiceModel invoice, string number, decimal amount, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<InvoiceModel>>(new BaseRequest<InvoiceModel>("transaction",invoice)), stream);
                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<(string, decimal)>>(new BaseRequest<(string, decimal)>("transaction", (number, amount))), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<TransactionModel>>((GetRequest(stream)));
            }
        }

        public async Task<BaseResponse<IEnumerable<TransactionModel>>> GetTransactionsByInvoice(InvoiceModel invoice, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<InvoiceModel>>(new BaseRequest<InvoiceModel>("historyBySender", invoice)), stream);
                return bankSerializer.DeSerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>((GetRequest(stream)));
            }
        }

        public async Task<BaseResponse<IEnumerable<TransactionModel>>> GetTransactionsByUser(UserModel user, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<InvoiceModel>>(new BaseRequest<InvoiceModel>("historyByUser", null)), stream);
                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("historyByUser",user)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<IEnumerable<TransactionModel>>>((GetRequest(stream)));
            }
        }
    }
}
