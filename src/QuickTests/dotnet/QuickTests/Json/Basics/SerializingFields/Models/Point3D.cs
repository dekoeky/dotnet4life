using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.SerializingFields.Models;
public struct Point3D
{
    [JsonPropertyName("X")] public double X;
    [JsonPropertyName("Y")] public double Y;
    [JsonPropertyName("Z")] public double Z;
}