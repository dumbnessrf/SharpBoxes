using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 定义了一个同步命令接口
    /// </summary>
    public interface IActionCommand : ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        void Execute();

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <returns></returns>
        bool CanExecute();
    }

    /// <summary>
    /// 定义了一个带参数传递命令接口
    /// </summary>
    /// <typeparam name="T">传递参数的类型</typeparam>
    public interface IActionCommand<in T> : ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">传递的参数</param>
        void Execute(T parameter);

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">传递的参数</param>
        /// <returns></returns>
        bool CanExecute(T parameter);
    }
}
