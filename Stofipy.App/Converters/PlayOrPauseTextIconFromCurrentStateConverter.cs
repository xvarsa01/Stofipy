using System.Globalization;

namespace Stofipy.App.Converters;

public class PlayOrPauseTextIconFromCurrentStateConverter () : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is [bool isNowPlayingComponent, bool isSomethingPlaying])
        {
            return isNowPlayingComponent && isSomethingPlaying ? "||" : "▶";
        }

        return "";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}