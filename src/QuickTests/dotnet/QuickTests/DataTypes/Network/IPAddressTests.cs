using System.Net;

namespace QuickTests.DataTypes.Network;

[TestClass]
public class IPAddressTests
{
    [TestMethod]
    public void IPAddressAny_Equals_0_0_0_0()
    {
        //ARRANGE
        const string input = "0.0.0.0";

        //ACT
        var ip = IPAddress.Parse(input);

        //ASSERT
        Assert.AreEqual(IPAddress.Any, ip);
    }

    [TestMethod]
    public void IPAddressLoopback_Equals_127_0_0_1()
    {
        //ARRANGE
        const string input = "127.0.0.1";

        //ACT
        var ip = IPAddress.Parse(input);

        //ASSERT
        Assert.AreEqual(IPAddress.Loopback, ip);
    }

    [TestMethod]
    public void IPAddressAny_ToString()
    {
        //ARRANGE
        var ip = IPAddress.Any;
        const string expected = "0.0.0.0";

        //ACT
        var result = ip.ToString();

        //ASSERT
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void IPAddressLoopback_ToString()
    {
        //ARRANGE
        var ip = IPAddress.Loopback;
        const string expected = "127.0.0.1";

        //ACT
        var result = ip.ToString();

        //ASSERT
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void IPAddressNone_ToString()
    {
        //ARRANGE
        var ip = IPAddress.None;
        const string expected = "255.255.255.255";

        //ACT
        var result = ip.ToString();

        //ASSERT
        Assert.AreEqual(expected, result);
    }
}
