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

        public AuthorizationListener(int port, IRepository<UserModel> _users, IAuthorizationService _authorizationService) : base(port)
        {
            users = _users;
            authorizationService = _authorizationService;
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();

                GetRequest();

                //Временно || В дальнейшем приходить будет сериализованный класс
                var bufList = request.Split("||");
                if(authorizationService.IsUserRegistered(users, new UserModel(1, bufList[0], bufList[1])))
                {
                    await stream.WriteAsync(Encoding.UTF8.GetBytes("true\n"));
                }
                else
                {
                    await stream.WriteAsync(Encoding.UTF8.GetBytes("false\n"));
                } 

                stream = null;
            }
        }
    }
}
