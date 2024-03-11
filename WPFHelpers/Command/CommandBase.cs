using System;
using System.Windows.Input;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 命令基类，实现了<see cref="ICommand"/>接口
    /// <example>
    /// <strong>下面示例演示了如何创建一个自定义的异步命令</strong>
    ///<code>
    /// public class CustomAsyncCommand : CommandBase
    /// {
    ///     private readonly Student _student;
    ///     public CustomAsyncCommand(Student student)
    ///     {
    ///         _student = student;
    ///     }
    ///     public override async void Execute(object parameter)
    ///     {
    ///         await Task.Factory.StartNew(() =>
    ///         {
    ///             try
    ///             {
    ///                 Task.Delay(1000);
    ///                 _student.Study(parameter);
    ///             }
    ///             catch (Exception e)
    ///             {
    ///                 Console.WriteLine(e);
    ///                 throw;
    ///             }
    ///         });
    ///     }
    /// }
    ///
    /// public ICommand CustomCommand { get; private set; }
    /// CustomCommand = new CustomAsyncCommand(new Student { Name = "Tom Jesus", Age = 2022 });
    /// </code>
    /// </example>
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// 确定命令是否可以执行
        /// </summary>
        /// <param name="parameter">要传递的参数</param>
        /// <returns>如果可以执行返回 true，否则返回 false</returns>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">要传递的参数</param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// 当命令可执行状态改变时发生
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 引发 <see cref="CanExecuteChanged"/> 事件
        /// </summary>
        protected void OnCanExcutedChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}