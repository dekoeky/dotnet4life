using MyTool.Commands;
using System.CommandLine;
using System.Text;

// Emoji support :)
Console.OutputEncoding = Encoding.UTF8;

return new MyRootCommand().Invoke(args);