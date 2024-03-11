using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpBoxes.WPFHelpers.UserControls;

/// <summary>
/// 一个支持占位符的<see cref="TextBox"/><strong>控件</strong>
/// </summary>
public class PlaceholderTextBoxControl : TextBox
{
    /// <summary>
    /// 是否为空
    /// </summary>
    public static readonly DependencyProperty IsEmptyProperty = DependencyProperty.Register(
        "IsEmpty",
        typeof(bool),
        typeof(PlaceholderTextBoxControl),
        new PropertyMetadata(false)
    );

    /// <summary>
    /// 是否为空
    /// </summary>
    public bool IsEmpty
    {
        get { return (bool)GetValue(IsEmptyProperty); }
        private set { SetValue(IsEmptyProperty, value); }
    }

    /// <summary>
    /// 占位符
    /// </summary>
    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }

    /// <summary>
    /// 占位符
    /// </summary>
    // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        "Placeholder",
        typeof(string),
        typeof(PlaceholderTextBoxControl),
        new PropertyMetadata("")
    );

    static PlaceholderTextBoxControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(PlaceholderTextBoxControl),
            new FrameworkPropertyMetadata(typeof(PlaceholderTextBoxControl))
        );
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInitialized(EventArgs e)
    {
        UpdateIsEmpty();
        base.OnInitialized(e);
    }

    /// <summary>
    /// 文本改变
    /// </summary>
    /// <param name="e"></param>
    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        UpdateIsEmpty();
        base.OnTextChanged(e);
    }

    private void UpdateIsEmpty()
    {
        IsEmpty = string.IsNullOrEmpty(Text);
    }
}
