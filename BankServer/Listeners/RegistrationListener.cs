using BankServer.Interfaces;
using BankServer.Models;
using BankSerializer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class RegistrationListener : BaseListener
    {
        private IRepository<UserModel> users;
        private IGeneratorId userGeneratorId;
        private IRegistrationService registrationService;

        private Serializer bankSerializer;

        public RegistrationListener(int port, IEncoderService encoderService, IRepository<UserModel> _users, IRegistrationService _registrationService, IGeneratorId _userGeneratorId) : base(port, encoderService)
        {
            users = _users;
            userGeneratorId = _userGeneratorId;
            registrationService = _registrationService;

            bankSerializer = new();
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
                        await SendingMesageAsync("true");
                        break;
                    }
                    else
                    {
                        await SendingMesageAsync("false");
                    } 
                }

                GetRequest();

                var userData = request.Split("||");
                var newUser = new UserModel(userGeneratorId.Next(), userData[0], userData[1]);

                registrationService.AddUser(users, newUser);

                await SendingMesageAsync(bankSerializer.SerializeJSON<UserModel>(newUser));

                stream = null;
            }
        }
    }
}
