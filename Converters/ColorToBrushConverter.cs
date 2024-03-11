using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SharpBoxes.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
        )
        {
            var color = (System.Windows.Media.Color)value;
            return new System.Windows.Media.SolidColorBrush(color);
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
        )
        {
            var brush = (System.Windows.Media.Brush)value;
            return ((System.Windows.Media.SolidColorBrush)brush).Color;
        }
    }
}
