namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// 带参数传递的导航服务接口，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// </summary>
/// <typeparam name="TParameter">要在导航时进行传递的参数类型</typeparam>
public interface INavigateWithParameterService<in TParameter>
{
    /// <summary>
    /// 导航并传递参数
    /// </summary>
    /// <param name="parameter">参数</param>
    void Navigate(TParameter parameter);
}
