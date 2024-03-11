using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SharpBoxes.Converters
{
    /// <summary>
    /// 字符串到SolidColorBrush转换器
    /// <example>
    /// 下面示例演示了如何使用StringToSolidBrushConverter转换器，只需要在XAML中声明即可使用。
    /// <code>
    /// &lt;conv:StringToSolidBrushConverter x:Key="StringToSolidBrushConverter" /&gt;
    ///
    /// &lt;Label
    ///     Height="30"
    ///     Margin="8"
    ///     Background="{Binding DisplayColor, Converter={StaticResource StringToSolidBrushConverter}}"
    /// /&gt;
    /// </code>
    /// string displayColor = "#FFEC1313";
    /// </example>
    /// </summary>
    public class StringToSolidBrushConverter : IValueConverter
    {
        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">参数</param>
        /// <param name="culture">本地化信息</param>
        /// <returns>转换后的SolidColorBrush</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)ColorConverter.ConvertFromString(value.ToString());
            return new SolidColorBrush(color);
        }

        /// <summary>
        /// 反转换方法
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="parameter">参数</param>
        /// <param name="culture">本地化信息</param>
        /// <returns>转换后的字符串</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            var brush = value as SolidColorBrush;

            return brush.Color.ToString();
        }
    }
}
