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

namespace SharpBoxes.WPFHelpers.Dialogs;

public enum EInputType
{
    String,
    Float,
    Double,
    Int
}

public class InputTypeToVisibilityConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is EInputType inputType)
        {
            if (parameter is EInputType inputParameterType)
            {
                if (inputType == inputParameterType)
                {
                    return Visibility.Visible;
                }
            }
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}

public class EInputTypeToTypeConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is EInputType inputType)
        {
            return inputType switch
            {
                EInputType.String => typeof(string),
                EInputType.Float => typeof(float),
                EInputType.Double => typeof(double),
                EInputType.Int => typeof(int),
                _ => null
            };
        }
        return null;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        System.Globalization.CultureInfo culture
    )
    {
        if (value is Type type)
        {
            return type switch
            {
                { } when type == typeof(string) => EInputType.String,
                { } when type == typeof(float) => EInputType.Float,
                { } when type == typeof(double) => EInputType.Double,
                { } when type == typeof(int) => EInputType.Int,
                _ => null
            };
        }
        return null;
    }
}

/// <summary>
/// Interaction logic for InputDialog.xaml
/// </summary>
public partial class InputDialog : Window
{
    public InputDialogViewModel inputDialogViewModel;

    public T GetValue<T>()
    {
        return (T)inputDialogViewModel.Value;
    }

    public InputDialog()
    {
        InitializeComponent();
        inputDialogViewModel = new();
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        Close();
    }

    private void okButton_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = inputDialogViewModel;
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (this.DialogResult == null)
        {
            this.DialogResult = false;
        }
    }
}
