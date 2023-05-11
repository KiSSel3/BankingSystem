using BankServer.Interfaces;
using BankServer.Models;
using BankSerializer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankServer.Listeners
{
    public class UserListener : BaseListener
    {
        private IRepository<UserModel> repository;
        private IUserService userService;
        private IGeneratorId userGeneratorId;

        private Serializer serializer;

        public UserListener(int port, IEncoderService encoderService, IRepository<UserModel> _repository, IUserService _userService, IGeneratorId _userGeneratorId) : base(port, encoderService)
        {
            repository = _repository;
            userService = _userService;
            userGeneratorId = _userGeneratorId;

            serializer = new();
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();
                try
                {


                    //var serstr = serializer.SerializeJSONList<UserModel>(repository.GetAll());
                    //var str = encoderService.Encript(serstr, "key");

                    //await Console.Out.WriteLineAsync($"\nКоличество клиентво: {str}");

                    
                }
                catch
                {
                   //await Console.Out.WriteLineAsync("Ошибка");
                }
                
                stream = null;
            }
        }
    }
}
