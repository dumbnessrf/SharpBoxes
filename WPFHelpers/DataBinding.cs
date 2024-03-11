using System.Windows;
using System.Windows.Data;

namespace SharpBoxes.WPFHelpers
{
    /// <summary>
    /// WPF的<see cref="System.Windows.Data.Binding"/>相关帮助类
    /// <para></para>
    /// 例如：将<see cref="System.Windows.Controls.TextBox"/>的<see cref="System.Windows.Controls.TextBox.Text"/>属性与<see cref="System.Windows.Controls.Slider"/>的<see cref="System.Windows.Controls.Slider.Value"/>属性绑定
    /// </summary>
    public static class DataBindingHelper
    {
        /// <summary>
        /// 对象属性绑定
        /// <para>示例代码
        ///<code>
        /// <example>
        /// DataBindingHelper.Bind(data,nameof(Name),TextBox.TextProperty,txtBlock);
        /// </example>
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="t">要绑定的实体类</param>
        /// <param name="propertyName">要绑定的属性在实体类中的名称</param>
        /// <param name="dp">要绑定的依赖属性，如<see cref="System.Windows.Controls.TextBlock"/>的<see cref="System.Windows.Controls.TextBlock.TextProperty"/></param>
        /// <param name="target">要绑定的依赖对象，如<see cref="System.Windows.Controls.TextBlock"/></param>
        /// <typeparam name="T">实体类类型</typeparam>
        public static void Bind<T>(
            T t,
            string propertyName,
            DependencyProperty dp,
            DependencyObject target
        )
        {
            Binding myBinding = new Binding(propertyName);
            myBinding.Source = t;
            BindingOperations.SetBinding(target, dp, myBinding);
        }

        /// <summary>
        /// Removes a binding from a target object and its dependency property.
        /// </summary>
        /// <param name="dp">The dependency property from which to remove the binding.</param>
        /// <param name="target">The target object from which to remove the binding.</param>
        public static void UndoBind(DependencyProperty dp, DependencyObject target)
        {
            BindingOperations.ClearBinding(target, dp);
        }
    }
}
