using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBoxes.Helpers;

/// <summary>
/// 删除指定时间以前的文件夹，并且支持指定间隔轮询判断文件夹是否符合要求
/// <example>
/// <code>
/// FolderDeleteService.AddDeleteFolders(
///     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images")
/// );
/// FolderDeleteService.ChangeDeleteDay(30);
/// FolderDeleteService.ChangeAndStartInterval(TimeSpan.FromHours(1));
/// </code>
/// </example>
/// </summary>
public static class FolderDeleteService
{
    private static Timer _timer;
    private static int _interval = 1000;
    private static int _deleteDay = 7;
    private static List<string> DeleteFolders = new List<string>();

    /// <summary>
    /// 删除结束事件，参数为删除的文件夹名称
    /// </summary>
    public static event Action<string> OnFolderDeleted;

    /// <summary>
    /// 开始删除事件，参数为即将删除的文件夹名称
    /// </summary>
    public static event Action<string> OnFolderDeleteStarted;

    /// <summary>
    /// 删除失败事件
    /// </summary>
    public static event Action<Exception> OnFolderDeleteFailed;

    static FolderDeleteService()
    {
        _timer = new Timer(DeleteFoldersCallback, null, -1, _interval);
    }

    /// <summary>
    /// 应用间隔并启动
    /// </summary>
    /// <remarks>
    /// <paramref name="interval"/>:间隔，单位ms
    /// </remarks>
    /// <param name="interval">间隔，单位ms</param>
    public static void ChangeAndStartInterval(int interval)
    {
        _interval = interval;
        _timer.Change(0, _interval);
    }

    /// <summary>
    /// 应用间隔并启动
    /// </summary>
    /// <remarks>
    /// <paramref name="interval"/>:<see cref="TimeSpan"/>类型
    /// </remarks>
    /// <param name="interval">间隔</param>
    public static void ChangeAndStartInterval(TimeSpan interval)
    {
        _interval = Convert.ToInt32(interval.TotalMilliseconds);
        _timer.Change(0, _interval);
    }

    /// <summary>
    /// 更改删除时间，最后写入时间刻超过该时间才进行删除
    ///
    /// </summary>
    /// <remarks>
    /// <paramref name="deleteDay"/>:删除时间，最后写入时间刻超过该时间才进行删除
    /// </remarks>
    /// <param name="deleteDay">删除时间</param>
    public static void ChangeDeleteDay(int deleteDay)
    {
        _deleteDay = deleteDay;
    }

    private static async void DeleteFoldersCallback(object state)
    {
        await DeleteAllDirectoriesAsync(
            DeleteFolders
                .SelectMany(s =>
                {
                    if (Directory.Exists(s))
                    {
                        var dirs = Directory.GetDirectories(s, "*", SearchOption.AllDirectories);
                        return dirs.Select(s1 => new DirectoryInfo(s1)).ToList();
                    }

                    return new List<DirectoryInfo>();
                })
                .ToList(),
            _deleteDay
        );
    }

    /// <summary>
    /// 添加要进行检查并删除文件夹
    /// </summary>
    /// <remarks>
    /// <paramref name="path"/>:要进行检查并删除文件夹路径
    /// </remarks>
    /// <param name="path">文件夹路径</param>
    public static void AddDeleteFolders(string path)
    {
        if (Directory.Exists(path) && !DeleteFolders.Contains(path))
        {
            DeleteFolders.Add(path);
        }
    }

    private static IEnumerable<DirectoryInfo> SortByCreationTime(
        List<DirectoryInfo> list,
        bool newFirst = true
    )
    {
        if (newFirst)
        {
            list.Sort((info, info1) => info1.CreationTime.CompareTo(info.CreationTime));
            return list;
        }
        else
        {
            list.Sort((info, info1) => info.CreationTime.CompareTo(info1.CreationTime));
            return list;
        }
    }

    private static IEnumerable<DirectoryInfo> SortByLastWriteTime(
        List<DirectoryInfo> list,
        bool newFirst = true
    )
    {
        if (newFirst)
        {
            list.Sort((info, info1) => info1.LastWriteTime.CompareTo(info.LastWriteTime));
            return list;
        }
        else
        {
            list.Sort((info, info1) => info.LastWriteTime.CompareTo(info1.LastWriteTime));
            return list;
        }
    }

    /// <summary>
    /// delete folders based on last write time
    /// </summary>
    /// <param name="list">directory info be deleted</param>
    /// <param name="deleteDays">if directory last write time is less than <paramref name="deleteDays"/>, it would be delete</param>
    private static async Task DeleteAllDirectoriesAsync(
        List<DirectoryInfo> list,
        int deleteDays = 7
    )
    {
        var now = DateTime.Now;
        var deleteTime = now.AddDays(-deleteDays);
        list = list.Where(info => info.LastWriteTime < deleteTime).ToList();

        foreach (var dir in list)
        {
            await Task.Run(() =>
            {
                if (!dir.Exists)
                    return;
                OnFolderDeleteStarted?.Invoke(dir.FullName);
                DeleteFilesOneByOne(dir.FullName);
                try
                {
                    dir.Delete(true);
                }
                catch (Exception ex)
                {
                    OnFolderDeleteFailed?.Invoke(ex);
                }
                OnFolderDeleted?.Invoke(dir.FullName);
            });
        }
    }

    private static void DeleteFilesOneByOne(string folder)
    {
        if (!Directory.Exists(folder))
        {
            return;
        }
        var files = Directory.GetFiles(folder);
        foreach (var file in files)
        {
            if (File.Exists(file))
                File.Delete(file);
        }
        Thread.Sleep(5);
    }
}
