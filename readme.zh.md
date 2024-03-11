# SharpBoxes

查看其他相关的Visual Studio扩展：[SharpBoxes.Cuts](https://marketplace.visualstudio.com/items?itemName=dumbnessrf.SharpBoxes)
它提供了许多有用的C#、XAML代码片段，如**带有OnPropertyChanged的完整属性声明**，**自动用Task.Run(()=>{  }包围**等...

目录
- [SharpBoxes](#sharpboxes)
  - [说明](#说明)
  - [使用方法](#使用方法)
    - [EnumBindingSourceExtension](#enumbindingsourceextension)
    - [Command](#command)
    - [Messenger](#messenger)
    - [Navigation](#navigation)
    - [ViewModelBase](#viewmodelbase)
    - [事件反射助手](#事件反射助手)
    - [自定义输入对话框](#自定义输入对话框)
    - [BindablePasswordBox](#bindablepasswordbox)
    - [HighlightTextBlock](#highlighttextblock)
    - [WPF中的转换器](#wpf中的转换器)


## 说明
一个方便C#和WPF开发的包。

* 简单的`CacheHelper`，无需任何其他扩展。
* 许多常用的`Converters`，用于WPF。
* 许多`String`、`Int`、`Double`、`Float`、`List`、`Dictionary`等的扩展方法，用于转换。
* 集成反射和Linq，方便使用。
* 提供了一些常用的WPF用户控件，如`HighlightTextBlock`、`PlaceholderTextBoxControl`、`CustomToolTip`。
* 提供了Messenger、Navigation、Command、ViewModelBase、Auto Validation等WPF MVVM模式的功能。
* 提供了一些WPF的对话框扩展，如`MessageBox`、`OpenFileDialog`、`SaveFileDialog`、`CustomInputDialog`。
* 一些未来SDK的预览功能，如`record`、`CallerArgumentExpression`、`RequiredMember`。
* ...

## 使用方法
### EnumBindingSourceExtension
对于WPF，你可以使用`EnumBindingSourceExtension`轻松地将枚举绑定到combobox。
```xaml
<ComboBox HorizontalAlignment="Center" VerticalAlignment="Center" MinWidth="150"
      ItemsSource="{Binding Source={local:EnumBindingSource {x:Type local:Status}}}"/>
```
```csharp
public enum Status
{
    Horrible,
    Bad,
    SoSo,
    Good,
    Better,
    Best
}
//类似DisplayName，用于自定义显示名称
public enum Status
{
    [DescriptionAttribute("这是可怕的")]
    Horrible,
    [DescriptionAttribute("这是糟糕的")]
    Bad,
    [DescriptionAttribute("这是一般的")]
    SoSo,
        name = value;
        OnPropertyChanged(name, value);
    }
}
public IActionCommand UpdateSyncCommand { get; private set; }
    [DescriptionAttribute("这是好的")]
    Good,
    [DescriptionAttribute("这是更好的")]
    Better,
    [DescriptionAttribute("这是最好的")]
    Best
}
```

### Command
对于WPF，你可以使用`ActionCommand`轻松地将命令绑定到按钮。
```csharp
private string name;
public string Name
{
    get => name;
    set
    {
        OnPropertyChanging(name, value);
UpdateSyncCommand = new ActionCommand(
    () =>
    {
        Thread.Sleep(1000);
        this.Name = "James Harden";
    },
    () => true
);
```
或者，你可以使用`AsyncCommand`轻松地将异步命令绑定到按钮。
```csharp
private string name;
public string Name
{
    get => name;
    set
    {
        OnPropertyChanging(name, value);
        name = value;
        OnPropertyChanged(name, value);
    }
}
public IAsyncCommand UpdateAsyncCommand { get; private set; }
UpdateAsyncCommand = new AsyncCommand(
    async () =>
    {
        await Task.Delay(1000);
        this.Name = "Clay Tomposon";
    },
    () => true
);
```
此外，你还可以使用`NavigateWithParameterCommand`轻松地将导航命令绑定到按钮。
```csharp
internal class StudentsViewModel : SharpBoxes.WPFHelpers.ViewModelBase
{
    //...
}
internal class TeachersViewModel : SharpBoxes.WPFHelpers.ViewModelBase
{
    //...
}

public SharpBoxes.WPFHelpers.ViewModelBase SelectedViewModel
{
    get { return selectedViewModel; }
    set
    {
        //若viewmodel被改变，则对应datatemplate的view也会被改变，见app.xaml中定义的对应viewmodel对应view的datatemplate
        selectedViewModel = value;
        OnPropertyChanged();
    }
}
public ICommand NavigateAsyncCommand { get; private set; }
NavigateWithParameterStore<ViewModelBase, string> navigateWithParameterStore =
    new NavigateWithParameterStore<ViewModelBase, string>(
        GetViewModelByParameter,
        (s, vm) => this.SelectedViewModel = vm
    );
navigateWithParameterStore.OnCurrentViewModelChangeAfter += (s, vm) =>
{
    Console.WriteLine($"Current view model changed to {vm}");
};
this.NavigateAsyncCommand = new NavigateWithParameterCommand<string>(
    new NavigateWithParameterService<string, ViewModelBase>(navigateWithParameterStore)
);
private static ViewModelBase GetViewModelByParameter(string s) =>
    s switch
    {
        "Teachers" => new TeachersViewModel(),
        "Students" => new StudentsViewModel(),
        _ => null
    };
```
在xaml中
```xaml
<Application
    x:Class="Samples.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:Samples"
    xmlns:views="clr-namespace:Samples.Views"
    xmlns:vm="clr-namespace:Samples.ViewModels"
    StartupUri="Views/MainWindow.xaml">
    <Application.Resources>
        <DataTemplate DataType="{x:Type vm:StudentsViewModel}">
            <views:StudentsView />
        </DataTemplate>
        <DataTemplate DataType="{x:Type vm:TeachersViewModel}">
            <views:TeachersView />
        </DataTemplate>
    </Application.Resources>
</Application>
```
现在，你可以随心所欲地导航了。
### Messenger
对于WPF，你可以使用`Messenger`在`ViewModels`或其他对象之间发送消息。 
无参数传递
```csharp
Messenger.Default.RegisterAction(
"The key of the message with no param",
    () =>
    {
        this.Message = "Receive the message with no param";
    }
);
//使用方法
this.SendAsyncCommand = new AsyncCommand(
    () => Messenger.Default.SendAsync("The key of the message with no param")
);
```
带参数传递
```csharp
Messenger.Default.RegisterAction(
"The key of the message with param",
 (p) =>
 {
     if (p is StringInput input)
     {
         this.Message = input.Value;
     }
 }
);
//使用方法
var rd = new Random();
    this.SendWithParamAsyncCommand = new AsyncCommand(
    () =>
Messenger.Default.SendAsync(
    "The key of the message with param",
    new StringInput($"The value of the message is {rd.Next(1, 100)}")
    )
);
```
此外，你还可以发送一个请求并得到一个响应，就像C#中的`ManualResetEvent`。
```csharp
Messenger.Default.RegisterFunc(
    "The key of the message with in and out param",
    (input) =>
    {
        if (input is StringInput stringInput)
        {
            this.Message = $"Receive the message with in param :{stringInput.Value}";
        }
        return new StringOutput("the out value is Happy New Year Too!");
    }
);
//使用方法
this.RequestAsyncCommand = new AsyncCommand(async () =>
{
    var res = await Messenger.Default.RequestAsync(
        "The key of the message with in and out param",
        new StringInput("Happy New Year!"),
        CancellationToken.None
    );
    if (res is StringOutput stringOutput)
    {
        SharpBoxes.WPFHelpers.Dialogs.MessageBoxes.Information(stringOutput.Value);
    }
});
```
### Navigation
对于WPF，你可以使用导航命令和导航服务轻松地在页面之间导航。
```csharp
NavigateWithParameterStore<ViewModelBase, string> navigateWithParameterStore =
    new NavigateWithParameterStore<ViewModelBase, string>(
        GetViewModelByParameter,
        (s, vm) => this.SelectedViewModel = vm
    );
navigateWithParameterStore.OnCurrentViewModelChangeAfter += (s, vm) =>
{
    Console.WriteLine($"Current view model changed to {vm}");
};
navigateWithParameterStore.OnCurrentViewModelChangeBefore += (s, vm) =>
{
    Console.WriteLine($"Current view model changing to {vm}");
};
this.NavigateAsyncCommand = new NavigateWithParameterCommand<string>(
    new NavigateWithParameterService<string, ViewModelBase>(navigateWithParameterStore)
);
private static ViewModelBase GetViewModelByParameter(string s) =>
    s switch
    {
        "Teachers" => new TeachersViewModel(),
        "Students" => new StudentsViewModel(),
        _ => null
    };
```
关于更完整的使用方法，请参考`Command`中的`NavigateWithParameterCommand`。
### ViewModelBase
对于WPF，你可以使用`ViewModelBase`轻松地创建一个`ViewModel`。
```csharp
internal class MainViewModel : SharpBoxes.ViewModelBase
{
    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }
}
```
### 事件反射助手
C#中,你可以使用`EventReflectionHelper`轻松地反射事件。
获取事件唤醒时所调用的方法
```csharp
public interface INotifyValueChanged
{
    event EventHandler&lt;NotifyValueChangedEventArgs&gt; OnNotifyValueChanged;
}
var invokeMethod = instance.GetEventHandlerRaiseMethods("OnNotifyValueChanged", typeof(EventHandler&lt;NotifyValueChangedEventArgs&gt;)).FirstOrDefault();
//invokeMethod.Invoke(this, new object[] { null, myEventArgs });
invokeMethod.Method?.Invoke(
invokeMethod.FieldValue,
[
    null,
    new NotifyValueChangedEventArgs()
 ]
);
```
### 自定义输入对话框
对于WPF，你可以使用`CustomInputDialog`轻松地创建一个自定义输入对话框。
```csharp
SharpBoxes.WPFHelpers.Dialogs.DialogManager.ShowInputDialog(
    "Please input your value.\n"
    + "if input type is integer or double, float, the limited range is 0-100.\n"
    + "try input value that out of range, then will show error message.",
    0,
    100,
    InputType,
    out var value,
    "Password"
);
SharpBoxes.WPFHelpers.Dialogs.MessageBoxes.Information(
$"Input: {value}",
"Input Dialog Input");
```
### 可绑定的密码框
对于WPF，你可以使用`BindablePasswordBox`轻松地创建一个可绑定的`PasswordBox`。
```xaml
xmlns:userControls="clr-namespace:SharpBoxes.WPFHelpers.UserControls;assembly=SharpBoxes"

<userControls:BindablePasswordBox
    Background="AliceBlue"
    Foreground="Red"
    PaddingNew="5"
    Password="{Binding userControlsViewModel.Password}"
    PlaceHolder="Please input password!" />
```
```csharp
private string _password;
public string Password
{
    get { return _password; }
    set
    {
        _password = value;
        OnPropertyChanged();
    }
}
```
### 高亮文本块
对于WPF，你可以使用`HighlightTextBlock`轻松地创建一个高亮文本块。
```xaml
xmlns:userControls="clr-namespace:SharpBoxes.WPFHelpers.UserControls;assembly=SharpBoxes"

<TextBox Text="{Binding userControlsViewModel.HighlightText, UpdateSourceTrigger=PropertyChanged}" />
<userControls:HighlightTextBlock
    BackgroundHighlight="Yellow"
    ForegroundHighlight="Blue"
    HighlightText="{Binding userControlsViewModel.HighlightText}"
    Text="asdjalksj akjdaklsj      wtupogjk    askdnas" />
```
![img.png](.assets/img2.png)
### wpf中的转换器
* `BooleanToVisibilityConverter`
* `BrushToColorConverter`
* `ColorToBrushConverter`
* `StringToSolidColorBrushConverter`

