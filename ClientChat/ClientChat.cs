using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientChat;

public class ChatClient : IClientChat
{
    public async Task Start()
    {
        Console.WriteLine("Connecting...");

        using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        await socket.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 55000));
        Console.WriteLine("Connected to server");

        var message = "Hello from client";
        var buffer = Encoding.UTF8.GetBytes(message);

        await socket.SendAsync(buffer);
        Console.WriteLine("Sent message: " + message);

        var responseBuffer = new byte[1024];
        int bytesRead = await socket.ReceiveAsync(responseBuffer);
        var response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        Console.WriteLine("Reply from server: " + response);

        socket.Close();
    }
}
