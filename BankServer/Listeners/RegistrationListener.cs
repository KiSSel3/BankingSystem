using BankServer.Interfaces;
using BankServer.Models;
using BankServer.Request;
using BankServer.Response;

using System.Net.Sockets;

namespace BankServer.Listeners
{
    public class RegistrationListener : BaseListener
    {
        private IUserRepository users;
        private IGeneratorId userGeneratorId;
        private IRegistrationService registrationService;

        public RegistrationListener(int port, IEncoderService encoderService, IUserRepository _users, IRegistrationService _registrationService, IGeneratorId _userGeneratorId) : base(port, encoderService)
        {
            users = _users;
            userGeneratorId = _userGeneratorId;
            registrationService = _registrationService;
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                BaseResponse<UserModel> response;

                stream = client.GetStream();

                try
                {
                    var request = bankSerializer.DeSerializeXML<BaseRequest<UserModel>>(GetRequest());

                    if (request.Path == "registration")
                    {
                        response = await registrationService.Registration(users, request.Data, userGeneratorId);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<UserModel>>(response));
                    }
                    else
                    {
                        response = new BaseResponse<UserModel>(false, null);
                        await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<UserModel>>(response));
                    }
                }
                catch
                {
                    response = new BaseResponse<UserModel>(false, null);
                    await SendingMesageAsync(bankSerializer.SerializeJSON<BaseResponse<UserModel>>(response));
                }
                finally
                {
                    stream = null;
                }
            }
        }
    }
}