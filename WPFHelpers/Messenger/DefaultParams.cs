using System;
using System.Diagnostics;

namespace SharpBoxes.WPFHelpers.Messenger;

/// <summary>
/// 一个输入的消息，传递信息的类型为<see cref="string"/>
/// </summary>
[DebuggerStepThrough]
public class StringInput : IMessengerInput
{
    /// <summary>
    /// 拥有者
    /// </summary>
    public object Owner { get; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 初始化<see cref="StringInput"/>的新实例
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="owner">拥有者</param>

    public StringInput(string value, object owner = null)
    {
        Value = value;
        Owner = owner;
    }
}

/// <summary>
/// 一个输出的消息，传递信息的类型为<see cref="string"/>
/// </summary>
[DebuggerStepThrough]
public class StringOutput : IMessengerOutput
{
    /// <summary>
    /// 拥有者
    /// </summary>
    public object Owner { get; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringOutput"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="owner">The owner.</param>
    public StringOutput(string value, object owner = null)
    {
        Value = value;
        Owner = owner;
    }
}

/// <summary>
/// 一个输入的消息，传递信息的类型为泛型；泛型类型为<see cref="ViewModelBase"/>，用于传递ViewModel
/// </summary>
/// <typeparam name="T">传递消息类型</typeparam>
[DebuggerStepThrough]
public class ViewModelInput<T> : IMessengerInput
    where T : ViewModelBase
{
    /// <summary>
    /// 拥有者
    /// </summary>
    public object Owner { get; }

    /// <summary>
    /// 值
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelInput{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="owner">The owner.</param>
    public ViewModelInput(T value, object owner = null)
    {
        Value = value;
        Owner = owner;
    }
}

/// <summary>
/// 一个输出的消息，传递信息的类型为泛型；泛型类型为<see cref="ViewModelBase"/>，用于传递ViewModel
/// </summary>
/// <typeparam name="T">传递消息类型</typeparam>
[DebuggerStepThrough]
public class ViewModelOutput<T> : IMessengerOutput
    where T : ViewModelBase
{
    /// <summary>
    /// 拥有者
    /// </summary>
    public object Owner { get; }

    /// <summary>
    /// 值
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelOutput{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="owner">The owner.</param>
    public ViewModelOutput(T value, object owner = null)
    {
        Value = value;
        Owner = owner;
    }
}
