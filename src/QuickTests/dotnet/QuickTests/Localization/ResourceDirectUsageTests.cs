using System.Globalization;

namespace QuickTests.Localization;

/// <summary>
/// Demonstrates the direct usage of Generated Resource class <see cref="MyResources"/>
/// </summary>
[TestClass]
public class ResourceDirectUsageTests
{
    [TestMethod]
    public void Get_Greeting()
    {
        // ---------- ACT --------------
        var greeting = MyResources.Greeting;
        var cultureName = MyResources.Culture?.Name ?? "<null>";

        // ---------- ASSERT -----------
        Console.WriteLine($"Greeting: {greeting}");
        Console.WriteLine($"Culture: {cultureName}");
        Console.WriteLine($"Current: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine($"Current UI: {CultureInfo.CurrentUICulture.Name}");
    }

    [TestMethod]
    public void Get_Greeting_ViaResourceManager()
    {
        // ---------- ACT --------------
        var resourceManager = MyResources.ResourceManager;
        var greeting = resourceManager.GetString("Greeting");
        var cultureName = MyResources.Culture?.Name ?? "<null>";

        // ---------- ASSERT -----------
        Console.WriteLine($"Greeting: {greeting}");
        Console.WriteLine($"Culture: {cultureName}");
        Console.WriteLine($"Current: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine($"Current UI: {CultureInfo.CurrentUICulture.Name}");
    }


    [DataRow("nl-BE")]
    [DataRow("nl-NL")]
    [DataRow("nl")]
    [DataRow("en")]
    [DataRow("ru-RU")]
    [TestMethod]
    public void Get_Greeting_AfterSettingResourceCulture(string culture)
    {
        // -------- ARRANGE ------------
        MyResources.Culture = CultureInfo.GetCultureInfoByIetfLanguageTag(culture); // Has no influence

        // ---------- ACT --------------
        var greeting = MyResources.Greeting;
        var cultureName = MyResources.Culture.Name ?? "<null>";

        // ---------- ASSERT -----------
        Console.WriteLine($"Greeting: {greeting}");
        Console.WriteLine($"Culture: {cultureName}");
        Console.WriteLine($"Current: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine($"Current UI: {CultureInfo.CurrentUICulture.Name}");
    }

    [DataRow("nl-BE")]
    [DataRow("nl-NL")]
    [DataRow("nl")]
    [DataRow("en")]
    [DataRow("ru-RU")]
    [TestMethod]
    public void Get_Greeting_AfterSettingCurrentUICulture(string culture)
    {
        // -------- ARRANGE ------------
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfoByIetfLanguageTag(culture);

        // ---------- ACT --------------
        var greeting = MyResources.Greeting;
        var cultureName = MyResources.Culture?.Name ?? "<null>";

        // ---------- ASSERT -----------
        Console.WriteLine($"Greeting: {greeting}");
        Console.WriteLine($"Culture: {cultureName}");
        Console.WriteLine($"Current: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine($"Current UI: {CultureInfo.CurrentUICulture.Name}");
    }

    [DataRow("nl-BE")]
    [DataRow("nl-NL")]
    [DataRow("nl")]
    [DataRow("en")]
    [DataRow("ru-RU")]
    [TestMethod]
    public void Get_Greeting_AfterSettingCurrentCulture(string culture)
    {
        // -------- ARRANGE ------------
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag(culture);

        // ---------- ACT --------------
        var greeting = MyResources.Greeting;
        var cultureName = MyResources.Culture?.Name ?? "<null>";

        // ---------- ASSERT -----------
        Console.WriteLine($"Greeting: {greeting}");
        Console.WriteLine($"Culture: {cultureName}");
        Console.WriteLine($"Current: {CultureInfo.CurrentCulture.Name}");
        Console.WriteLine($"Current UI: {CultureInfo.CurrentUICulture.Name}");
    }
}