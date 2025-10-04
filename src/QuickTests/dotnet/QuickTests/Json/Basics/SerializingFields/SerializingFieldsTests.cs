using QuickTests.Json.Basics.SerializingFields.Models;
using System.Text.Json;

namespace QuickTests.Json.Basics.SerializingFields;

/// <summary>
/// Fields are not serialized by default. These tests demonstrate that, and how you can serialize them if needed.
/// </summary>
[TestClass]
public class SerializingFieldsTests
{
    //Data
    private const double ExpectedX = 1.1;
    private const double ExpectedY = 2.2;
    private const double ExpectedZ = 3.3;
    private readonly Point3D _point = new() { X = ExpectedX, Y = ExpectedY, Z = ExpectedZ, };
    private readonly Point3DWithJsonInclude _pointWithJsonInclude = new() { X = ExpectedX, Y = ExpectedY, Z = ExpectedZ, };

    //Json Serializer Options
    private readonly JsonSerializerOptions _optionsSimple = new()
    {
        WriteIndented = true,
    };
    private readonly JsonSerializerOptions _optionsWithIncludeFields = new()
    {
        WriteIndented = true,
        IncludeFields = true,   //This option specifies to serialize fields by default
    };

    private const string EmptyJson = "{}";
    private const string ExpectedJson = """
                                {
                                  "X": 1.1,
                                  "Y": 2.2,
                                  "Z": 3.3
                                }
                                """;

    [TestMethod]
    public void FieldsAreNotSerializedByDefault()
    {
        //Act
        var json = JsonSerializer.Serialize(_point, _optionsSimple);

        //Assert
        Console.WriteLine(json);
        Assert.AreEqual(EmptyJson, json);
    }

    [TestMethod]
    public void FieldsAreNotDeserializedByDefault()
    {
        //Act
        var result = JsonSerializer.Deserialize<Point3D>(ExpectedJson, _optionsSimple);

        //Assert
        Assert.AreEqual(0, result.X);
        Assert.AreEqual(0, result.Y);
        Assert.AreEqual(0, result.Z);
    }



    [TestMethod]
    public void FieldsWithJsonIncludeAreSerializedByDefault()
    {
        //Act
        var json = JsonSerializer.Serialize(_pointWithJsonInclude, _optionsSimple);

        //Assert
        Console.WriteLine(json);
        Assert.AreEqual(ExpectedJson.ReplaceLineEndings(), json.ReplaceLineEndings());
    }

    [TestMethod]
    public void FieldsWithJsonIncludeAreDeserializedByDefault()
    {
        //Act
        var result = JsonSerializer.Deserialize<Point3DWithJsonInclude>(ExpectedJson, _optionsSimple);

        //Assert
        Assert.AreEqual(ExpectedX, result.X);
        Assert.AreEqual(ExpectedY, result.Y);
        Assert.AreEqual(ExpectedZ, result.Z);
    }



    [TestMethod]
    public void FieldsAreSerializedWhenSpecifiedInOptions()
    {
        //Act
        var json = JsonSerializer.Serialize(_point, _optionsWithIncludeFields);

        //Assert
        Console.WriteLine(json);
        Assert.AreEqual(ExpectedJson.ReplaceLineEndings(), json.ReplaceLineEndings());
    }

    [TestMethod]
    public void FieldsAreDeserializedWhenSpecifiedInOptions()
    {
        //Act
        var result = JsonSerializer.Deserialize<Point3D>(ExpectedJson, _optionsWithIncludeFields);

        //Assert
        Assert.AreEqual(ExpectedX, result.X);
        Assert.AreEqual(ExpectedY, result.Y);
        Assert.AreEqual(ExpectedZ, result.Z);
    }
}