using System;
using System.Collections;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Stofipy.App.Converters
{
    public class MultiIndexOfConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[0] is not object currentItem || values[1] is not IEnumerable list)
                return "?";

            int index = 0;
            foreach (var item in list)
            {
                if (Equals(item, currentItem))
                    return (index + 1).ToString();
                index++;
            }

            return "?";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
