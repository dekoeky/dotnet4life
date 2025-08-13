using System.Text.Json.Serialization;

namespace QuickTests.Json.Basics.SerializingFields.Models;

public struct Point3DWithJsonInclude
{
    [JsonInclude][JsonPropertyName("X")] public double X;
    [JsonInclude][JsonPropertyName("Y")] public double Y;
    [JsonInclude][JsonPropertyName("Z")] public double Z;
}