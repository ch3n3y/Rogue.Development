using Microsoft.AspNetCore.SignalR;

namespace Rogue.Development.Ui.Stocks;

public class StocksHub : Hub
{
    public const string Url = "/stocks";

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}