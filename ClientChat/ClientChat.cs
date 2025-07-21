
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat
{
    class ClientChat() : ChatBase, IClientChat
    {
        private HubConnection _connection;

        public ClientChat()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(ServerUrl)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });
        }

        public async Task ConnectAsync()
        {
            await _connection.StartAsync();
            Console.WriteLine("✅ Cliente conectado.");

            while (true)
            {
                var message = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(message)) continue;
                await _connection.InvokeAsync("SendMessage", "Cliente", message);
            }
        }
    }
}
