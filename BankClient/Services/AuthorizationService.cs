using BankClient.Interfaces;
using BankSerializer;
using Domain.Interfaces;
using Domain.Models;
using Domain.Request;
using Domain.Response;
using System.Net.Sockets;

namespace BankClient.Services
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {

        private Serializer bankSerializer;

        public AuthorizationService(IEncoderService encoderService) : base(encoderService)
        {
            bankSerializer = new();
        }

        public async Task<BaseResponse<UserModel>> Authorization(string name, string password, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {

                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                var newUser = new UserModel(name, password);

                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("authorization", newUser)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<UserModel>>((GetRequest(stream)));
            }
        }
    }
}
