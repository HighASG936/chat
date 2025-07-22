using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerChat.Services
{
    public class ServerChat : IServerChat
    {
        private readonly TcpListener _listener;

        public ServerChat()
        {
            _listener = new TcpListener(IPAddress.Any, 55000);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine("Server listening in port 55000...");

            while (true)
            {
                var client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Connected Client");
                _ = HandleClient(client);
            }
        }

        private static async Task HandleClient(TcpClient client)
        {
            Console.WriteLine("Attended client...");
            using var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                int bytesRead = await stream.ReadAsync(buffer);
                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received message: " + message);

                var response = Encoding.UTF8.GetBytes("Successfully message received");
                await stream.WriteAsync(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handling error client: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Disconnected client.");
            }
        }

    }

}