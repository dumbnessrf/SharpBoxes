
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpBoxes.Dlls
{
    /// <summary>
    /// Dll模型类
    /// </summary>
    public class DllModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="path">路径</param>
        /// <param name="assembly">程序集</param>
        /// <param name="baseTypeFilter">基类型过滤器</param>
        public DllModel(string name, string path, Assembly assembly, Type baseTypeFilter)
        {
            Name = name;
            Path = path;
            Assembly = assembly;
            Types = LibLoadHelper.FindSpecifiedTypeInheritFromAssembly(assembly, baseTypeFilter);
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        private Assembly Assembly { get; set; }

        /// <summary>
        /// 类型列表
        /// </summary>
        private List<Type> Types { get; set; }

        /// <summary>
        /// 从Dll创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        /// <returns>创建的对象</returns>
        public T CreateObjectFromDll<T>(out string message, object[] args = null)
        {
            message = "加载成功";
            args ??= new object[] { };
            try
            {
                var instance = Activator.CreateInstance(Types.First(), args);
                return (T)instance;
            }
            catch (Exception ex)
            {
                message = $"加载{Assembly}失败：{ex}";
                return default(T);
            }
        }
    }
}
