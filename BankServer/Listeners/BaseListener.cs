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

        public BaseListener(int port)
        {
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

            while (byteRead != '\n' && byteRead != -1)
            {
                buffer.Add((byte)byteRead);

                byteRead = stream.ReadByte();
            }

            request = Encoding.UTF8.GetString(buffer.ToArray());
        }
    }
}
