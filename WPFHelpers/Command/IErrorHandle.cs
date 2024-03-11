
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.WPFHelpers.Command
{
    /// <summary>
    /// 定义了一个错误处理接口
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="ex">需要处理的异常</param>
        void HandleError(Exception ex);
    }

    /// <summary>
    /// 提供了一些任务相关的实用方法
    /// </summary>
    public static class TaskUtilities
    {
        /// <summary>
        /// 安全地异步执行任务并忽略结果
        /// </summary>
        /// <param name="task">需要执行的任务</param>
        /// <param name="handler">错误处理器</param>
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(
            this Task task,
            IErrorHandler handler = null
        )
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
