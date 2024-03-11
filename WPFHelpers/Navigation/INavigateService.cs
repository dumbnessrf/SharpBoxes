namespace SharpBoxes.WPFHelpers.Navigation;

/// <summary>
/// 不带参数传递的导航服务接口，具体完整使用案例见<see cref="Command.NavigateCommand"/>或<see cref="Command.NavigateWithParameterCommand"/>
/// </summary>
public interface INavigateService
{
    /// <summary>
    /// 导航
    /// </summary>
    void Navigate();
}
