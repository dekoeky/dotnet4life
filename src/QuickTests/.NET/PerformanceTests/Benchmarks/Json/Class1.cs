using BenchmarkDotNet.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PerformanceTests.Benchmarks.Json;

//[SimpleJob(warmupCount: 1, iterationCount:1)]
[SimpleJob]
[MemoryDiagnoser]
public class WWW
{
    MyPoco data;
    private MyPoco data2;

    public WWW()
    {
        data = new MyPoco();
        data2 = new MyPoco();
        data2.SomeFloatDigitalProp03.OverrideValue = 999f;
    }
    private JsonSerializerOptions options = new(JsonSerializerOptions.Default)
    {
        IncludeFields = true,
        WriteIndented = true,
    };

    //[Benchmark]
    public string str()
    {
        return JsonSerializer.Serialize(data, options);
        //Console.WriteLine(json);
        //return json;

    }
    //[Benchmark]
    public JsonDocument doc()
    {
        return JsonSerializer.SerializeToDocument(data, options);
        //Console.WriteLine(json);
        //return json;
    }

    [Benchmark]
    public string test()
    {
        var a = JsonSerializer.SerializeToDocument(data, options);
        var b = JsonSerializer.SerializeToDocument(data2, options);

        var e1 = a.RootElement.EnumerateObject();
        e1.Reset();
        var e2 = b.RootElement.EnumerateObject();
        e2.Reset();

        Dictionary<string, JsonElement> changed = new Dictionary<string, JsonElement>();
        while (true)
        {
            var next1 = e1.MoveNext();
            var next2 = e2.MoveNext();

            if (next1 != next2) throw new InvalidOperationException();
            if (!e1.Current.NameEquals(e2.Current.Name)) throw new InvalidOperationException();

            if (JsonElement.DeepEquals(e1.Current.Value, e2.Current.Value))
            {
                continue;
            }


            changed.Add(e2.Current.Name, e2.Current.Value);
        }

        var json = JsonSerializer.Serialize(changed, options);
        Console.WriteLine(json);
        return json;
    }
}

public class MyPoco
{
    public float SomeFloatField1 = 3.14f;
    public bool SomeBoolField1 = true;

    public DigitalIo<bool> someBool1 = new(true);
    public DigitalIo<bool> someBool2 = new(true);
    public DigitalIo<bool> someBool3 = new(true);
    public DigitalIo<bool> someBool4 = new(true);
    public DigitalIo<bool> someBool5 = new(true);
    public DigitalIo<bool> someBool6 = new(true);
    public DigitalIo<bool> someBool7 = new(true);
    public DigitalIo<bool> someBool8 = new(true);
    public DigitalIo<bool> someBool9 = new(true);
    public DigitalIo<bool> someBool10 = new(true);
    public DigitalIo<bool> someBool11 = new(true);
    public DigitalIo<bool> someBool12 = new(true);
    public DigitalIo<bool> someBool13 = new(true);
    public DigitalIo<float> SomeFloatDigitalProp01 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp02 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp03 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp04 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp05 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp06 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp07 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp08 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp09 { get; set; } = new(55.0f);
    public DigitalIo<float> SomeFloatDigitalProp10 { get; set; } = new(55.0f);
}

public class DigitalIoActualValueJsonConverter<T> : JsonConverter<DigitalIo<T>> where T : struct, IEquatable<T>
{
    public override DigitalIo<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var actualValue = JsonSerializer.Deserialize<T>(ref reader, options);

        return new DigitalIo<T>(actualValue);
    }

    public override void Write(Utf8JsonWriter writer, DigitalIo<T> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.ActualValue, options);
    }
}
public class DigitalIoActualValueJsonConverterFactory : JsonConverterFactory
{
    //TODO: Make Generic Class?
    public override bool CanConvert(Type typeToConvert)
    {
        // Check if the type is DigitalInput<T>
        if (typeToConvert.IsDigitalIo())
        {
            //TODO: Unit Test
            return true;
        }




        return false;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        // Get the generic type argument of DigitalInput<T>
        var genericType = typeToConvert.GetGenericArguments()[0];

        // Create an instance of the generic JsonConverter for the specific generic type
        var converter = (JsonConverter)Activator.CreateInstance(
            typeof(DigitalIoActualValueJsonConverter<>).MakeGenericType(genericType))!;

        return converter;
    }
}
public static class DigitalIoReflectionExtensions
{
    public static bool IsDigitalIo(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(DigitalIo<>);
    public static bool IsDigitalIo(this Type type, out Type genericTypeArgument)
    {
        if (!IsDigitalIo(type))
        {
            genericTypeArgument = null!;
            return false;
        }

        genericTypeArgument = type.GenericTypeArguments[0];
        return true;
    }
}
/// <summary>
/// Represents a Unity Prefab Input Or Output, which can be overridden by the UI
/// </summary>
/// <typeparam name="T"></typeparam>
[JsonConverter(typeof(DigitalIoActualValueJsonConverterFactory))]
public class DigitalIo<T>
    where T : struct, IEquatable<T>
{
    //TODO: Rename DigitalIo. More logical name would be something like OverridableIo
    //todo: some function to extract value from json ?

    #region Private Fields

    private bool _isOverridden;
    private T _overrideValue;
    private T _actualValue;
    private T _finalValue;

    #endregion

    #region Constructors

    public DigitalIo()
    {
        // _isOverridden : Defaults to false;
        // _actualValue = default;
        // _overrideValue = default;
        // _finalValue = default;
    }

    public DigitalIo(T initialValue)
    {
        //IsOverridden is assumed false here
        _finalValue = _actualValue = initialValue;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The un-altered un-overridden value.
    /// </summary>
    public T ActualValue
    {
        get => _actualValue;
        set
        {
            if (_finalValue.Equals(value))
                return;

            _actualValue = value;
            OnPropertyChanged();
            UpdateFinalValue();
        }
    }

    /// <summary>
    /// The value that is used as override.
    /// </summary>
    public T OverrideValue
    {
        get => _overrideValue;
        set
        {
            if (_finalValue.Equals(value))
                return;

            _overrideValue = value;
            OnPropertyChanged();
            UpdateFinalValue();
        }
    }

    /// <summary>
    /// Whether the <see cref="ActualValue"/> is overridden.
    /// </summary>
    public bool IsOverridden
    {
        get => _isOverridden;
        set
        {
            if (_finalValue.Equals(value))
                return;

            _isOverridden = value;
            OnPropertyChanged();
            UpdateFinalValue();
        }
    }
    /// <summary>
    /// <see cref="ActualValue"/> when <see cref="IsOverridden"/> is <c>false</c>
    /// or <see cref="OverrideValue"/> when <see cref="IsOverridden"/> is <c>true</c>
    /// </summary>
    public T FinalValue
    {
        get => _finalValue;
        private set
        {
            if (_finalValue.Equals(value))
                return;

            _finalValue = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Updates the state of FinalValue.
    /// </summary>
    private void UpdateFinalValue() => FinalValue = _isOverridden ? _overrideValue : _actualValue;

    /// <summary>
    /// Convenience method for applying an override.
    /// </summary>
    public void Override(T overrideValue)
    {
        OverrideValue = overrideValue;
        IsOverridden = true;
    }

    /// <summary>
    /// Convenience method for removing an override
    /// </summary>
    public void Restore()
    {
        IsOverridden = false;
    }

    #endregion

    #region Operators

    public static implicit operator DigitalIo<T>(T input) => new(input);
    public static implicit operator T(DigitalIo<T> io) => io.FinalValue;

    #endregion

    #region OnPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}