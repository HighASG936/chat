using System.Net;
using System.Net.Sockets;

namespace ServerChat.Services
{
    public class ServerChat : IServerChat
    {
        private readonly TcpListener _listener;

        public ServerChat()
        {
            _listener = new TcpListener(IPAddress.Any, 5000);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("Server listening in port 5000...");

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Connected Client");
                _ = HandleClient(client);
            }
        }

        private static Task HandleClient(TcpClient client)
        {
            using var stream = client.GetStream();
            return Task.CompletedTask;
        }
    }

}