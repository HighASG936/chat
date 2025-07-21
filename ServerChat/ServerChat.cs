
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat
{
    class ServerChat() : ChatBase, IServerChat
    {                
        public void Start()
        {
            var connection = new HubConnectionBuilder()
            .WithUrl(ServerUrl)
            .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}: {message}");
            });
            await connection.StartAsync();

            while (true)
            {
                var message = Console.ReadLine();
                await connection.InvokeAsync("SendMessage", "Usuario", message);
            }
        }
    }
}