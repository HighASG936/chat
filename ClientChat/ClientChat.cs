using System.Net.Sockets;
using System.Text;

namespace ClientChat;

public class ChatClient : IClientChat
{
    public async Task Start()
    {
        using var client = new TcpClient();
        await client.ConnectAsync("localhost", 5000);
        Console.WriteLine("Conectado al servidor");

        using var stream = client.GetStream();

        var message = "Hola desde el cliente";
        var buffer = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(buffer, 0, buffer.Length);
        Console.WriteLine("Mensaje enviado: " + message);

        var responseBuffer = new byte[1024];
        var bytesRead = await stream.ReadAsync(responseBuffer);
        var response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        Console.WriteLine("Respuesta del servidor: " + response);
    }
}
