
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpBoxes.Dlls
{
    /// <summary>
    /// <para>提供库加载帮助的静态类。</para>
    /// <para>主要功能包括加载指定的DLL，获取指定类型的实例，从指定的文件夹中获取DLL文件，从指定的程序集中查找指定类型的子类或实现了指定接口的类等。</para>
    /// </summary>
    public static class LibLoadHelper
    {
        /// <summary>
        /// <para>加载指定的DLL并获取指定类型的实例。</para>
        /// <para>首先加载指定名称的DLL，然后获取指定名称空间的指定类型，最后创建并返回该类型的实例。</para>
        /// </summary>
        /// <typeparam name="T">期望的类型</typeparam>
        /// <param name="dllName">DLL名称</param>
        /// <param name="namespaceName">命名空间名称</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="args">构造函数参数</param>
        /// <param name="message">返回的消息</param>
        /// <returns>创建的实例</returns>
        public static T LoadDll<T>(
            string dllName,
            string namespaceName,
            string typeName,
            object[] args,
            out string message
        )
            where T : class
        {
            message = "加载成功";

            //加载指定名称的DLL
            var assembly = Assembly.Load(dllName);
            //获取指定名称空间的指定类型
            var type = assembly.GetType($"{namespaceName}.{typeName}");
            if (type == null)
            {
                message = $"{namespaceName}.{typeName}类型获取失败，返回null";
                return null;
            }
            //获取指定类型的实例
            try
            {
                var instance = Activator.CreateInstance(type, args) as T;
                if (instance == null)
                {
                    message = $"{namespaceName}.{typeName}构造失败，返回null";
                }
                return instance;
            }
            catch (Exception ex)
            {
                message = $"{namespaceName}.{typeName}构造失败{ex}";
                return null;
            }
        }

        /// <summary>
        /// <para>加载指定的DLL并获取指定类型的实例。</para>
        /// <para>首先加载指定名称的DLL，然后获取指定名称空间的指定类型，最后创建并返回该类型的实例。</para>
        /// </summary>
        /// <typeparam name="T">期望的类型</typeparam>
        /// <param name="dllName">DLL名称</param>
        /// <param name="namespaceName">命名空间名称</param>
        /// <param name="typeName">类型名称</param>
        /// <param name="message">返回的消息</param>
        /// <returns>创建的实例</returns>
        public static T LoadDll<T>(
            string dllName,
            string namespaceName,
            string typeName,
            out string message
        )
            where T : class
        {
            message = "加载成功";
            //加载指定名称的DLL
            var assembly = Assembly.Load(dllName);
            //获取指定名称空间的指定类型
            var type = assembly.GetType($"{namespaceName}.{typeName}");
            if (type == null)
            {
                message = $"{namespaceName}.{typeName}类型获取失败，返回null";
                return null;
            }
            //获取指定类型的实例
            try
            {
                var instance = Activator.CreateInstance(type) as T;
                if (instance == null)
                {
                    message = $"{namespaceName}.{typeName}构造失败，返回null";
                }
                return instance;
            }
            catch (Exception ex)
            {
                message = $"{namespaceName}.{typeName}构造失败{ex}";
                return null;
            }
        }

        /// <summary>
        /// <para>从指定的文件夹中获取DLL文件。</para>
        /// <para>遍历文件夹中的所有文件，只保留可以成功加载的DLL文件。</para>
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <returns>DLL文件列表</returns>
        private static List<string> GeDllFiles(string folder)
        {
            var files = Directory.GetFiles(folder, "*.dll").ToList();
            var rubbishes = new List<string>();
            foreach (var file in files)
            {
                try
                {
                    _ = Assembly.LoadFile(file);
                }
                catch (Exception)
                {
                    rubbishes.Add(file);
                }
            }
            files.RemoveAll(s => rubbishes.Contains(s));
            return files;
        }

        /// <summary>
        /// <para>从指定的文件夹中获取DllModel列表。</para>
        /// <para>首先获取文件夹中的所有DLL文件，然后为每个文件创建一个DllModel实例。</para>
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="baseTypeFilter">基类型过滤器</param>
        /// <returns>DllModel列表</returns>
        public static List<DllModel> GetDllModelsFromFolder(string folder, Type baseTypeFilter)
        {
            var files = GeDllFiles(folder);
            var dllModels = new List<DllModel>();
            foreach (var file in files)
            {
                var dllModel = new DllModel(
                    Path.GetFileNameWithoutExtension(file),
                    file,
                    Assembly.LoadFile(file),
                    baseTypeFilter
                );

                dllModels.Add(dllModel);
            }
            return dllModels;
        }

        /// <summary>
        /// <para>从指定的程序集中查找指定类型的子类或实现了指定接口的类。</para>
        /// <para>遍历程序集中的所有类型，只保留是指定基类型的子类或实现了指定接口的类型。</para>
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="baseType">基类型或接口类型</param>
        /// <returns>找到的类型列表</returns>
        public static List<Type> FindSpecifiedTypeInheritFromAssembly(
            Assembly assembly,
            Type baseType
        )
        {
            var types = assembly.GetTypes();
            return types
                .Where(
                    type =>
                        type.IsSubclassOf(baseType)
                        || type.GetInterface(baseType.FullName) is not null
                )
                .ToList();
        }

        /// <summary>
        /// <para>创建指定类型的实例。</para>
        /// <para>使用Activator.CreateInstance方法创建指定类型的实例。</para>
        /// </summary>
        /// <typeparam name="T">期望的类型</typeparam>
        /// <param name="message">返回的消息</param>
        /// <param name="args">构造函数参数</param>
        /// <returns>创建的实例</returns>
        public static T CreateObjectFromType<T>(out string message, object[] args = null)
        {
            message = "创建实例成功";
            args ??= new object[] { };
            try
            {
                var instance = Activator.CreateInstance(typeof(T), args);
                return (T)instance;
            }
            catch (Exception ex)
            {
                message = $"创建实例{typeof(T)}失败：{ex}";
                return default(T);
            }
        }

        /// <summary>
        /// <para>从指定的程序集中查找附加了指定Attribute的类型。</para>
        /// <para>遍历程序集中的所有类型，只保留附加了指定Attribute的类型。</para>
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="attr">Attribute类型</param>
        /// <returns>找到的类型列表</returns>
        public static List<Type> FindSpecifiedTypeHasAttributeFromAssembly(
            Assembly assembly,
            Type attr
        )
        {
            var types = assembly.GetTypes();
            return types.Where(type => type.GetCustomAttribute(attr) != null).ToList();
        }

        /// <summary>
        /// <para>从指定的类型中查找附加了指定Attribute的属性。</para>
        /// <para>遍历类型中的所有属性，只保留附加了指定Attribute的属性。</para>
        /// </summary>
        /// <param name="classType">类类型</param>
        /// <param name="attrs">Attribute类型列表</param>
        /// <returns>找到的属性列表</returns>
        public static List<PropertyInfo> FindSpecifiedPropertyHasAttributeFromType(
            Type classType,
            params Type[] attrs
        )
        {
            var properties = classType.GetProperties();
            List<PropertyInfo> propertyList = new();
            foreach (var prop in properties)
            {
                foreach (var attr in attrs)
                {
                    if (prop.GetCustomAttribute(attr) != null)
                    {
                        propertyList.Add(prop);
                    }
                }
            }
            return propertyList;
        }

        /// <summary>
        /// <para>从指定的类型中查找没有附加指定Attribute的属性。</para>
        /// <para>遍历类型中的所有属性，只保留没有附加指定Attribute的属性。</para>
        /// </summary>
        /// <param name="classType">类类型</param>
        /// <param name="attrs">Attribute类型列表</param>
        /// <returns>找到的属性列表</returns>
        public static List<PropertyInfo> FindSpecifiedPropertyExceptAttributeFromType(
            Type classType,
            params Type[] attrs
        )
        {
            var properties = classType.GetProperties();
            List<PropertyInfo> propertyList = new();
            foreach (var prop in properties)
            {
                foreach (var attr in attrs)
                {
                    if (prop.GetCustomAttribute(attr) == null)
                    {
                        propertyList.Add(prop);
                    }
                }
            }
            return propertyList;
        }

        /// <summary>
        /// <para>从指定的类型中查找附加了指定Attribute的方法。</para>
        /// <para>遍历类型中的所有方法，只保留附加了指定Attribute的方法。</para>
        /// </summary>
        /// <param name="classType">类类型</param>
        /// <param name="attr">Attribute类型</param>
        /// <returns>找到的方法列表</returns>
        public static List<MethodInfo> FindSpecifiedMethodHasAttributeFromType(
            Type classType,
            Type attr
        )
        {
            var methods = classType.GetMethods();
            List<MethodInfo> methodList = new();
            foreach (var method in methods)
            {
                if (method.GetCustomAttribute(attr) != null)
                {
                    methodList.Add(method);
                }
            }
            return methodList;
        }
    }
}