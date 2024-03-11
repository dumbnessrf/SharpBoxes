using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SharpBoxes.MarkupExtensions
{
    /// <summary>
    /// 枚举绑定源扩展类，继承自MarkupExtension
    /// <example>
    /// <code>
    /// &lt;ComboBox HorizontalAlignment="Center" VerticalAlignment="Center" MinWidth="150"
    ///       ItemsSource="{Binding Source={local:EnumBindingSource {x:Type local:Status}}}"/&gt;
    ///
    /// public enum Status
    /// {
    ///     Horrible,
    ///     Bad,
    ///     SoSo,
    ///     Good,
    ///     Better,
    ///     Best
    /// }
    /// //也支持自定义显示内容，类似于DisplayName
    /// [<see cref="TypeConverter"/>(typeof(<see cref="EnumDescriptionTypeConverter"/>))]
    /// public enum Status
    /// {
    ///     [<see cref="DescriptionAttribute"/>("This is horrible")]
    ///     Horrible,
    ///     [<see cref="DescriptionAttribute"/>("This is bad")]
    ///     Bad,
    ///     [<see cref="DescriptionAttribute"/>("This is so so")]
    ///     SoSo,
    ///     [<see cref="DescriptionAttribute"/>("This is good")]
    ///     Good,
    ///     [<see cref="DescriptionAttribute"/>("This is better")]
    ///     Better,
    ///     [<see cref="DescriptionAttribute"/>("This is best")]
    ///     Best
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        /// <summary>
        /// 枚举类型私有字段
        /// </summary>
        private Type _enumType;

        /// <summary>
        /// 枚举类型公有属性
        /// </summary>
        public Type EnumType
        {
            get => _enumType;
            set
            {
                // 当新值与旧值不同时
                if (value != _enumType)
                {
                    // 当新值不为空时
                    if (null != value)
                    {
                        // 获取枚举类型，如果是可空类型，则获取其基础类型
                        var enumType = Nullable.GetUnderlyingType(value) ?? value;
                        // 如果获取的类型不是枚举类型，抛出异常
                        if (!enumType.IsEnum)
                        {
                            throw new ArgumentException("Type must bu for an Enum");
                        }
                    }

                    // 更新枚举类型
                    _enumType = value;
                }
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EnumBindingSourceExtension() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

        /// <summary>
        /// 重写ProvideValue方法，提供枚举值
        /// </summary>
        /// <param name="serviceProvider">服务提供者</param>
        /// <returns>枚举值</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // 如果枚举类型为空，返回null
            if (null == _enumType)
            {
                return null;
            }

            // 获取实际的枚举类型，如果是可空类型，则获取其基础类型
            var actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            // 获取枚举的所有值
            var enumValues = Enum.GetValues(actualEnumType);

            // 如果实际的枚举类型与枚举类型相同，返回枚举的所有值
            if (actualEnumType == _enumType)
            {
                return enumValues;
            }

            // 创建一个新的数组，长度为枚举值的长度加一
            var tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            // 将枚举值复制到新的数组中，从索引1开始
            enumValues.CopyTo(tempArray, 1);

            // 返回新的数组
            return tempArray;
        }
    }

    /// <summary>
    /// 枚举绑定源扩展类的可选附加属性，支持自定义显示内容
    /// <example>
    /// <code>
    /// &lt;ComboBox HorizontalAlignment="Center" VerticalAlignment="Center" MinWidth="150"
    ///       ItemsSource="{Binding Source={local:EnumBindingSource {x:Type local:Status}}}"/&gt;
    ///
    /// public enum Status
    /// {
    ///     Horrible,
    ///     Bad,
    ///     SoSo,
    ///     Good,
    ///     Better,
    ///     Best
    /// }
    /// //也支持自定义显示内容，类似于DisplayName
    /// [<see cref="TypeConverter"/>(typeof(<see cref="EnumDescriptionTypeConverter"/>))]
    /// public enum Status
    /// {
    ///     [<see cref="DescriptionAttribute"/>("This is horrible")]
    ///     Horrible,
    ///     [<see cref="DescriptionAttribute"/>("This is bad")]
    ///     Bad,
    ///     [<see cref="DescriptionAttribute"/>("This is so so")]
    ///     SoSo,
    ///     [<see cref="DescriptionAttribute"/>("This is good")]
    ///     Good,
    ///     [<see cref="DescriptionAttribute"/>("This is better")]
    ///     Better,
    ///     [<see cref="DescriptionAttribute"/>("This is best")]
    ///     Best
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type) { }

        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object value,
            Type destinationType
        )
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])
                            fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        return (
                            (attributes.Length > 0)
                            && (!String.IsNullOrEmpty(attributes[0].Description))
                        )
                            ? attributes[0].Description
                            : value.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
