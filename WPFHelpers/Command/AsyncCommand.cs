using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 异步命令类，实现了<see cref="IAsyncCommand"/>接口
    /// <example>
    ///<strong>下面示例了如何创建一个异步命令</strong>
    /// <code>
    /// private string name;
    /// public string Name
    /// {
    ///     get => name;
    ///     set
    ///     {
    ///         OnPropertyChanging(name, value);
    ///         name = value;
    ///         OnPropertyChanged(name, value);
    ///     }
    /// }
    /// public IAsyncCommand UpdateAsyncCommand { get; private set; }
    /// UpdateAsyncCommand = new AsyncCommand(
    ///     async () =>
    ///     {
    ///         await Task.Delay(1000);
    ///         this.Name = "Clay Tomposon";
    ///     },
    ///     () => true
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        /// <summary>
        /// 当命令可执行状态改变时发生
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// 初始化 <see cref="AsyncCommand"/> 类的新实例
        /// </summary>
        /// <param name="execute">执行方法实体</param>
        /// <param name="canExecute">确定命令是否可以执行的函数</param>
        /// <param name="errorHandler">错误处理器</param>
        public AsyncCommand(
            Func<Task> execute,
            Func<bool> canExecute = null,
            IErrorHandler errorHandler = null
        )
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 确定命令是否可以执行
        /// </summary>
        /// <returns>如果可以执行返回 true，否则返回 false</returns>
        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 引发 <see cref="CanExecuteChanged"/> 事件
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }

    /// <summary>
    /// 异步命令类，实现了<see cref="IAsyncCommand{T}"/>接口，支持参数
    /// </summary>
    /// <typeparam name="T">命令参数类型</typeparam>
    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        /// <summary>
        /// 当命令可执行状态改变时发生
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// 初始化 <see cref="AsyncCommand{T}"/> 类的新实例
        /// </summary>
        /// <param name="execute">执行方法实体</param>
        /// <param name="canExecute">确定命令是否可以执行的函数</param>
        /// <param name="errorHandler">错误处理器</param>
        public AsyncCommand(
            Func<T, Task> execute,
            Func<T, bool> canExecute = null,
            IErrorHandler errorHandler = null
        )
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 确定命令是否可以执行
        /// </summary>
        /// <param name="parameter">要传递的参数</param>
        /// <returns>如果可以执行返回 true，否则返回 false</returns>
        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <param name="parameter">要传入的参数</param>
        /// <returns>表示异步操作的任务</returns>
        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 引发 <see cref="CanExecuteChanged"/> 事件
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }
}
