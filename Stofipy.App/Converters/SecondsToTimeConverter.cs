using System.Globalization;

namespace Stofipy.App.Converters;

public class SecondsToTimeConverter : IValueConverter
{
    public object Convert(object? value, Type? targetType, object? parameter, CultureInfo culture)
    {
        if (value is int totalSeconds)
        {
            var time = TimeSpan.FromSeconds(totalSeconds);
            return $"{(int)time.TotalMinutes}:{time.Seconds:D2}"; // e.g., 2:05
        }

        return "0:00";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Only used one-way
    }
}