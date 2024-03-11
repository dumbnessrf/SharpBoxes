
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 定义了一个异步命令接口
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task ExecuteAsync();

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <returns>如果可以执行返回 true，否则返回 false</returns>
        bool CanExecute();
    }

    /// <summary>
    /// 定义了一个带参数传递的异步命令接口
    /// </summary>
    /// <typeparam name="T">传递参数的类型</typeparam>
    public interface IAsyncCommand<in T> : ICommand
    {
        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <param name="parameter">传递的参数</param>
        /// <returns>表示异步操作的任务</returns>
        Task ExecuteAsync(T parameter);

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">传递的参数</param>
        /// <returns>如果可以执行返回 true，否则返回 false</returns>
        bool CanExecute(T parameter);
    }
}
