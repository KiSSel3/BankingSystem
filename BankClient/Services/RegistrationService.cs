using BankClient.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Domain.Response;
using Domain.Request;
using BankSerializer;

using System.Net.Sockets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient.Services
{
    public class RegistrationService : BaseService, IRegistrationService
    {
        Serializer bankSerializer;

        public RegistrationService(IEncoderService encoderService) : base(encoderService) 
        {
            bankSerializer = new();
        }


        public async Task<BaseResponse<UserModel>> Registration(string name, string password, string ipAdress, int port)
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAdress, port);
                var stream = client.GetStream();

                var newUser = new UserModel(name, password);


                await SendingMessageAsync(bankSerializer.SerializeXML<BaseRequest<UserModel>>(new BaseRequest<UserModel>("registration", newUser)), stream);

                return bankSerializer.DeSerializeJSON<BaseResponse<UserModel>>((GetRequest(stream)));
            }
        }
    }
}
