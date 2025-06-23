using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;

namespace QuickTests.DependencyInjection.Options;

[TestClass]
public class MyOptionsTests
{
    [TestMethod]
    public void InvalidOptionsThrowOptionsValidationException()
    {
        //ARRANGE
        var s = new ServiceCollection();
        s.AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidate>();
        s.AddSingleton<IPostConfigureOptions<MyOptions>, MyOptionsPostConfigure>();
        s.AddOptions<MyOptions>();

        using var sp = s.BuildServiceProvider();

        //ACT
        // ReSharper disable once AccessToDisposedClosure
        var exception = Assert.Throws<OptionsValidationException>(() =>
            sp.GetRequiredService<IOptions<MyOptions>>().Value);

        Debug.WriteLine("Exception was successfully thrown:");
        Debug.WriteLine(exception);
    }

    [TestMethod]
    public void TestValidOptions()
    {
        //ARRANGE
        var s = new ServiceCollection();
        s.AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidate>();
        s.AddSingleton<IPostConfigureOptions<MyOptions>, MyOptionsPostConfigure>();
        s.AddOptions<MyOptions>();

        s.Configure<MyOptions>(o =>
        {
            o.Ip = "192.168.0.1";
            o.Port = 1234;
        });

        using var sp = s.BuildServiceProvider();

        //ACT
        var options = sp.GetRequiredService<IOptions<MyOptions>>().Value;

        //ASSERT
        Assert.IsNotNull(options);
        Console.WriteLine(options);
        Assert.AreEqual("192.168.0.1", options.Ip);
        Assert.AreEqual(1234, options.Port);
        Assert.IsTrue(MyOptionsPostConfigure.CallCount > 0, $"{nameof(MyOptionsPostConfigure)}.{nameof(MyOptionsPostConfigure.PostConfigure)} was not called");
        Assert.IsTrue(MyOptionsPostConfigure.CallCount > 0, $"{nameof(MyOptionsValidate)}.{nameof(MyOptionsValidate.Validate)} was not called");
    }

    [TestMethod]
    public void TestPostConfigure()
    {
        //ARRANGE
        var s = new ServiceCollection();
        s.AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidate>();
        s.AddSingleton<IPostConfigureOptions<MyOptions>, MyOptionsPostConfigure>();
        s.AddOptions<MyOptions>();

        s.Configure<MyOptions>(o =>
        {
            o.Ip = "192-168-0-1"; //To demonstrate IPostConfigureOptions
            o.Port = 1234;
        });

        using var sp = s.BuildServiceProvider();

        //ACT
        var options = sp.GetRequiredService<IOptions<MyOptions>>().Value;

        //ASSERT
        Assert.IsNotNull(options);
        Console.WriteLine(options);
        Assert.AreEqual("192.168.0.1", options.Ip);
        Assert.AreEqual(1234, options.Port);
        Assert.IsTrue(MyOptionsPostConfigure.CallCount > 0, $"{nameof(MyOptionsPostConfigure)}.{nameof(MyOptionsPostConfigure.PostConfigure)} was not called");
        Assert.IsTrue(MyOptionsPostConfigure.CallCount > 0, $"{nameof(MyOptionsValidate)}.{nameof(MyOptionsValidate.Validate)} was not called");
    }
}

file record MyOptions
{
    public required string Ip { get; set; }
    public int Port { get; set; }
}

file class MyOptionsValidate : IValidateOptions<MyOptions>
{
    public static int CallCount = 0;
    public ValidateOptionsResult Validate(string? name, MyOptions o)
    {
        Debug.WriteLine($"{nameof(MyOptionsValidate)}.{nameof(Validate)}() called");

        if (string.IsNullOrEmpty(o.Ip))
            return ValidateOptionsResult.Fail($"No {nameof(o.Ip)} provided");

        if (!IPAddress.TryParse(o.Ip, out _))
            return ValidateOptionsResult.Fail($"Invalid {nameof(o.Ip)} provided");



        CallCount++;
        return ValidateOptionsResult.Success;
    }
}

file class MyOptionsPostConfigure : IPostConfigureOptions<MyOptions>
{
    public static int CallCount = 0;
    public void PostConfigure(string? name, MyOptions myOptions)
    {
        Debug.WriteLine($"{nameof(MyOptionsPostConfigure)}.{nameof(PostConfigure)}() called");

        if (!string.IsNullOrEmpty(myOptions.Ip) && myOptions.Ip.Contains('-'))
        {
            Debug.WriteLine("Replacing '-' by '.'...");
            myOptions.Ip = myOptions.Ip.Replace('-', '.');
        }
        CallCount++;
    }
}