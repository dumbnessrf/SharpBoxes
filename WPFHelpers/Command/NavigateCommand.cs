using SharpBoxes.WPFHelpers.Navigation;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 提供导航功能的命令类，建议搭配<see cref="Navigation.NavigateWithParameterStore"/>和<see cref="Navigation.NavigateWithParameterService"/>使用
    /// <para></para>
    /// <see cref="Navigation.NavigateWithParameterStore"/>负责存储导航的资源，<see cref="Navigation.NavigateWithParameterService"/>负责导航的具体实现
    /// <example>
    /// <strong>下面示例了如何进行导航的完整案例</strong>
    /// <code>
    /// public SharpBoxes.WPFHelpers.ViewModelBase SelectedViewModel
    /// {
    ///     get { return selectedViewModel; }
    ///     set
    ///     {
    ///         //若viewmodel被改变，则对应datatemplate的view也会被改变，见app.xaml中定义的对应viewmodel对应view的datatemplate
    ///         selectedViewModel = value;
    ///         OnPropertyChanged();
    ///     }
    /// }
    /// public ICommand NavigateAsyncCommand { get; private set; }
    /// NavigateWithParameterStore&lt;ViewModelBase, string&gt; navigateWithParameterStore =
    ///     new NavigateWithParameterStore&lt;ViewModelBase, string&gt;(
    ///         GetViewModelByParameter,
    ///         (s, vm) => this.SelectedViewModel = vm
    ///     );
    /// navigateWithParameterStore.OnCurrentViewModelChangeAfter += (s, vm) =>
    /// {
    ///     Console.WriteLine($"Current view model changed to {vm}");
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
    /// </code></example>
    /// <example>
    /// 在App.xaml中定义ViewModel对应的View
    /// <code>
    /// &lt;Application
    ///     x:Class="Samples.App"
    ///     xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    ///     xmlns:local="clr-namespace:Samples"
    ///     xmlns:views="clr-namespace:Samples.Views"
    ///     xmlns:vm="clr-namespace:Samples.ViewModels"
    ///     StartupUri="Views/MainWindow.xaml"&gt;
    ///     &lt;Application.Resources&gt;
    ///         &lt;DataTemplate DataType="{x:Type vm:StudentsViewModel}"&gt;
    ///             &lt;views:StudentsView /&gt;
    ///         &lt;/DataTemplate&gt;
    ///         &lt;DataTemplate DataType="{x:Type vm:TeachersViewModel}"&gt;
    ///             &lt;views:TeachersView /&gt;
    ///         &lt;/DataTemplate&gt;
    ///     &lt;/Application.Resources&gt;
    /// &lt;/Application&gt;
    /// </code>
    /// </example>
    /// <example>
    /// ViewModel定义
    /// <code>
    /// internal class StudentsViewModel : SharpBoxes.WPFHelpers.ViewModelBase
    /// {
    ///     //...
    /// }
    /// internal class TeachersViewModel : SharpBoxes.WPFHelpers.ViewModelBase
    /// {
    ///     //...
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class NavigateCommand : CommandBase
    {
        /// <summary>
        /// 导航服务接口的实例
        /// </summary>
        private readonly INavigateService _navigateService;

        /// <summary>
        /// 构造函数，初始化导航服务接口的实例
        /// </summary>
        /// <param name="navigateService">导航服务接口的实例</param>
        public NavigateCommand(INavigateService navigateService)
        {
            _navigateService = navigateService;
        }

        /// <summary>
        /// 执行导航命令
        /// </summary>
        /// <param name="parameter">命令参数，此处未使用</param>
        public override void Execute(object parameter)
        {
            _navigateService.Navigate();
        }
    }
}
