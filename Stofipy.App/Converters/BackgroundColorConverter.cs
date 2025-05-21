using System.Globalization;
using Stofipy.BL.Models;

namespace Stofipy.App.Converters;

public class FileBackgroundColorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 ||
            values[0] is not bool isSelected ||
            values[1] is not bool isHovered)
            return "Transparent";

        return isSelected ? "#805a5a5a" : (isHovered ? "#802a2a2a" : "Transparent");
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}