using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.Helpers;

public static class IOHelper
{
    public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

        // Cache directories before we start copying
        DirectoryInfo[] dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir, true);
            }
        }
    }

    /// <summary>
    /// 打开文件选择对话框
    /// <code>
    /// var path = OpenFileDialog(true, "txt", "json");
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
    /// var savePath = SaveFileDialog(true, "txt", "json");
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

    public static string OpenFolderDialog(string path = "")
    {
        var dialog = new System.Windows.Forms.FolderBrowserDialog();
        if (string.IsNullOrEmpty(path) is false)
        {
            dialog.SelectedPath = path;
        }
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            return dialog.SelectedPath;
        }
        return null;
    }
}
