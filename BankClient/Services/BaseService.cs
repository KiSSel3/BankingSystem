using BankSerializer;
using Domain.Interfaces;

using System.Net.Sockets;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankClient.Services
{
    public abstract class BaseService
    {
        protected IEncoderService encoderService;

        public BaseService(IEncoderService _encoderService)
        {
            encoderService = _encoderService;
        }

        protected async Task SendingMessageAsync(string message, NetworkStream? stream)
        {
            if (stream is null)
                return;

            message = encoderService.Encript(message, "1-1-08Key8For8Encrypt80-1-1");
            await stream.WriteAsync(Encoding.UTF8.GetBytes(message + "END"));
        }

        protected string GetRequest(NetworkStream? stream)
        {
            if (stream is null)
                return "";

            List<byte> buffer = new();
            int byteRead = stream.ReadByte();

            while (true)
            {
                if (byteRead == -1 || (buffer.Count >= 3 && byteRead == (byte)'D' && buffer[buffer.Count - 1] == (byte)'N' && buffer[buffer.Count - 2] == (byte)'E'))
                {
                    buffer.RemoveRange(buffer.Count - 2, 2);
                    break;
                }

                buffer.Add((byte)byteRead);
                byteRead = stream.ReadByte();
            }

            return encoderService.Decrypt(Encoding.UTF8.GetString(buffer.ToArray()), "1-1-08Key8For8Encrypt80-1-1");
        }
    }
}
