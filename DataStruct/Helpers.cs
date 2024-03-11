using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace SharpBoxes.DataStruct
{
    /// <summary>
    /// 辅助类
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// 将字典转换为Json字符串
        /// </summary>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="dict">字典</param>
        /// <returns>Json字符串</returns>
        public static string DictToJson<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            return JsonConvert.SerializeObject(dict);
        }

        /// <summary>
        /// 将Json字符串转换为字典
        /// </summary>
        /// <typeparam name="TKey">键的类型</typeparam>
        /// <typeparam name="TValue">值的类型</typeparam>
        /// <param name="json">Json字符串</param>
        /// <returns>字典</returns>
        public static Dictionary<TKey, TValue> JsonToDict<TKey, TValue>(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
        }

        /// <summary>
        /// 通过Json进行深度复制
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns>复制的对象</returns>
        public static T CloneByJson<T>(T t)
        {
            return JsonConvert.DeserializeObject<T>(
                JsonConvert.SerializeObject(
                    t,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    }
                ),
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                }
            );
        }

        /// <summary>
        /// 将列表转换为数据表
        /// </summary>
        /// <typeparam name="T">列表中的对象类型</typeparam>
        /// <param name="datas">列表</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTable<T>(this List<T> datas)
            where T : class
        {
            DataTable dt = new DataTable();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            foreach (T data in datas)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in properties)
                {
                    dr[pi.Name] = pi.GetValue(data, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 将数据表转换为列表
        /// </summary>
        /// <typeparam name="T">列表中的对象类型</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns>列表</returns>
        public static List<T> ToList<T>(this DataTable dt)
            where T : class, new()
        {
            List<T> datas = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (DataRow dr in dt.Rows)
            {
                T data = new T();
                foreach (PropertyInfo pi in properties)
                {
                    if (dt.Columns.Contains(pi.Name))
                    {
                        pi.SetValue(data, dr[pi.Name], null);
                    }
                }
                datas.Add(data);
            }
            return datas;
        }

        /// <summary>
        /// 将字符串转换为double数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>double数组</returns>
        public static double[] ToDoubleArr(this string str, string[] separator)
        {
            return str.Split(separator, StringSplitOptions.None)
                .Select(x => double.Parse(x))
                .ToArray();
        }

        /// <summary>
        /// 将字符串转换为int数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>int数组</returns>
        public static int[] ToIntArr(this string str, string[] separator)
        {
            return str.Split(separator, StringSplitOptions.None)
                .Select(x => int.Parse(x))
                .ToArray();
        }

        /// <summary>
        /// 将字符串转换为字符串数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>字符串数组</returns>
        public static string[] ToStringArr(this string str, string[] separator)
        {
            return str.Split(separator, StringSplitOptions.None);
        }

        /// <summary>
        /// 将字符串数组转换为字符串
        /// </summary>
        /// <param name="arr">字符串数组</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>字符串</returns>
        public static string ToStr(this IEnumerable<string> arr, string separator)
        {
            return string.Join(separator, arr);
        }

        /// <summary>
        /// 将int数组转换为字符串
        /// </summary>
        /// <param name="arr">int数组</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>字符串</returns>
        public static string ToStr(this IEnumerable<int> arr, string separator)
        {
            return string.Join(separator, arr);
        }

        /// <summary>
        /// 将double数组转换为字符串
        /// </summary>
        /// <param name="arr">double数组</param>
        /// <param name="separator">分隔符，默认为逗号</param>
        /// <returns>字符串</returns>
        public static string ToStr(this IEnumerable<double> arr, string separator)
        {
            return string.Join(separator, arr);
        }
    }
}
