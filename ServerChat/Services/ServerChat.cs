
using Microsoft.AspNetCore.SignalR.Client;

namespace ServerChat.Services
{
    public class ServerChat : ChatBase.ChatBase, IServerChat
    {
        private HubConnection _connection; 

        private async void StartConnection()
        {
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
                return;
            }
        }

        public async Task Start()
        {
            _connection = new HubConnectionBuilder() 
                .WithUrl(ServerUrl)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });

            StartConnection();

            while (true)
            {
                var message = Console.ReadLine();
                await _connection.InvokeAsync("SendMessage", "Usuario", message);
            }
        }
    }
}