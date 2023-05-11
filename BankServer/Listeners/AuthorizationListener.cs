using BankSerializer;
using BankServer.Interfaces;
using BankServer.Models;

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
        private IRepository<UserModel> users;
        private IAuthorizationService authorizationService;
        private IUserService userService;

        private Serializer bankSerializer;
        public AuthorizationListener(int port, IEncoderService encoderService, IRepository<UserModel> _users, IAuthorizationService _authorizationService, IUserService _userService) : base(port, encoderService)
        {
            users = _users;
            authorizationService = _authorizationService;
            userService = _userService;

            bankSerializer = new();
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                string[] userData;

                stream = client.GetStream();

                while(true)
                {
                    GetRequest();

                    userData = request.Split("||");
                    if (authorizationService.IsUserRegistered(users, userData[0], userData[1]))
                    {
                        await SendingMesageAsync("true");
                        break;
                    }
                    else
                    {
                        await SendingMesageAsync("false");
                    }
                }

                var currentUser = userService.GetUser(users, userData[0]);
                await SendingMesageAsync(bankSerializer.SerializeJSON<UserModel>(currentUser));

                stream = null;
            }
        }
    }
}
