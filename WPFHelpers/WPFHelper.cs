using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using SharpBoxes.WPFHelpers.UserControls;

namespace SharpBoxes.WPFHelpers
{
    /// <summary>
    /// Wpf相关帮助类
    /// </summary>
    public static class WpfHelper
    {
        /// <summary>
        /// 自动设置<see cref="FrameworkElement.ToolTip"/>属性，并应用样式
        /// <example>
        ///<code>
        ///FrameworkElement element = new Button(); // This could be any UI element that inherits from FrameworkElement
        ///string message = "This is a tooltip message";
        ///string header = "Tooltip Header";
        ///List&lt;RecordInfo&gt; records = new List&lt;RecordInfo&gt;
        ///{
        ///    new RecordInfo { /* Initialize RecordInfo object here */ },
        ///    // Add more RecordInfo objects if needed
        ///};
        ///element.SetToolTip(message, header, records);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="dpobj">要绑定ToolTip的控件UI对象</param>
        /// <param name="message">显示的ToolTip</param>
        /// <param name="header">显示的标题</param>
        /// <param name="records">自定义显示的附加消息</param>
        public static void SetToolTip(
            this FrameworkElement dpobj,
            string message,
            string header = "",
            List<RecordInfo> records = null
        )
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            if (dpobj is null)
            {
                return;
            }
            if (dpobj.ToolTip != null)
            {
                dpobj.ToolTip = null;
            }

            records ??= new List<RecordInfo>();

            dpobj.ToolTip = new HeaderedToolTip()
            {
                HeaderText = header,
                ContentText = message,
                Records = records
            };
            ToolTipService.SetHasDropShadow(dpobj, true);
            ToolTipService.SetShowOnDisabled(dpobj, true);
            ToolTipService.SetInitialShowDelay(dpobj, 500);
        }

        /// <summary>
        /// 清除ToolTip
        /// </summary>
        /// <param name="dpobj"></param>
        public static void ClearToolTip(this FrameworkElement dpobj)
        {
            if (dpobj is null)
            {
                return;
            }

            ToolTipService.SetHasDropShadow(dpobj, false);
            ToolTipService.SetShowOnDisabled(dpobj, false);
            ToolTipService.SetInitialShowDelay(dpobj, 0);
            dpobj.ToolTip = null;
        }

        /// <summary>
        /// 打开文件选择对话框
        /// <code>
        /// var path = SharpBoxes.WPFHelpers.WpfHelper.OpenFileDialog(true, "txt", "json");
        /// </code>
        /// </summary>
        /// <param name="needAllFile">是否显示所有文件过滤项</param>
        /// <param name="filters">文件过滤后缀</param>
        /// <returns>返回选择的文件</returns>
        public static string OpenFileDialog(bool needAllFile = false, params string[] filters)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //openFile.Filter="(*.jpg,*.png,*.jpeg,*.bmp,*.gif)|*.jgp;*.png;*.jpeg;*.bmp;*.gif|All files(*.*)|*.*";

            string str = SplitFilterString(needAllFile, filters);

            dialog.Filter = str;
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        private static string SplitFilterString(bool needAllFile = false, params string[] filters)
        {
            var str = "*.";
            foreach (var filter in filters)
            {
                str += filter + ";*.";
            }
            str = str.Substring(0, str.Length - 3);
            str = "(" + str + ")";
            str += "|";
            foreach (var filter in filters)
            {
                str += "*." + filter + ";";
            }
            str = str.Substring(0, str.Length - 1);
            if (needAllFile)
            {
                str += "|All files(*.*)|*.*";
            }

            return str;
        }

        /// <summary>
        /// 打开文件选择对话框
        /// <code>
        /// var savePath = SharpBoxes.WPFHelpers.WpfHelper.SaveFileDialog(true, "txt", "json");
        /// </code>
        /// </summary>
        /// <param name="needAllFile">是否显示所有文件过滤项</param>
        /// <param name="filters">文件过滤后缀</param>
        /// <returns>返回选择的文件</returns>
        public static string SaveFileDialog(bool needAllFile = false, params string[] filters)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();

            string str = SplitFilterString(needAllFile, filters);

            dialog.Filter = str;
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        /// <summary>
        /// This method is used to create a StackPanel with the specified <see cref="Orientation"/> and UI elements.
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static StackPanel CreateStackPanel(
            Orientation orientation,
            params UIElement[] elements
        )
        {
            var stackPanel = new StackPanel();
            stackPanel.Orientation = orientation;
            foreach (var element in elements)
            {
                stackPanel.Children.Add(element);
            }
            return stackPanel;
        }

        /// <summary>
        /// This method is used to create a DockPanel with the specified last child fill and UI elements.
        /// </summary>
        /// <param name="lastChildFill"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static DockPanel CreateDockPanel(
            bool lastChildFill,
            params (UIElement, Dock)[] elements
        )
        {
            var dockPanel = new DockPanel();
            dockPanel.LastChildFill = lastChildFill;
            foreach (var (element, dock) in elements)
            {
                dockPanel.Children.Add(element);
                DockPanel.SetDock(element, dock);
            }

            return dockPanel;
        }

        /// <summary>
        /// This method is used to create a WrapPanel with the specified orientation and UI elements.
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static WrapPanel CreateWrapPanel(
            Orientation orientation,
            params UIElement[] elements
        )
        {
            var wrapPanel = new WrapPanel();
            wrapPanel.Orientation = orientation;
            foreach (var element in elements)
            {
                wrapPanel.Children.Add(element);
            }
            return wrapPanel;
        }

        /// <summary>
        /// Disconnects <paramref name="child"/> from it's parent if any.
        /// </summary>
        /// <param name="child"></param>
        public static void RemoveFromSelfParent(this FrameworkElement child)
        {
            var parent = child.Parent;
            if (parent == null)
                return;

            if (parent is Panel panel)
            {
                panel.Children.Remove(child);
                return;
            }

            if (parent is Decorator decorator)
            {
                if (decorator.Child == child)
                    decorator.Child = null;

                return;
            }

            if (parent is ContentPresenter contentPresenter)
            {
                if (contentPresenter.Content == child)
                    contentPresenter.Content = null;
                return;
            }

            if (parent is ContentControl contentControl)
            {
                if (contentControl.Content == child)
                    contentControl.Content = null;
                return;
            }

            //if (parent is ItemsControl itemsControl)
            //{
            //  itemsControl.Items.Remove(child);
            //  return;
            //}
        }

        /// <summary>
        /// Adds or inserts a child back into its parent
        /// </summary>
        /// <param name="child"></param>
        /// <param name="index"></param>
        public static void AddToParent(
            this UIElement child,
            DependencyObject parent,
            int? index = null
        )
        {
            if (parent == null)
                return;

            if (parent is ItemsControl itemsControl)
                if (index == null)
                    itemsControl.Items.Add(child);
                else
                    itemsControl.Items.Insert(index.Value, child);
            else if (parent is Panel panel)
                if (index == null)
                    panel.Children.Add(child);
                else
                    panel.Children.Insert(index.Value, child);
            else if (parent is Decorator decorator)
                decorator.Child = child;
            else if (parent is ContentPresenter contentPresenter)
                contentPresenter.Content = child;
            else if (parent is ContentControl contentControl)
                contentControl.Content = child;
        }

        /// <summary>
        /// Removes the child from its parent collection or its content.
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool RemoveFromParent(
            this UIElement child,
            out DependencyObject parent,
            out int? index
        )
        {
            parent = child.GetParent(true);
            if (parent == null)
                parent = child.GetParent(false);

            index = null;

            if (parent == null)
                return false;

            if (parent is ItemsControl itemsControl)
            {
                if (itemsControl.Items.Contains(child))
                {
                    index = itemsControl.Items.IndexOf(child);
                    itemsControl.Items.Remove(child);
                    return true;
                }
            }
            else if (parent is Panel panel)
            {
                if (panel.Children.Contains(child))
                {
                    index = panel.Children.IndexOf(child);
                    panel.Children.Remove(child);
                    return true;
                }
            }
            else if (parent is Decorator decorator)
            {
                if (decorator.Child == child)
                {
                    decorator.Child = null;
                    return true;
                }
            }
            else if (parent is ContentPresenter contentPresenter)
            {
                if (contentPresenter.Content == child)
                {
                    contentPresenter.Content = null;
                    return true;
                }
            }
            else if (parent is ContentControl contentControl)
            {
                if (contentControl.Content == child)
                {
                    contentControl.Content = null;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method is used to get the parent of a <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="depObj"></param>
        /// <param name="isVisualTree"></param>
        /// <returns></returns>
        public static DependencyObject GetParent(this DependencyObject depObj, bool isVisualTree)
        {
            if (isVisualTree)
            {
                if (depObj is Visual or Visual3D)
                    return VisualTreeHelper.GetParent(depObj);
                return null;
            }
            else
                return LogicalTreeHelper.GetParent(depObj);
        }

        /// <summary>
        /// This method is used to remove all children from a <c>UIElementCollection</c>.
        /// </summary>
        /// <param name="children"></param>
        public static void RemoveAllChildren(this UIElementCollection children)
        {
            for (int i = children.Count - 1; i >= 0; i--)
                children.RemoveAt(i);
        }

        /// <summary>
        /// This method is used to remove all children from a <c>UIElementCollection</c> and dispose them.
        /// </summary>
        /// <param name="children"></param>
        public static void RemoveAllChildrenAndDispose(this UIElementCollection children)
        {
            for (int i = children.Count - 1; i >= 0; i--)
            {
                UIElement child = children[i];

                children.RemoveAt(i);

                child = null;
            }
        }
    }
}
