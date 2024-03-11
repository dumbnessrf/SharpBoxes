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
/// 一个支持高亮显示的<see cref="TextBlock"/><strong>控件</strong>
/// </summary>
[TemplatePart(Name = TEXT_DISPLAY_PART_NAME, Type = typeof(TextBlock))]
public class HighlightTextBlock : Control
{
    private const string TEXT_DISPLAY_PART_NAME = "PART_TextDisplay";

    private TextBlock _displayTextBlock;

    /// <summary>
    /// 高亮的文本
    /// </summary>
    public static readonly DependencyProperty HighlightTextProperty = DependencyProperty.Register(
        "HighlightText",
        typeof(string),
        typeof(HighlightTextBlock),
        new PropertyMetadata(string.Empty, OnHighlightTextPropertyChanged)
    );

    /// <summary>
    /// 高亮的文本
    /// </summary>
    public string HighlightText
    {
        get { return (string)GetValue(HighlightTextProperty); }
        set { SetValue(HighlightTextProperty, value); }
    }

    /// <summary>
    /// 显示的文本
    /// </summary>
    public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(
        typeof(HighlightTextBlock),
        new PropertyMetadata(string.Empty, OnHighlightTextPropertyChanged)
    );

    /// <summary>
    /// 显示的文本
    /// </summary>
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    /// <summary>
    /// 高亮字体的样式
    /// </summary>
    public static readonly DependencyProperty HighlightRunStyleProperty =
        DependencyProperty.Register(
            "HighlightRunStyle",
            typeof(Style),
            typeof(HighlightTextBlock),
            new PropertyMetadata(CreateDefaultHighlightRunStyle())
        );

    private static object CreateDefaultHighlightRunStyle()
    {
        Style style = new Style(typeof(Run));
        style.Setters.Add(new Setter(Run.BackgroundProperty, Brushes.Yellow));

        return style;
    }

    /// <summary>
    /// 高亮字体的样式
    /// <para></para>优先级为<para></para>
    /// <list type="bullet">
    /// <item><description><see cref="BackgroundHighlight"/> and <see cref="ForegroundHighlight"/></description></item>
    /// <item><description><see cref="HighlightRunStyle"/></description></item>
    /// </list>
    /// </summary>
    public Style HighlightRunStyle
    {
        get { return (Style)GetValue(HighlightRunStyleProperty); }
        set { SetValue(HighlightRunStyleProperty, value); }
    }

    /// <summary>
    /// 高亮字体的前景色
    /// <remarks>默认为黑色</remarks>
    /// </summary>
    public Brush ForegroundHighlight
    {
        get { return (Brush)GetValue(ForegroundHighlightProperty); }
        set { SetValue(ForegroundHighlightProperty, value); }
    }

    /// <summary>
    /// 高亮字体的前景色
    /// </summary>
    public static readonly DependencyProperty ForegroundHighlightProperty =
        DependencyProperty.Register(
            "ForegroundHighlight",
            typeof(Brush),
            typeof(HighlightTextBlock),
            new PropertyMetadata(Brushes.Black)
        );

    /// <summary>
    /// 高亮字体的背景色
    /// <remarks>默认为黄色</remarks>
    /// </summary>
    public Brush BackgroundHighlight
    {
        get { return (Brush)GetValue(BackgroundHighlightProperty); }
        set { SetValue(BackgroundHighlightProperty, value); }
    }

    /// <summary>
    /// 高亮字体的背景色
    /// </summary>
    public static readonly DependencyProperty BackgroundHighlightProperty =
        DependencyProperty.Register(
            "BackgroundHighlight",
            typeof(Brush),
            typeof(HighlightTextBlock),
            new PropertyMetadata(Brushes.Yellow)
        );

    static HighlightTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(HighlightTextBlock),
            new FrameworkPropertyMetadata(typeof(HighlightTextBlock))
        );
    }

    /// <summary>
    /// 发生在应用模板时，相当于界面加载时
    /// </summary>
    public override void OnApplyTemplate()
    {
        _displayTextBlock = Template.FindName(TEXT_DISPLAY_PART_NAME, this) as TextBlock;
        UpdateHighlightDisplay();

        base.OnApplyTemplate();
    }

    private static void OnHighlightTextPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (d is HighlightTextBlock highlightTextBlock)
        {
            highlightTextBlock.UpdateHighlightDisplay();
        }
    }

    private void UpdateHighlightDisplay()
    {
        if (_displayTextBlock != null)
        {
            _displayTextBlock.Inlines.Clear();

            int highlightTextLength = HighlightText is null ? 0 : HighlightText.Length;
            if (highlightTextLength == 0)
            {
                _displayTextBlock.Text = Text;
            }
            else
            {
                for (int i = 0; i < Text.Length; i++)
                {
                    if (i + highlightTextLength > Text.Length)
                    {
                        _displayTextBlock.Inlines.Add(new Run(Text.Substring(i)));
                        break;
                    }

                    int nextHighlightTextIndex = Text.IndexOf(HighlightText, i);
                    if (nextHighlightTextIndex == -1)
                    {
                        _displayTextBlock.Inlines.Add(new Run(Text.Substring(i)));
                        break;
                    }

                    _displayTextBlock.Inlines.Add(
                        new Run(Text.Substring(i, nextHighlightTextIndex - i))
                    );
                    _displayTextBlock.Inlines.Add(CreateHighlightedRun(HighlightText));

                    i = nextHighlightTextIndex + highlightTextLength - 1;
                }
            }
        }
    }

    private Run CreateHighlightedRun(string text)
    {
        if (BackgroundHighlight is not null && ForegroundHighlight is not null)
        {
            return new Run(text)
            {
                Background = BackgroundHighlight,
                Foreground = ForegroundHighlight
            };
        }
        else if (HighlightRunStyle is not null)
        {
            return new Run(text) { Style = HighlightRunStyle };
        }
        else
        {
            return new Run(text) { FontWeight = FontWeights.Bold };
        }
    }
}
