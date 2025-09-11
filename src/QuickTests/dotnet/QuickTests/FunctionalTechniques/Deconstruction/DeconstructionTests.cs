using QuickTests.FunctionalTechniques.Deconstruction.TestModels;

namespace QuickTests.FunctionalTechniques.Deconstruction;

[TestClass]
public class DeconstructionTests
{
    private static readonly Random Random = new();
    private readonly LicensePlate _licensePlate = new("BE", "2 - ABC - 123");
    private readonly Car _car = new()
    {
        BuildYear = 1999,
        Color = "Red",
        HorsePower = 421.5f,
    };
    private readonly Plane _plane = new()
    {
        Altitude = 10000,
        Speed = 300,
    };

    [TestMethod]
    public void Deconstruct_CarClass_3Values()
    {
        //Act
        var (deconstructedBuildYear, deconstructedColor, deconstructedHorsePower) = _car;

        //Assert
        Assert.AreEqual(_car.Color, deconstructedColor);
        Assert.AreEqual(_car.BuildYear, deconstructedBuildYear);
        Assert.AreEqual(_car.HorsePower, deconstructedHorsePower);
    }

    [TestMethod]
    public void Deconstruct_CarClass_2Values()
    {
        //Act
        var (deconstructedBuildYear, deconstructedColor) = _car;

        //Assert
        Assert.AreEqual(_car.Color, deconstructedColor);
        Assert.AreEqual(_car.BuildYear, deconstructedBuildYear);
    }

    [TestMethod]
    public void Deconstruct_PlaneClass()
    {
        //Act
        var (altitude, speed) = _plane;

        //Assert
        Assert.AreEqual(_plane.Altitude, altitude);
        Assert.AreEqual(_plane.Speed, speed);
    }

    [TestMethod]
    public void Deconstruct_LicensePlateRecord()
    {
        //Act
        var (countryCode, plateNumber) = _licensePlate;

        //Assert
        Assert.AreEqual(_licensePlate.CountryCode, countryCode);
        Assert.AreEqual(_licensePlate.PlateNumber, plateNumber);
    }

    [TestMethod]
    public void Deconstruct_KeyValuePairs()
    {
        //Arrange
        var dictionary = Enumerable.Range(0, 10).ToDictionary(i => i, i => $"Number {i}");

        //Act
        foreach (var (key, value) in dictionary) //Perform Deconstruction, for each KeyValuePair in the dictionary
        {
            //Assert
            Assert.IsTrue(dictionary.TryGetValue(key, out var lookedUp));
            Assert.AreEqual(lookedUp, value);
        }
    }
}