using BankServer.Interfaces;
using BankServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class RegistrationListener : BaseListener
    {
        private IRepository<UserModel> users;
        private IRegistrationService registrationService;

        public RegistrationListener(int port, IRepository<UserModel> _users, IRegistrationService _registrationService) : base(port)
        {
            users = _users;
            registrationService = _registrationService;
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();
                
                while(true)
                {
                    GetRequest();

                    if(registrationService.IsNewName(users, request))
                    {
                        await stream.WriteAsync(Encoding.UTF8.GetBytes("true\n"));
                        break;
                    }
                    else
                    {
                        await stream.WriteAsync(Encoding.UTF8.GetBytes("false\n"));
                    } 
                }

                GetRequest();

                //Временно || В дальнейшем приходить будет сериализованный класс
                var bufList = request.Split("||");
                registrationService.AddUser(users, new UserModel(1, bufList[0], bufList[1]));

                stream = null;
            }
        }
    }
}
