using System.Globalization;
using Stofipy.App.Enums;

namespace Stofipy.App.Converters;

public class EnumEqualsConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value?.Equals(parameter) ?? false;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => ((bool)(value ?? FileType.All) ? parameter : Binding.DoNothing)!;
}