using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace QuickTests.Tcp.Client;

/// <summary>
/// This test demonstrates that using localhost is slower than using the loopback IP address.
/// </summary>
[TestClass]
[DoNotParallelize] // To avoid port conflicts
public class Localhost
{
    private const int Port = 9999;

    [TestMethod]
    [DataRow("127.0.0.1")] // Fast
    [DataRow("localhost")] // Slower
    public async Task IPv4(string host)
    {
        //ARRANGE
        using var listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();

        //ACT
        using var client = new TcpClient();
        var sw = Stopwatch.StartNew();
        await client.ConnectAsync(host, Port);

        //ASSERT
        Console.WriteLine($"{host}: {sw.Elapsed}");
    }

    [TestMethod]
    [DataRow("::1")]        // Fast
    [DataRow("localhost")]  // Slower
    public async Task IPv6(string host)
    {
        //ARRANGE
        using var listener = new TcpListener(IPAddress.IPv6Any, Port);
        listener.Start();

        //ACT
        using var client = new TcpClient();
        var sw = Stopwatch.StartNew();
        await client.ConnectAsync(host, Port);

        //ASSERT
        Console.WriteLine($"{host}: {sw.Elapsed}");
    }
}