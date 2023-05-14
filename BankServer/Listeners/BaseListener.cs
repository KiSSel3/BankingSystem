using BankServer.Interfaces;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BankServer.Listeners
{
    public abstract class BaseListener
    {
        private readonly TcpListener listener;
        private readonly CancellationTokenSource cancellationTokenSource;

        protected NetworkStream? stream = default(NetworkStream);
        protected string request = default(string);

        protected IEncoderService encoderService;

        public BaseListener(int port, IEncoderService _encoderService)
        {
            encoderService = _encoderService;

            listener = new TcpListener(IPAddress.Any, port);
            cancellationTokenSource = new CancellationTokenSource();   
        }

        public int Port()
        {
            return ((IPEndPoint)listener.LocalEndpoint).Port;
        }

        public void Start()
        {
            listener.Start();
            Task.Run(() => AcceptClientsAsync(cancellationTokenSource.Token));
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            listener.Stop();
        }

        //try-catch
        private async Task AcceptClientsAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync();
                Task.Run(() => HandleClientAsync(client));
            }
        }

        protected abstract Task HandleClientAsync(TcpClient client);

        protected void GetRequest()
        {
            if (stream is null)
                return;

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

            request = encoderService.Decrypt(Encoding.UTF8.GetString(buffer.ToArray()),"1-1-08Key8For8Encrypt80-1-1");
        }

        protected async Task SendingMesageAsync(string message)
        {
            if (stream is null)
                return;

            message = encoderService.Encript(message, "1-1-08Key8For8Encrypt80-1-1");

            await stream.WriteAsync(Encoding.UTF8.GetBytes(message + "END"));
        } 
    }
}
