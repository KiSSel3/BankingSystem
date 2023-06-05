using BankSerializer;
using BankServer.Interfaces;
using Domain.Models;
using Domain.Request;
using Domain.Response;
using Domain.Interfaces;

using System.Net.Sockets;
using System.Net;

namespace BankServer.Listeners
{
    public class InvoiceListener : BaseListener
    {
        private IInvoiceRepository invoices;
        private IUserRepository users;
        private IInvoiceService invoiceService;
        private IBaseGenerator numberGenerator;

        private Serializer bankSerializer;

        public InvoiceListener(int port, IEncoderService encoderService, IInvoiceRepository _invoices, IUserRepository _users,  IInvoiceService _invoiceService, IBaseGenerator _numberGenerator) : base(port, encoderService)
        {
            invoices = _invoices;
            users = _users;
            invoiceService = _invoiceService;
            numberGenerator = _numberGenerator;

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
                    request.Data = await users.Normalization(request.Data);


                    if (request.Path == "add")
                    {
                        response = await invoiceService.AddInvoice(invoices, request.Data, numberGenerator);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));

                    }
                    else if(request.Path == "update")
                    {
                        try
                        {
                            var invoiceRequest = bankSerializer.DeSerializeXML<BaseRequest<InvoiceModel>>(GetRequest());
                            var newResponse = await invoiceService.UpdateInvoice(invoices, invoiceRequest.Data);

                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<InvoiceModel>>(newResponse));
                        }
                        catch
                        {
                            var newResponse = new BaseResponse<InvoiceModel>(false, null);
                            await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<InvoiceModel>>(newResponse));
                        }
                    }
                    else if (request.Path == "get")
                    {
                        response = await invoiceService.GetUserInvoices(invoices, request.Data);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<IEnumerable<InvoiceModel>>>(response));
                    }
                    else if (request.Path == "delete")
                    {
                        var invoiceRequest = bankSerializer.DeSerializeXML<BaseRequest<InvoiceModel>>(GetRequest());
                        invoiceRequest.Data = await invoices.Normalization(invoiceRequest.Data);

                        if(invoiceRequest.Path == "delete")
                        {
                            response = await invoiceService.DeleteInvoice(invoices, invoiceRequest.Data, request.Data, numberGenerator);
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