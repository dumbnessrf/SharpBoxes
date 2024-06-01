using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace SharpBoxes.WPFHelpers.Progress;

/// <summary>
/// 用于进度报告功能，该功能可以有效解决进度条与UI主线程之间使用不当产生的假死问题
/// <example>
/// <code>
/// var loading = App.Container.Resolve&lt;LoadingWindow&gt;();
/// loading.Message = "Loading ...";
/// var reporter = ProgessFacilities.Create&lt;CustomProgessBody&gt;(
///     value =>
///     {
///         loading.Message = value.Message;
///     },
///     async () =>
///     {
///         await Task.Delay(500);
///         loading.Visibility = Visibility.Hidden;
///     },
///     100
/// );
/// loading.Show();
/// reporter.Report(new(0, $"Registering ui component..."));
/// await ......
/// reporter.Report(new(50, $"Showing interface..."));
/// await ......
/// reporter.Report(new(100, $"Initializing completely!"));
/// </code>
/// </example>
/// </summary>
public class ProgressFacilities
{
    /// <summary>
    /// 创建一个自定义的进度对象，用于处理指定类型的进度信息。
    /// </summary>
    /// <typeparam name="T">进度信息的类型。</typeparam>
    /// <param name="progressHandler">用于处理进度信息的委托。</param>
    /// <param name="complete">用于完成进度的异步任务。</param>
    /// <param name="maximum">进度的最大值。</param>
    /// <returns>一个自定义的进度对象。</returns>
    public static IProgress<T> Create<T>(
        Action<T> progressHandler,
        Func<Task> complete,
        double maximum
    )
        where T : IProgessBody
    {
        return new CustomProgress<T>(progressHandler, complete, maximum);
    }

    /// <summary>
    /// 创建一个自定义的进度对象，用于处理指定类型的进度信息。
    /// </summary>
    /// <param name="progressHandler">用于处理进度信息的委托。</param>
    /// <param name="complete">用于完成进度的异步任务。</param>
    /// <param name="maximum">进度的最大值。</param>
    /// <returns>一个自定义的进度对象。</returns>
    public static IProgress<double> Create(
        Action<double> progressHandler,
        Func<Task> complete,
        double maximum
    )
    {
        return new CustomProgress(progressHandler, complete, maximum);
    }
}

class CustomProgress<T> : Progress<T>
    where T : notnull, IProgessBody
{
    private readonly Func<Task>? _complete;
    private readonly double _maximum;
    private bool _isCompleted;

    public CustomProgress(Action<T> handler, Func<Task>? complete, double maximum)
        : base(handler)
    {
        _complete = complete;
        _maximum = maximum;
        ProgressChanged += CheckCompletion;
    }

    protected override void OnReport(T value)
    {
        if (_isCompleted)
            return;
        base.OnReport(value);
    }

    private async void CheckCompletion(object? sender, T e)
    {
        if (e.Progress.Equals(_maximum) && !_isCompleted)
        {
            _isCompleted = true;
            if (_complete != null)
            {
                await _complete();
            }
        }
    }
}

class CustomProgress : Progress<double>
{
    private readonly Func<Task>? _complete;
    private readonly double _maximum;
    private bool _isCompleted;

    public CustomProgress(Action<double> handler, Func<Task>? complete, double maximum)
        : base(handler)
    {
        _complete = complete;
        _maximum = maximum;
        ProgressChanged += CheckCompletion;
    }

    protected override void OnReport(double value)
    {
        if (_isCompleted)
            return;
        base.OnReport(value);
    }

    private async void CheckCompletion(object? sender, double e)
    {
        if (e.Equals(_maximum) && !_isCompleted)
        {
            _isCompleted = true;
            if (_complete != null)
            {
                await _complete();
            }
        }
    }
}

public interface IProgessBody
{
    public double Progress { get; set; }
}
