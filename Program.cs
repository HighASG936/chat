
using System;
using Microsoft.Extensions.DependencyInjection;
using test_github_net.Chat.Services;
using Microsoft.Extensions.DependencyInjection;

// Add Services
var services = new ServiceCollection();
services.AddSingleton<IServerChat, ServerChat>();
services.AddTransient<IClientChat, ClientChat>();

var provider = services.BuildServiceProvider();

// var client = provider.GetRequiredService<IClientChat>();
// client.Connect();

void StartServer()
{
    var server = provider.GetRequiredService<IServerChat>();
    server.Start();
}

Console.WriteLine("Hello World");
StartServer();
