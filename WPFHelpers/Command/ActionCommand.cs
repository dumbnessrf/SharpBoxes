using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 表示可以执行操作的命令。
    /// <example>
    /// <strong>下面示例了如何创建一个同步命令</strong>
    ///<code>
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
    /// public IActionCommand UpdateSyncCommand { get; private set; }
    /// UpdateSyncCommand = new ActionCommand(
    ///     () =>
    ///     {
    ///         Thread.Sleep(1000);
    ///         this.Name = "James Harden";
    ///     },
    ///     () => true
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public class ActionCommand : IActionCommand
    {
        /// <summary>
        /// 可执行状态更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged;
        private bool _isExecuting;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// 初始化 <see cref="ActionCommand"/> 类的新实例。
        /// </summary>
        /// <param name="execute">要执行的操作。</param>
        /// <param name="canExecute">确定命令是否可以执行的函数。</param>
        /// <param name="errorHandler">错误处理器。</param>
        public ActionCommand(
            Action execute,
            Func<bool> canExecute = null,
            IErrorHandler errorHandler = null
        )
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 确定命令是否可以执行。
        /// </summary>
        /// <returns>
        /// 如果命令可以执行，则为 <c>true</c>；否则为 <c>false</c>。
        /// </returns>
        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        /// <summary>
        /// 执行命令。
        /// </summary>
        public void Execute()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    _execute();
                }
                catch (Exception ex)
                {
                    _errorHandler?.HandleError(ex);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 引发 <see cref="CanExecuteChanged"/> 事件。
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
            Execute();
        }
        #endregion
    }

    /// <summary>
    /// 表示可以执行操作的命令。
    /// </summary>
    /// <typeparam name="T">代表支持传入参数的命令。</typeparam>
    public class ActionCommand<T> : IActionCommand<T>
    {
        /// <summary>
        /// 当命令的可执行状态更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        /// <summary>
        /// 初始化 <see cref="ActionCommand{T}"/> 类的新实例。
        /// </summary>
        /// <param name="execute">执行方法实体</param>
        /// <param name="canExecute">确定命令是否可以执行的函数。</param>
        /// <param name="errorHandler">错误处理器</param>
        public ActionCommand(
            Action<T> execute,
            Func<T, bool> canExecute = null,
            IErrorHandler errorHandler = null
        )
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        /// <summary>
        /// 确定命令是否可以执行。
        /// </summary>
        /// <param name="parameter">要传递的参数</param>
        /// <returns></returns>
        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="parameter">要传入的参数</param>
        public void Execute(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 引发 <see cref="CanExecuteChanged"/> 事件。
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
            Execute((T)parameter);
        }
        #endregion
    }
}
