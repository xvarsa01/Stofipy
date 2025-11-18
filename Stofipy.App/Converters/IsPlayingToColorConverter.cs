using System.Globalization;
using Stofipy.App.Helpers;
using Stofipy.App.Resources;

namespace Stofipy.App.Converters;

public class IsPlayingToColorConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        var standard = ResourcesHelper.GetResourceValue(ColorKeys.Gray7Color);
        var playing = ResourcesHelper.GetResourceValue(ColorKeys.Brand1ColorLight);
        return value is true ? playing : standard;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Only used one-way
    }
}