using System.Text.RegularExpressions;

namespace QuickTests.Text;

[TestClass]
public partial class RegexTests
{
    [TestMethod]
    public void TestMethod1()
    {
        //ARRANGE
        var input = LargeBlockOfTextContainingTheSecretCode;
        var rgx = TheRegex;

        //ACT
        var match = rgx.Match(input);
        var code = match.Groups[1].Value;

        //ASSERT
        Assert.AreEqual("2736287361", code);
    }

    [GeneratedRegex("The secret code is\\s*(\\d+)")]
    partial Regex TheRegex { get; }
    private const string LargeBlockOfTextContainingTheSecretCode =
        """
        sqjdkqjsdmlkqsjdmlkqsjdmlqsjdljmsqhdmlqskjd jkshd lqdh mqh mldlmd qmsddd qsd qdqsd
        dsq dqs dqs ds d. sqdsqdSdsdqd.
        qsdQSdsqdqsljdkqjdlkqs jdlqj dlkqjsdlkjqsl kdjqlskhjd lqshkjh d .
        sqmldjlsdj mqsjd mljdmljd The secret code is 2736287361. qsdqsd qsd
        sqdqsdqpksdjk dkjd qslkmj dmlkqjsd lkqjsd qsd qsd d.
        sdqsdlqsdjlkqj dlkqj dlkqsjd klqsjdljq.
        """;
}