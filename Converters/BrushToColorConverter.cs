using System;
using System.Globalization;
using System.Windows.Data;

namespace SharpBoxes.Converters;

public class BrushToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var brush = (System.Windows.Media.Brush)value;
        return ((System.Windows.Media.SolidColorBrush)brush).Color;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var color = (System.Windows.Media.Color)value;
        return new System.Windows.Media.SolidColorBrush(color);
    }
}
