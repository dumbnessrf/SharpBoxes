using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SharpBoxes.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public System.Windows.Visibility FalserDefaultState { get; set; } =
            System.Windows.Visibility.Collapsed;

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
        )
        {
            if (value is bool)
            {
                return (bool)value ? System.Windows.Visibility.Visible : FalserDefaultState;
            }
            return FalserDefaultState;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture
        )
        {
            if (value is System.Windows.Visibility)
            {
                return (System.Windows.Visibility)value == System.Windows.Visibility.Visible;
            }
            return false;
        }
    }
}
