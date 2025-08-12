using Shared;
using Shared.Messages;
using System.Net.Sockets;

namespace Client;

internal class DeviceClient(string host, int port)
{
    private TcpClient? _client;
    private NetworkStream? _stream;
    private readonly byte[] _sendBuffer = new byte[Constants.MaxMessageLength];
    private readonly byte[] _receiveBuffer = new byte[Constants.MaxMessageLength];
    private readonly SemaphoreSlim _sendLock = new(1, 1);

    public Task SendDummy(CancellationToken cancellationToken = default) =>
        Send(new DummyMessage(), cancellationToken);

    public Task SetBackgroundColor(ConsoleColor color, CancellationToken cancellationToken = default) =>
        Send(new SetBackgroundColorMessage(color), cancellationToken);

    public Task SetForegroundColor(ConsoleColor color, CancellationToken cancellationToken = default) =>
        Send(new SetForegroundColorMessage(color), cancellationToken);


    public Task<ConsoleColor> GetForegroundColor(CancellationToken cancellationToken = default) =>
        SendAndReceive<GetForegroundColorReplyMessage>(new GetForegroundColorMessage(), cancellationToken);

    private async Task Send(MessageBase message, CancellationToken cancellationToken) //TODO: Find better performance than byte[]
    {
        await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            await SendCore(message, cancellationToken);
        }
        finally
        {
            _sendLock.Release();
        }
    }

    private async Task SendAndReceive<TReceive>(MessageBase message, CancellationToken cancellationToken)
    {
        await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            await SendCore(message, cancellationToken);
            _stream.ReadExactlyAsync()
        }
        finally
        {
            _sendLock.Release();
        }
    }

    private async ValueTask SendCore(MessageBase message, CancellationToken cancellationToken)
    {
        _client ??= new TcpClient();

        if (!_client.Connected)
            await _client.ConnectAsync(host, port, cancellationToken);

        _stream = _client.GetStream();

        await _stream.WriteAsync(message.GetBytes(), cancellationToken);
    }

    private async Task<T> ReceiveCore<T>() where T : MessageBase
    {

    }

}