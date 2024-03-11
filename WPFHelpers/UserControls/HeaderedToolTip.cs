using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SharpBoxes.WPFHelpers.UserControls;

/// <summary>
/// 一个带有标题和其他自定义的高自由度的<see cref="ToolTip"/>
/// </summary>
    [TemplatePart(Name = PART_TooltipHeader, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_TooltipContents, Type = typeof(TextBlock))]
    public class HeaderedToolTip : ToolTip
    {
        private const string PART_TooltipHeader = "PART_TooltipHeader";
        private const string PART_TooltipContents = "PART_TooltipContents";
        private const string PART_TooltipRecords = "PART_TooltipRecords";
        private TextBlock _headerBlock = null;
        private TextBlock _contentsBlock = null;
        private ListBox _recordsListBox = null;

        static HeaderedToolTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HeaderedToolTip),
                new FrameworkPropertyMetadata(typeof(HeaderedToolTip))
            );
        }
/// <summary>
/// 初始化<see cref="HeaderedToolTip"/>的新实例
/// </summary>
        public HeaderedToolTip()
        {
            Loaded += (e, s) =>
            {
                if (_headerBlock == null)
                {
                    if (!ApplyTemplate())
                        OnApplyTemplate();
                }
            };
            base.StaysOpen = true;
        }
/// <summary>
/// 标题
/// </summary>
        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
/// <summary>
/// 标题
/// </summary>
        // Using a DependencyProperty as the backing store for HeaderText. This enables animation, styling, binding, etc.
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
            "HeaderText",
            typeof(string),
            typeof(HeaderedToolTip),
            new UIPropertyMetadata(string.Empty)
        );
        /// <summary>
        /// 内容
        /// </summary>
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set => SetValue(ContentTextProperty, value);
        }
/// <summary>
/// 内容
/// </summary>
        // Using a DependencyProperty as the backing store for HeaderText. This enables animation, styling, binding, etc.
        public static readonly DependencyProperty ContentTextProperty = DependencyProperty.Register(
            "ContentText",
            typeof(string),
            typeof(HeaderedToolTip),
            new UIPropertyMetadata(string.Empty)
        );
/// <summary>
/// 除标题和内容外的其他附加信息，显示在标题和内容下面
/// </summary>
        public List<RecordInfo> Records
        {
            get { return (List<RecordInfo>)GetValue(RecordsProperty); }
            set { SetValue(RecordsProperty, value); }
        }
/// <summary>
/// 除标题和内容外的其他附加信息，显示在标题和内容下面
/// </summary>
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecordsProperty = DependencyProperty.Register(
            "Records",
            typeof(List<RecordInfo>),
            typeof(HeaderedToolTip),
            new PropertyMetadata(new List<RecordInfo>())
        );



/// <summary>
/// 应用此模板时，相当于初始化时，获取模板中的控件
/// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _headerBlock = GetTemplateChild(PART_TooltipHeader) as TextBlock;
            _contentsBlock = GetTemplateChild(PART_TooltipContents) as TextBlock;
            _recordsListBox = GetTemplateChild(PART_TooltipRecords) as ListBox;
        }
    }

/// <summary>
/// 附加信息
/// </summary>
/// <param name="message">消息</param>
/// <param name="color">前景色</param>
    public class RecordInfo(string message, Color color)
    {
    /// <summary>
    /// 消息
    /// </summary>
        public string Message { get; set; } = message;
        /// <summary>
        /// 前景色
        /// </summary>
        public Color Color { get; set; } = color;
    }

