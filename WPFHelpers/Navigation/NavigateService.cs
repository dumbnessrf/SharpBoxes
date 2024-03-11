using System;

namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// 导航服务，不带参数，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
public class NavigateService<TViewModel> : INavigateService
    where TViewModel : ViewModelBase
{
    //private readonly NavigateStore _navigateStore;
    private readonly Action<TViewModel> _setViewModel;
    private readonly Func<TViewModel> _getViewModel;

    /// <summary>
    /// 初始化一个新的<see cref="NavigateService{TViewModel}"/>实例
    /// </summary>
    /// <param name="setViewModel">设置ViewModel的方式</param>
    /// <param name="getViewModel">获取ViewModel的方式</param>
    public NavigateService(Func<TViewModel> getViewModel, Action<TViewModel> setViewModel)
    {
        _getViewModel = getViewModel;
        _setViewModel = setViewModel;
    }

    /// <summary>
    /// 导航到新的视图模型
    /// </summary>
    public void Navigate()
    {
        //_navigateStore.CurrentViewModel = GetViewModel();
        _setViewModel(_getViewModel());
    }
}
