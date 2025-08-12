using Client;
using Shared;
using static Shared.Constants;




var client = new DeviceClient("localhost", DefaultServerPort);


while (true)
    switch (SelectEnum<MessageType>())
    {
        case MessageType.Dummy: await client.SendDummy(); break;
        case MessageType.SetBackgroundColor: await client.SetBackgroundColor(SelectEnum<ConsoleColor>()); break;
        case MessageType.SetForegroundColor: await client.SetForegroundColor(SelectEnum<ConsoleColor>()); break;
        default: Console.WriteLine("Not implemented"); break;
    }

T SelectEnum<T>() where T : struct, Enum
{
    var possibleValues = string.Join(',', Enum.GetValues<T>());
    while (true)
    {
        Console.WriteLine($"Select {typeof(T).Name}: ({possibleValues})");

        if (Enum.TryParse(Console.ReadLine(), true, out T value))
            return value;
        else
            Console.WriteLine($"Invalid {typeof(T).Name} value! Valid options are: {possibleValues}");
    }
}


//var host = "localhost";
//var port = Constants.DefaultServerPort;

//using var client = new TcpClient();
//await client.ConnectAsync(host, port);

//var stream = client.GetStream();
//byte[] buffer = new byte[1024];

//while (true)
//{
//    int bytesRead = await stream.ReadAsync(buffer);

//    if (bytesRead == 0)
//    {
//        Console.WriteLine("Disconnected from server");
//        break; // connection closed
//    }

//    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//    Console.WriteLine($"Received: {message}");
//}
