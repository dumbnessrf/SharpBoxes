using System;
using System.Data.Common;

namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// 要进行导航的不带参数资源，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// <para></para>带参数使用方法见带参数的<see cref="NavigateWithParameterStore{TViewModel, TParameter}"/>使用方法
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
public class NavigateStore<TViewModel>
    where TViewModel : ViewModelBase
{
    private readonly Action<TViewModel> _setViewModel;
    private readonly Func<TViewModel> _getViewModel;

    /// <summary>
    /// ViewModel更改后触发
    /// </summary>
    public event Action<TViewModel> OnCurrentViewModelChangeAfter;

    /// <summary>
    /// ViewModel更改前触发
    /// </summary>
    public event Action<TViewModel> OnCurrentViewModelChangeBefore;

    /// <summary>
    /// 初始化一个新的<see cref="NavigateStore{TViewModel}"/>实例
    /// </summary>
    /// <param name="setViewModel">设置ViewModel的方式</param>
    /// <param name="getViewModel">获取ViewModel的方式</param>
    public NavigateStore(Action<TViewModel> setViewModel, Func<TViewModel> getViewModel)
    {
        this._setViewModel = setViewModel;
        this._getViewModel = getViewModel;
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public NavigateStore() { }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    internal void Navigate()
    {
        TViewModel viewModel = _getViewModel?.Invoke();
        OnCurrentViewModelChangeBefore?.Invoke(viewModel);
        _setViewModel(viewModel);
        OnCurrentViewModelChangeAfter?.Invoke(viewModel);
    }
}
