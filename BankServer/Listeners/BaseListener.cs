using System.Net;
using System.Net.Sockets;

namespace BankServer.Listeners
{
    public abstract class BaseListener
    {
        private readonly TcpListener listener;
        private readonly CancellationTokenSource cancellationTokenSource; 

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
    }
}
