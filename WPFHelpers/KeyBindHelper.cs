using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SharpBoxes.WPFHelpers;

/// <summary>
/// 用于注册按键输入捕获的扩展帮助类，支持连续按键捕获，如Ctrl+K,Ctrl+D，连续按下两组按键后，触发指定Command
/// <example>
/// <code>
/// public MainWindow()
/// {
///     InitializeComponent();
///     KeyBindHelper.RegisterKeyBind(
///         KeyBind.Create("ShowMessageBox", ModifierKeys.Control, Key.S, ShowMessageBoxCommand)
///     );
///     KeyBindHelper.RegisterTwoContinuousKeyBind(
///         ModifierKeys.Control,
///         Key.K,
///         ModifierKeys.Control,
///         Key.D,
///         FormatCommand,
///         "Format Code"
///     );
///     foreach (var item in KeyBindHelper.GetKeyBinds())
///     {
///         InputBindings.Add(item.Bind);
///     }
/// }
/// </code>
/// </example>
/// </summary>
public static class KeyBindHelper
{
    private static List<KeyBind> _keyBinds = new();
    private static List<KeyPair> _keyPairs = new();
    private static KeyBinding LastKeyInput;

    /// <summary>
    /// 检查指定按键组合是否已注册
    /// </summary>
    /// <param name="modifierKeys"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool CheckRegister(ModifierKeys modifierKeys, Key key) =>
        _keyBinds.Any(s => s.Bind.Modifiers == modifierKeys && s.Bind.Key == key);

    /// <summary>
    /// 注册两个连续键盘快捷键，并将它们与指定的命令和命令参数关联起来。只有在连续按下注册的按键时，才激活Command
    /// </summary>
    /// <param name="modifierKeys1">第一个快捷键的修饰键。</param>
    /// <param name="key1">第一个快捷键的键。</param>
    /// <param name="modifierKeys2">第二个快捷键的修饰键。</param>
    /// <param name="key2">第二个快捷键的键。</param>
    /// <param name="command">要关联的命令。</param>
    /// <param name="name">快捷键的功能名称。</param>
    /// <param name="commandParameter">命令的参数。</param>
    public static void RegisterTwoContinuousKeyBind(
        ModifierKeys modifierKeys1,
        Key key1,
        ModifierKeys modifierKeys2,
        Key key2,
        ICommand command,
        string name,
        object commandParameter = null
    )
    {
        RegisterKeyBind(KeyBind.Create($"{name}1", modifierKeys1, key1, null));
        RegisterKeyBind(KeyBind.Create($"{name}2", modifierKeys2, key2, null));
        _keyPairs.Add(
            new()
            {
                key1 = key1,
                modifierKeys1 = modifierKeys1,
                key2 = key2,
                modifierKeys2 = modifierKeys2,
                Command = command,
                CommandParameter = commandParameter
            }
        );
    }

    /// <summary>
    /// 注册普通按键输入
    /// </summary>
    /// <param name="keyBind"></param>
    public static void RegisterKeyBind(KeyBind keyBind)
    {
        keyBind.Command.OnExecute += Command_OnExecute;
        _keyBinds.Add(keyBind);
    }

    private static void Command_OnExecute(AdvancedKeyInputCommand cmd)
    {
        if (LastKeyInput != null)
        {
            var firstcheck = _keyPairs.Where(
                s => s.key1 == LastKeyInput.Key && s.modifierKeys1 == LastKeyInput.Modifiers
            );
            foreach (var item in firstcheck)
            {
                if (
                    item.modifierKeys2 == cmd._keyBinding.Modifiers
                    && item.key2 == cmd._keyBinding.Key
                )
                {
                    item.Command?.Execute(item.CommandParameter);
                    LastKeyInput = null;
                    break;
                }
            }
        }
        LastKeyInput = cmd._keyBinding;
    }

    /// <summary>
    /// 当前所有注册的按键输入
    /// </summary>
    /// <returns></returns>
    public static List<KeyBind> GetKeyBinds() => _keyBinds;

    /// <summary>
    /// 连续按键输入捕获的数据结构
    /// </summary>
    class KeyPair
    {
        public ModifierKeys modifierKeys1;
        public Key key1;
        public ModifierKeys modifierKeys2;
        public Key key2;
        public ICommand Command;
        public object CommandParameter;
    }
}

public class KeyBind
{
    public KeyBinding Bind;
    public string Modifier { get; set; }
    public string Key { get; set; }

    /// <summary>
    /// 该组按键输入的功能名称
    /// </summary>
    public string Function { get; set; }

    /// <summary>
    /// 此处使用AdvancedKeyInputCommand，而不使用ICommand，是因为要对Execute进行监听，以此达到连续按键输入捕获的功能
    /// </summary>
    public AdvancedKeyInputCommand Command;

    public static KeyBind Create(
        string functionName,
        ModifierKeys modifierKeys,
        Key key,
        ICommand command,
        object commandParameter = null,
        IInputElement inputElement = null
    )
    {
        KeyBind keyBind = new();

        KeyBinding keyBinding = new KeyBinding();
        keyBinding.Modifiers = modifierKeys;
        keyBinding.Key = key;
        //--
        keyBind.Command = new(command, functionName, keyBinding);
        //---
        keyBinding.Command = keyBind.Command;
        keyBinding.CommandParameter = commandParameter;
        keyBinding.CommandTarget = inputElement;
        //---
        keyBind.Function = functionName;
        keyBind.Key = key.ToString();
        keyBind.Modifier = modifierKeys.ToString();
        keyBind.Bind = keyBinding;

        return keyBind;
    }
}

/// <summary>
/// 相比普通的ICommand，该Command支持在Execute时触发捕获事件，并且专门用于按键输入捕获功能
/// </summary>
public class AdvancedKeyInputCommand : ICommand
{
    private readonly ICommand _command;
    private readonly string _name;
    public readonly KeyBinding _keyBinding;

    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Command在Execute时触发捕获
    /// </summary>
    public event Action<AdvancedKeyInputCommand> OnExecute;

    /// <summary>
    ///
    /// </summary>
    /// <param name="command">该按键输入被捕获时，要执行的命令</param>
    /// <param name="name">该按键输入的功能名称，比如保存，打开等</param>
    /// <param name="keyBinding">要捕获的按键输入</param>
    public AdvancedKeyInputCommand(ICommand command, string name, KeyBinding keyBinding)
    {
        this._command = command;
        this._name = name;
        this._keyBinding = keyBinding;
    }

    public bool CanExecute(object parameter) => true;

    /// <summary>
    /// 执行命令。
    /// </summary>
    /// <param name="parameter">命令参数。</param>
    public void Execute(object parameter)
    {
        // 调用 OnExecute 事件，并传递当前对象。
        OnExecute?.Invoke(this);

        // 获取与当前 KeyBinding 匹配的 KeyBind 列表。
        var keyBinds = KeyBindHelper
            .GetKeyBinds()
            .Where(s => s.Bind.Modifiers == _keyBinding.Modifiers && s.Bind.Key == _keyBinding.Key)
            .ToList();

        // 遍历 KeyBind 列表，并执行每个 KeyBind 中的命令。
        keyBinds.ForEach(s => s.Command._command?.Execute(parameter));

        // 执行当前命令。
        //_command?.Execute(parameter);
    }
}
