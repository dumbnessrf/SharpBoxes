using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// 要进行导航的带参数资源，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// <para></para>
/// <example>
/// <code>
/// NavigateWithParameterStore&lt;ViewModelBase, string&gt; navigateWithParameterStore =
///     new NavigateWithParameterStore&lt;ViewModelBase, string&gt;(
///         GetViewModelByParameter,
///         (s, vm) => this.SelectedViewModel = vm
///     );
/// navigateWithParameterStore.OnCurrentViewModelChangeAfter += (s, vm) =>
/// {
///     Console.WriteLine($"Current view model changed to {vm}");
/// };
/// navigateWithParameterStore.OnCurrentViewModelChangeBefore += (s, vm) =>
/// {
///     Console.WriteLine($"Current view model changing to {vm}");
/// };
/// this.NavigateAsyncCommand = new NavigateWithParameterCommand&lt;string&gt;(
///     new NavigateWithParameterService&lt;string, ViewModelBase&gt;(navigateWithParameterStore)
/// );
/// private static ViewModelBase GetViewModelByParameter(string s) =>
///     s switch
///     {
///         "Teachers" => new TeachersViewModel(),
///         "Students" => new StudentsViewModel(),
///         _ => null
///     };
/// </code>
/// </example>
/// </summary>
/// <typeparam name="TViewModel">ViewModel类型</typeparam>
/// <typeparam name="TParameter">要进行传递的参数类型</typeparam>
public class NavigateWithParameterStore<TViewModel, TParameter>
    where TViewModel : ViewModelBase
{
    private readonly Func<TParameter, TViewModel> _getViewModel;
    private readonly Action<TParameter, TViewModel> _setViewModel;

    /// <summary>
    /// 初始化一个新的<see cref="NavigateWithParameterStore{TViewModel, TParameter}"/>实例
    /// </summary>
    ///
    /// <param name="getViewModel">获取ViewModel的方法</param>
    /// <param name="setViewModel">设置ViewModel的方法</param>
    public NavigateWithParameterStore(
        Func<TParameter, TViewModel> getViewModel,
        Action<TParameter, TViewModel> setViewModel
    )
    {
        this._getViewModel = getViewModel;
        this._setViewModel = setViewModel;
    }

    /// <summary>
    /// ViewModel更改后触发
    /// </summary>
    public event Action<TParameter, TViewModel> OnCurrentViewModelChangeAfter;

    /// <summary>
    /// ViewModel更改前触发
    /// </summary>
    public event Action<TParameter, TViewModel> OnCurrentViewModelChangeBefore;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public NavigateWithParameterStore() { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    internal void Navigate(TParameter parameter)
    {
        TViewModel viewModel = _getViewModel?.Invoke(parameter);
        OnCurrentViewModelChangeBefore?.Invoke(parameter, viewModel);
        _setViewModel(parameter, viewModel);
        OnCurrentViewModelChangeAfter?.Invoke(parameter, viewModel);
    }
}
