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

namespace SharpBoxes.WPFHelpers.UserControls
{
    /// <summary>
    /// 一个支持绑定的<see cref="PasswordBox"/><strong>控件</strong>
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        /// <summary>
        /// 新的Padding，支持占位符
        /// </summary>
        public Thickness PaddingNew
        {
            get { return _padding; }
            set
            {
                _padding = value;
                if (passwordBox != null)
                {
                    passwordBox.Padding = value;
                    textblock.Padding = value;
                }
            }
        }

        /// <summary>
        /// 新的Margin，支持占位符
        /// </summary>
        public Thickness MarginNew
        {
            get { return _marginNew; }
            set
            {
                _marginNew = value;
                if (this != null)
                {
                    this.Margin = value;
                }
            }
        }
        private bool _isPasswordChanging;
        private Thickness _padding = new Thickness(0);
        private Thickness _marginNew = new Thickness(0);

        /// <summary>
        /// Password的DP
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            "Password",
            typeof(string),
            typeof(BindablePasswordBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                PasswordPropertyChanged,
                null,
                false,
                UpdateSourceTrigger.PropertyChanged
            )
        );

        private static void PasswordPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        /// <summary>
        /// Password的Property
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// 占位符
        /// </summary>
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        /// <summary>
        /// 占位符
        /// </summary>
        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register(
            "PlaceHolder",
            typeof(string),
            typeof(BindablePasswordBox),
            new PropertyMetadata(string.Empty, PlaceHolderChanged)
        );

        private static void PlaceHolderChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePlaceHolder();
            }
        }

        private void UpdatePlaceHolder()
        {
            if (string.IsNullOrWhiteSpace(PlaceHolder) || string.IsNullOrEmpty(Password) is false)
            {
                if (textblock != null)
                    textblock.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (textblock != null)
                {
                    textblock.Visibility = Visibility.Visible;
                    textblock.Text = PlaceHolder;
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                passwordBox.Password = Password;
            }

            UpdatePlaceHolder();
        }
    }
}
