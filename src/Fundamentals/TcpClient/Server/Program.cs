using Shared;
using Shared.Messages;
using System.Net;
using System.Net.Sockets;
using static Shared.Constants;

var listener = new TcpListener(IPAddress.Any, DefaultServerPort);
listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
listener.Start(backlog: 1024); // allow a big accept queue
Console.WriteLine($"Server listening on {DefaultServerPort}...");

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };





// cap concurrent clients
var concurrency = new SemaphoreSlim(200);

while (!cts.IsCancellationRequested)
{
    var client = await listener.AcceptTcpClientAsync(cts.Token);
    _ = Task.Run(async () =>
    {
        await concurrency.WaitAsync(cts.Token);
        try { await HandleClientAsync(client, cts.Token); }
        catch (Exception ex) { Console.WriteLine(ex); }
        finally { concurrency.Release(); }
    }, cts.Token);
}

return;


async Task HandleClientAsync(TcpClient client, CancellationToken ct)
{
    using var _ = client;
    await using var stream = client.GetStream();

    client.NoDelay = true; // lower latency


    var data = new byte[MaxMessageLength];

    while (!ct.IsCancellationRequested)
    {
        // Read header (2 bytes). If 0 bytes => client closed.
        await stream.ReadExactlyAsync(data, 0, HeaderLength, ct);

        var messageType = (MessageType)data[0];
        int remainingBytes = data[1];

        //Read data, if present
        if (remainingBytes > 0)
            await stream.ReadExactlyAsync(data, HeaderLength, remainingBytes, ct);

        switch (messageType)
        {
            case MessageType.Dummy:
                Console.WriteLine("Dummy message received!");
                break;

            case MessageType.SetBackgroundColor:
                var mbc = SetBackgroundColorMessage.ParseFromDataBytes(data.AsSpan(DataStart));
                Console.BackgroundColor = mbc.Color;
                Console.WriteLine($"Background Color is now {mbc.Color}");
                break;

            case MessageType.SetForegroundColor:
                var mfc = SetForegroundColorMessage.Parse(data.AsSpan(DataStart));
                Console.ForegroundColor = mfc.Color;
                Console.WriteLine($"Foreground Color is now {mfc.Color}");
                break;
            default:
                Console.WriteLine($"Unknown MessageType Received: {messageType}");
                break;
        }

    }
}