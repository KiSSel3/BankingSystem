using BankClient.Interfaces;
using BankSerializer;
using Domain.Interfaces;
using Domain.Models;
using Domain.Request;
using Domain.Response;
using System.IO;
using System.Net.Sockets;

namespace BankClient.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        private Serializer bankSerializer;

        public InvoiceService(IEncoderService encoderService) : base(encoderService)
        {
            bankSerializer = new();
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> CreateInvoice(UserModel user, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
               var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("add", user)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>((GetRequest(stream)));
            }
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> GetInvoices(UserModel user, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("get", user)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>((GetRequest(stream)));
            }
        }

        public async Task<BaseResponse<IEnumerable<InvoiceModel>>> DeleteInvoice(UserModel user, InvoiceModel invoice, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("delete", user)), stream);
                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<InvoiceModel>>(new BaseRequest<InvoiceModel>("delete", invoice)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>((GetRequest(stream)));
            }
        }
    }
}
