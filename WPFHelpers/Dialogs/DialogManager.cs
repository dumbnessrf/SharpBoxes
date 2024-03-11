using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.WPFHelpers.Dialogs
{
    /// <summary>
    /// Modal对话框的管理器
    /// </summary>
    public static class DialogManager
    {
        /// <summary>
        /// <inheritdoc cref="ShowInputDialog(string,double,double,EInputType,out object,string)"/>
        /// </summary>
        /// <param name="description">输入描述</param>
        /// <param name="value">用户输入返回的值</param>
        /// <param name="title">输入对话框的标题</param>
        /// <typeparam name="TValue">输入参数的类型</typeparam>
        /// <returns></returns>
        public static bool ShowInputDialog<TValue>(
            string description,
            out TValue value,
            string title = "Input"
        )
        {
            EInputTypeToTypeConverter inputTypeToTypeConverter = new EInputTypeToTypeConverter();
            var dialog = new InputDialog();
            dialog.inputDialogViewModel.Caption = title;
            dialog.inputDialogViewModel.Description = description;
            var inputType = (EInputType)
                (inputTypeToTypeConverter.ConvertBack(typeof(TValue), null, null, null))!;

            dialog.inputDialogViewModel.InputType = inputType;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                value = dialog.GetValue<TValue>();
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// <inheritdoc cref="ShowInputDialog(string,double,double,EInputType,out object,string)"/>
        /// </summary>
        /// <param name="description">输入描述</param>
        /// <param name="minValue">输入最小值</param>
        /// <param name="maxValue">输入最大值</param>
        /// <param name="value">用户输入返回的值</param>
        /// <param name="title">输入对话框的标题</param>
        /// <typeparam name="TValue">输入参数的类型</typeparam>
        /// <returns></returns>
        public static bool ShowInputDialog<TValue>(
            string description,
            double minValue,
            double maxValue,
            out TValue value,
            string title = "Input"
        )
        {
            var inputTypeToTypeConverter = new EInputTypeToTypeConverter();
            var dialog = new InputDialog();
            dialog.inputDialogViewModel.Caption = title;
            dialog.inputDialogViewModel.Description = description;
            dialog.inputDialogViewModel._NumericRange = new NumericRange(minValue, maxValue);
            var inputType = (EInputType)
                (inputTypeToTypeConverter.ConvertBack(typeof(TValue), null, null, null))!;

            dialog.inputDialogViewModel.InputType = inputType;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                value = dialog.GetValue<TValue>();
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// <inheritdoc cref="ShowInputDialog(string,double,double,EInputType,out object,string)"/>
        /// </summary>
        /// <param name="description">输入描述</param>
        /// <param name="inputType">输入参数的类型，见<see cref="EInputType"/></param>
        /// <param name="value">用户输入返回的值</param>
        /// <param name="title">输入对话框的标题</param>
        /// <returns></returns>
        public static bool ShowInputDialog(
            string description,
            EInputType inputType,
            out object value,
            string title = "Input"
        )
        {
            var dialog = new InputDialog();
            dialog.inputDialogViewModel.Caption = title;
            dialog.inputDialogViewModel.Description = description;

            dialog.inputDialogViewModel.InputType = inputType;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                value = dialog.GetValue<object>();
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        ///  弹出输入对话框
        /// <example>
        /// <code>
        /// SharpBoxes.WPFHelpers.Dialogs.DialogManager.ShowInputDialog(
        ///     "Please input your value.\n"
        ///     + "if input type is integer or double, float, the limited range is 0-100.\n"
        ///     + "try input value that out of range, then will show error message.",
        ///     0,
        ///     100,
        ///     InputType,
        ///     out var value,
        ///     "Password"
        /// );
        /// SharpBoxes.WPFHelpers.Dialogs.MessageBoxes.Information(
        /// $"Input: {value}",
        /// "Input Dialog Input");
        /// </code>
        ///</example>
        /// </summary>
        /// <param name="description">输入描述</param>
        /// <param name="minValue">输入最小值</param>
        /// <param name="maxValue">输入最大值</param>
        /// <param name="inputType">输入参数的类型，见<see cref="EInputType"/></param>
        /// <param name="value">用户输入返回的值</param>
        /// <param name="title">输入对话框的标题</param>
        /// <returns></returns>
        public static bool ShowInputDialog(
            string description,
            double minValue,
            double maxValue,
            EInputType inputType,
            out object value,
            string title = "Input"
        )
        {
            var dialog = new InputDialog();
            dialog.inputDialogViewModel.Caption = title;
            dialog.inputDialogViewModel.Description = description;
            dialog.inputDialogViewModel._NumericRange = new NumericRange(minValue, maxValue);
            dialog.inputDialogViewModel.InputType = inputType;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                value = dialog.GetValue<object>();
                return true;
            }
            value = default;
            return false;
        }
    }
}
