using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBoxes.WPFHelpers.Messenger;

/// <summary>
/// Represents a messenger that facilitates communication between different parts of an application.
/// </summary>
public interface IMessengerParamHandle
{
    /// <summary>
    /// The owner of the message.
    /// </summary>
    public object Owner { get; }
}

/// <summary>
/// Represents a messenger that facilitates communication between different parts of an application.
/// </summary>
public interface IMessengerInput : IMessengerParamHandle { }

/// <summary>
/// Represents a messenger that facilitates communication between different parts of an application.
/// </summary>
public interface IMessengerOutput : IMessengerParamHandle { }

/// <summary>
/// Represents a messenger that facilitates communication between different parts of an application.
/// </summary>
public class Messenger
{
    private ConcurrentDictionary<string, Action<IMessengerInput>> ActionCallBacks = new();
    private ConcurrentDictionary<string, Func<IMessengerInput, IMessengerOutput>> FuncCallBacks =
        new();
    private ConcurrentDictionary<string, Action> ActionNoParamCallBacks = new();

    /// <summary>
    /// Represents a messenger that facilitates communication between different parts of an application.
    /// <para></para>
    /// <example>
    /// Triggering a message with a key but no parameter passed.
    /// <code>
    /// Messenger.Default.RegisterAction(
    /// "The key of the message with no param",
    ///     () =>
    ///     {
    ///         this.Message = "Receive the message with no param";
    ///     }
    /// );
    /// //Usage
    /// this.SendAsyncCommand = new AsyncCommand(
    ///     () => Messenger.Default.SendAsync("The key of the message with no param")
    /// );
    /// </code>
    /// </example>
    /// <example>
    /// Sending a message with a key with parameter.
    /// <code>
    /// Messenger.Default.RegisterAction(
    /// "The key of the message with param",
    ///  (p) =>
    ///  {
    ///      if (p is StringInput input)
    ///      {
    ///          this.Message = input.Value;
    ///      }
    ///  }
    /// );
    /// //Usage
    /// var rd = new Random();
    ///     this.SendWithParamAsyncCommand = new AsyncCommand(
    ///     () =>
    /// Messenger.Default.SendAsync(
    ///     "The key of the message with param",
    ///     new StringInput($"The value of the message is {rd.Next(1, 100)}")
    ///     )
    /// );
    /// </code>
    /// </example>
    /// <example>
    /// Requesting a message with a key with parameter and return a value in sequence.
    /// <code>
    /// Messenger.Default.RegisterFunc(
    ///     "The key of the message with in and out param",
    ///     (input) =>
    ///     {
    ///         if (input is StringInput stringInput)
    ///         {
    ///             this.Message = $"Receive the message with in param :{stringInput.Value}";
    ///         }
    ///
    ///         return new StringOutput("the out value is Happy New Year Too!");
    ///     }
    /// );
    /// //Usage
    /// this.RequestAsyncCommand = new AsyncCommand(async () =>
    /// {
    ///     var res = await Messenger.Default.RequestAsync(
    ///         "The key of the message with in and out param",
    ///         new StringInput("Happy New Year!"),
    ///         CancellationToken.None
    ///     );
    ///     if (res is StringOutput stringOutput)
    ///     {
    ///         SharpBoxes.WPFHelpers.Dialogs.MessageBoxes.Information(stringOutput.Value);
    ///     }
    /// });
    /// </code>
    /// </example>
    /// </summary>
    public static Messenger Default = new Messenger();

    /// <summary>
    /// Registers an action callback with the specified key.
    /// </summary>
    /// <param name="key">The key to associate with the action callback.</param>
    /// <param name="callBack">The action callback to register.</param>
    /// <returns><c>true</c> if the action callback was successfully registered; otherwise, <c>false</c>.</returns>
    public bool RegisterAction(string key, Action<IMessengerInput> callBack)
    {
        if (ActionCallBacks.ContainsKey(key))
        {
            ActionCallBacks[key] = callBack;
            //throw new NotSupportedException("same key exists!");
        }
        return ActionCallBacks.TryAdd(key, callBack);
    }

    /// <summary>
    /// Registers an action callback with the specified key.
    /// </summary>
    /// <param name="key">The key to associate with the action callback.</param>
    /// <param name="callBack">The action callback to register.</param>
    /// <returns><c>true</c> if the action callback was successfully registered; otherwise, <c>false</c>.</returns>
    public bool RegisterAction(string key, Action callBack)
    {
        if (ActionNoParamCallBacks.ContainsKey(key))
        {
            ActionNoParamCallBacks[key] = callBack;
            //throw new NotSupportedException("same key exists!");
        }
        return ActionNoParamCallBacks.TryAdd(key, callBack);
    }

    /// <summary>
    /// Sends a message with the specified key and data.
    /// </summary>
    /// <param name="key">The key associated with the message.</param>
    /// <param name="data">The data to be sent with the message.</param>
    public void Send(string key, IMessengerInput data)
    {
        if (ActionCallBacks.ContainsKey(key) is false)
        {
            throw new NotSupportedException($"key: {key} not found in the repository");
        }

        ActionCallBacks.TryGetValue(key, out var callback);
        callback?.Invoke(data);
    }

    /// <summary>
    /// Sends a message with the specified key.
    /// </summary>
    /// <param name="key">The key of the message.</param>
    /// <exception cref="NotSupportedException">Thrown when the specified key is not found in the repository.</exception>
    public void Send(string key)
    {
        if (ActionNoParamCallBacks.ContainsKey(key) is false)
        {
            throw new NotSupportedException($"key: {key} not found in the repository");
        }

        ActionNoParamCallBacks.TryGetValue(key, out var callback);
        callback?.Invoke();
    }

    /// <summary>
    /// Sends a message with the specified key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Task SendAsync(string key)
    {
        return Task.Factory.StartNew(() => Send(key));
    }

    /// <summary>
    /// Sends a message with the specified key and data.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task SendAsync(string key, IMessengerInput data)
    {
        return Task.Factory.StartNew(() => Send(key, data));
    }

    /// <summary>
    /// Registers a callback function with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the callback function.</param>
    /// <param name="callBack">The callback function to register.</param>
    /// <returns>True if the callback function was successfully registered; otherwise, false.</returns>
    public bool RegisterFunc(string key, Func<IMessengerInput, IMessengerOutput> callBack)
    {
        if (FuncCallBacks.ContainsKey(key))
        {
            FuncCallBacks[key] = callBack;
            //throw new NotSupportedException("same key exists!");
        }
        return FuncCallBacks.TryAdd(key, callBack);
    }

    /// <summary>
    /// Send a request by key and wait the response.
    /// </summary>
    /// <param name="key">The key associated with the callback function.</param>
    /// <param name="data">The data to be transfered.</param>
    /// <returns>The request result</returns>
    /// <exception cref="NotSupportedException">Throw if there is no key exists</exception>
    public IMessengerOutput Request(string key, IMessengerInput data)
    {
        if (FuncCallBacks.ContainsKey(key) is false)
        {
            throw new NotSupportedException($"key: {key} not found in the repository");
        }

        FuncCallBacks.TryGetValue(key, out var callback);
        return callback?.Invoke(data);
    }

    /// <summary>
    /// Sends a request message with the specified key and data to the messenger.
    /// </summary>
    /// <param name="key">The key associated with the request.</param>
    /// <param name="data">The input data for the request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the request.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the output of the request.</returns>
    public async Task<IMessengerOutput> RequestAsync(
        string key,
        IMessengerInput data,
        CancellationToken cancellationToken
    )
    {
        if (FuncCallBacks.ContainsKey(key) is false)
        {
            throw new NotSupportedException($"key: {key} not found in the repository");
        }

        FuncCallBacks.TryGetValue(key, out var callback);
        var res = await Task.Factory.StartNew<IMessengerOutput>(
            d => callback?.Invoke(d as IMessengerInput),
            (object)data,
            cancellationToken
        );
        return res;
    }
}
