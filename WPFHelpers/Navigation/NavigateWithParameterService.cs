using System;

namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// a service that navigates to a view model with a parameter，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// </summary>
/// <typeparam name="TParameter">the type of parameter which would passed when navigate</typeparam>
/// <typeparam name="TViewModel">the type of view model which would be navigated to</typeparam>
public class NavigateWithParameterService<TParameter, TViewModel>
    : INavigateWithParameterService<TParameter>
    where TViewModel : ViewModelBase
{
    //private readonly NavigateStore _navigateStore;

    private readonly Func<TParameter, TViewModel> _getViewModel;
    private readonly Action<TParameter, TViewModel> _setViewModel;
    private readonly NavigateWithParameterStore<TViewModel, TParameter> _navigateWithParameterStore;

    /// <summary>
    /// a service that navigates to a view model with a parameter
    /// </summary>
    /// <param name="getViewModel">the way to get newer view model</param>
    /// <param name="setViewModel">the way to set newer view model</param>
    public NavigateWithParameterService(
        Func<TParameter, TViewModel> getViewModel,
        Action<TParameter, TViewModel> setViewModel
    )
    {
        //_navigateStore = navigateStore;
        _getViewModel = getViewModel;
        _setViewModel = setViewModel;
    }

    /// <summary>
    /// 初始化一个新的<see cref="NavigateWithParameterService{TParameter, TViewModel}"/>实例
    /// </summary>
    /// <param name="navigateWithParameterStore">要进行导航的资源</param>
    public NavigateWithParameterService(
        NavigateWithParameterStore<TViewModel, TParameter> navigateWithParameterStore
    )
    {
        this._navigateWithParameterStore = navigateWithParameterStore;
    }

    /// <summary>
    /// 导航到新的视图模型
    /// </summary>
    /// <param name="parameter">要进行传递的参数</param>
    public void Navigate(TParameter parameter)
    {
        //_navigateStore.CurrentViewModel = GetViewModel(parameter);
        if (_navigateWithParameterStore is not null)
        {
            this._navigateWithParameterStore.Navigate(parameter);
        }
        else
        {
            _setViewModel(parameter, _getViewModel(parameter));
        }
    }
}
