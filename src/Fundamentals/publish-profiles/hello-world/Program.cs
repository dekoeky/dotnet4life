using HelloWorldApp;
using System.CodeDom.Compiler;
using System.Collections;

using var tw = new IndentedTextWriter(Console.Out);


PrintGreeting();
PrintEnvironmentInfo();
//PrintEnvironmentVariables();
PressToExit();

return;

void PrintGreeting()
{
    tw.WriteLine("Hello World!");
    tw.WriteLine();
}

void PrintEnvironmentInfo()
{
    tw.WriteLine("Environment Info:");
    tw.Indent++;
    tw.WriteLine($"Operating System: {Environment.OSVersion.VersionString}");
    tw.WriteLine($"64-Bit OS: {Environment.Is64BitOperatingSystem.YesNo()}");
    tw.WriteLine($"64-Bit Process: {Environment.Is64BitProcess.YesNo()}");
    tw.WriteLine($"Privileged Process: {Environment.IsPrivilegedProcess.YesNo()}");
    tw.WriteLine();
    tw.Indent--;

    //Environment.CurrentDirectory;
    //Environment.MachineName;
    //Environment.ProcessId;
    //Environment.ProcessPath;
    //Environment.ProcessorCount;
    //Environment.CurrentManagedThreadId;
    //Environment.UserName;
    //Environment.Version;
    //Environment.UserInteractive;
}

void PrintEnvironmentVariables()
{
    tw.WriteLine("Environment Variables:");
    tw.Indent++;

    foreach (var entry in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>().OrderBy(de => de.Key.ToString()))
        tw.WriteLine($"{entry.Key}: {entry.Value}");
    tw.Indent--;

    //For readability
    tw.WriteLine();
}

void PressToExit()
{
    Console.WriteLine("Press enter to exit...");
    Console.ReadLine();
}