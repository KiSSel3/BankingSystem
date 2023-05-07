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
    public class UserListener : BaseListener
    {
        private IRepository<UserModel> repository;
        private IUserService userService;

        public UserListener(int port, IRepository<UserModel> _repository, IUserService _userService) : base(port)
        {
            repository = _repository;
            userService = _userService;
        }

        protected override async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                stream = client.GetStream();

                await Console.Out.WriteLineAsync($"\nКоличество клиентво: {repository.GetAll().Count}\n");

                stream = null;
            }
        }
    }
}
