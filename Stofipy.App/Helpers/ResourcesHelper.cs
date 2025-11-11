using Stofipy.App.Resources;

namespace Stofipy.App.Helpers;

public static class ResourcesHelper
{
    private static readonly Color DefaultColor = Colors.Transparent;

    public static T? GetResourceValue<T>(ResourceKeys resourceKey)
        => GetResourceValue<T>(resourceKey.ToString());

    public static T GetResourceValue<T>(ResourceKeys resourceKey, T defaultValue)
    {
        return TryGetResourceValue<T>(resourceKey.ToString(), out var value)
            ? value ?? defaultValue
            : defaultValue;
    }

    public static Color? GetColorByName(string colorName)
        => GetResourceValue<Color>(colorName);

    public static Color GetResourceValue(ColorKeys colorKey)
        => GetResourceValue<Color>(colorKey.ToString()) ?? DefaultColor;


    public static bool TryGetResourceValue(ColorKeys colorKey, out Color colorValue)
    {
        bool succeeded;
        if (TryGetResourceValue(colorKey.ToString(), out Color? colorValueNullable)
            && colorValueNullable is not null)
        {
            colorValue = colorValueNullable;
            succeeded = true;
        }
        else
        {
            colorValue = DefaultColor;
            succeeded = false;
        }

        return succeeded;
    }

    private static T? GetResourceValue<T>(string key)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var resourceValue) is true)
        {
            return (T) resourceValue;
        }

        throw new ArgumentOutOfRangeException(nameof(key), key, null);
    }

    private static bool TryGetResourceValue<T>(string key, out T? value)
    {
        if (Application.Current?.Resources.TryGetValue(key, out var resourceValue) is true
            && resourceValue is T typedValue)
        {
            value = typedValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }
}
