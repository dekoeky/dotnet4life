using System.Numerics;

namespace QuickTests.DataTypes.Comparisons.Custom.TestModels;

public class OverridableIo<T> where T : struct, IEquatable<T>, IEqualityOperators<T, T, bool>
{
    private T _actualValue;
    private T _finalValue;
    private T _overrideValue;
    private bool _isOverriden;

    public bool IsOverriden
    {
        get => _isOverriden;
        set
        {
            if (_isOverriden == value) return;
            _isOverriden = value;
            UpdateFinalValue();
        }
    }

    public T OverrideValue
    {
        get => _overrideValue;
        set
        {
            if (_overrideValue == value) return;
            _overrideValue = value;
            UpdateFinalValue();
        }
    }

    public T ActualValue
    {
        get => _actualValue;
        set
        {
            if (_actualValue == value) return;
            _actualValue = value;
            UpdateFinalValue();
        }
    }

    public T FinalValue => _finalValue;

    private void UpdateFinalValue() => _finalValue = _isOverriden ? _overrideValue : _actualValue;
}