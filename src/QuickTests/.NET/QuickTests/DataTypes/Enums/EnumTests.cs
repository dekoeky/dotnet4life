namespace QuickTests.DataTypes.Enums;

[TestClass]
public class EnumTests
{

    public static IEnumerable<object[]> TestData =>
    [
        // Status,  Flag to check,  expected result

        [Status.BUSY,                          Status.BUSY,            true],
        [Status.BUSY,                          Status.CLIMATE_WARNING, false],
        [Status.BUSY | Status.CLIMATE_WARNING, Status.BUSY,            true ],
        [Status.BUSY | Status.CLIMATE_WARNING, Status.CLIMATE_WARNING, true ],
    ];

    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void New(Status status, Status flagToCheck, bool expectedResult)
    {
        //Act
        var result = status.HasFlag(flagToCheck);

        //Assert
        Assert.AreEqual(expectedResult, result);
    }

    [DataTestMethod]
    [DynamicData(nameof(TestData))]
    public void Old(Status status, Status flagToCheck, bool expectedResult)
    {
        //Act
        var result = (status & flagToCheck) == flagToCheck;

        //Assert
        Assert.AreEqual(expectedResult, result);
    }



    [Flags]
    public enum Status : byte
    {
        PICTURE_ACTIVE = 0x01,
        AUTODIMMING_ACTIVE = 0x02,
        RESTARTED = 0x04,
        COMMUNICATION_TIMEOUT = 0x08,
        UPS_ACTIVE = 0x10,
        MANUAL_CONTROL = 0x20,
        CLIMATE_WARNING = 0x40,
        BUSY = 0x80
    }
}