using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SharpBoxes.WPFHelpers.Dialogs
{
    /// <summary>
    /// 提供各种消息框的静态类。
    /// </summary>
    public static class MessageBoxes
    {
        /// <summary>
        /// 显示一个错误消息框。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Error"。</param>
        public static void Error(string message, string caption = "Error")
        {
            System.Windows.MessageBox.Show(
                message,
                caption,
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error
            );
        }

        /// <summary>
        /// 显示一个警告消息框。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Warning"。</param>
        public static void Warning(string message, string caption = "Warning")
        {
            System.Windows.MessageBox.Show(
                message,
                caption,
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning
            );
        }

        /// <summary>
        /// 显示一个信息消息框。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Information"。</param>
        public static void Information(string message, string caption = "Information")
        {
            System.Windows.MessageBox.Show(
                message,
                caption,
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information
            );
        }

        /// <summary>
        /// 显示一个确认消息框，并返回用户的选择。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Confirm"。</param>
        /// <returns>如果用户选择"Yes"，则返回true，否则返回false。</returns>
        public static bool Confirm(string message, string caption = "Confirm")
        {
            return System.Windows.MessageBox.Show(
                    message,
                    caption,
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Question
                ) == System.Windows.MessageBoxResult.Yes;
        }

        /// <summary>
        /// 显示一个带有"Yes"、"No"和"Cancel"按钮的消息框，并返回用户的选择。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Confirm"。</param>
        /// <returns>用户的选择，为<see cref="MessageBoxResult"/>枚举的一个值。</returns>
        public static MessageBoxResult YesNoCancel(string message, string caption = "Confirm")
        {
            return System.Windows.MessageBox.Show(
                message,
                caption,
                System.Windows.MessageBoxButton.YesNoCancel,
                System.Windows.MessageBoxImage.Question
            );
        }

        /// <summary>
        /// 显示一个带有"OK"和"Cancel"按钮的消息框，并返回用户的选择。
        /// </summary>
        /// <param name="message">要显示的消息。</param>
        /// <param name="caption">消息框的标题，默认为"Confirm"。</param>
        /// <returns>用户的选择，为<see cref="MessageBoxResult"/>枚举的一个值。</returns>
        public static MessageBoxResult OkCancel(string message, string caption = "Confirm")
        {
            return System.Windows.MessageBox.Show(
                message,
                caption,
                System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Question
            );
        }
    }
}
