using BankSerializer;
using BankServer.Interfaces;
using BankServer.Models;
using BankServer.Request;
using BankServer.Response;

using System.Net.Sockets;

namespace BankServer.Listeners
{
    public class InvoiceListener : BaseListener
    {
        private IInvoiceRepository invoices;
        private IInvoiceService invoiceService;
        private IGeneratorId invoiceGeneratorId;
        private IGeneratorNumberInvoice generatorNumberInvoice;

        private Serializer bankSerializer;

        public InvoiceListener(int port, IEncoderService encoderService, IInvoiceRepository _invoices, IInvoiceService _invoiceService, IGeneratorId _invoiceGeneratorId, IGeneratorNumberInvoice _generatorNumberInvoice) : base(port, encoderService)
        {
            invoices = _invoices;
            invoiceService = _invoiceService;
            invoiceGeneratorId = _invoiceGeneratorId;
            generatorNumberInvoice = _generatorNumberInvoice;

            bankSerializer = new();
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                BaseResponse<IEnumerable<InvoiceModel>> response;

                stream = client.GetStream();

                try
                {
                    var request = bankSerializer.DeSerializeXML<BaseRequest<UserModel>>(GetRequest());

                    if (request.Path == "add")
                    {
                        response = await invoiceService.AddInvoice(invoices, request.Data, generatorNumberInvoice, invoiceGeneratorId);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));

                    }
                    else if (request.Path == "get")
                    {
                        response = await invoiceService.GetUserInvoices(invoices, request.Data);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                    }
                    else if (request.Path == "delete")
                    {
                        var invoiceRequest = bankSerializer.DeSerializeXML<BaseRequest<InvoiceModel>>(GetRequest());

                        if(invoiceRequest.Path == "delete")
                        {
                            response = await invoiceService.DeleteInvoice(invoices, invoiceRequest.Data, request.Data);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                        }
                        else
                        {
                            response = new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                        }
                    }
                    else
                    {
                        response = new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                    }
                }
                catch
                {
                    response = new BaseResponse<IEnumerable<InvoiceModel>>(false, null);
                    await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                }
                finally
                {
                    stream = null;
                }
            }
        }
    }
}