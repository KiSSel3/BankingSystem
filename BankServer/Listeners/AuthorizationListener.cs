using BankSerializer;
using BankServer.Interfaces;
using BankServer.Models;
using BankServer.Request;
using BankServer.Response;
using BankServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class AuthorizationListener : BaseListener
    {
        private IUserRepository users;
        private IAuthorizationService authorizationService;

        public AuthorizationListener(int port, IEncoderService encoderService, IUserRepository _users, IAuthorizationService _authorizationService) : base(port, encoderService)
        {
            users = _users;
            authorizationService = _authorizationService;
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

                    if (request.Path == "authorization")
                    {
                        response = await authorizationService.Authorization(users, request.Data);
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
