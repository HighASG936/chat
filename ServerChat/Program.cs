
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerChat.Services;
using _ServerChat = ServerChat.Services.ServerChat;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IServerChat, _ServerChat>();
    })
    .Build();


using var scope = host.Services.CreateScope();
var server = scope.ServiceProvider.GetRequiredService<IServerChat>();
await server.Start();
