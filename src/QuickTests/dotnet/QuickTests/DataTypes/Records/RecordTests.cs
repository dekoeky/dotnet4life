namespace QuickTests.DataTypes.Records;

[TestClass]
public class RecordTests
{
    [TestMethod]
    public void New_Equals_Default_EqualityOperator()
    {
        //ARRANGE
        var r = new MyRecord();

        //ACT
        var result = r == MyRecord.Default;

        //ASSERT
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void New_Equals_Default_EqualsMethod()
    {
        //ARRANGE
        var r = new MyRecord();

        //ACT
        var result = r.Equals(MyRecord.Default);

        //ASSERT
        Assert.IsTrue(result);
    }


    [TestMethod]
    public void OtherParameters_DoesNotEqual_Default()
    {
        //ARRANGE
        var r = new MyRecord
        {
            Ip = "1.2.3.4",
            Port = 54321,
        };

        //ACT
        var result = r == MyRecord.Default;

        //ASSERT
        Assert.IsFalse(result);
    }

    private record MyRecord
    {
        public string? Ip { get; init; }
        public int Port { get; init; }

        public static readonly MyRecord Default = new();
    }
}
