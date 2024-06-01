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
        public static string DictToJson<TKey, TValue>(this Dictionary<TKey, TValue> dict)
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
        public static Dictionary<TKey, TValue> JsonToDict<TKey, TValue>(this string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
        }

        /// <summary>
        /// 通过Json进行深度复制
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns>复制的对象</returns>
        public static T CloneByJson<T>(this T t)
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

        public static DataTable ListToDataTable<T>(List<T> datas, bool isUseDisplayName = false)
        {
            DataTable dt = new DataTable();
            var propertys = typeof(T).GetProperties();
            foreach (var p in propertys)
            {
                if (isUseDisplayName)
                {
                    var attrs = p.GetCustomAttributes(
                        typeof(System.ComponentModel.DisplayNameAttribute),
                        false
                    );
                    if (attrs.Count() > 0)
                    {
                        var attr = attrs[0] as System.ComponentModel.DisplayNameAttribute;
                        dt.Columns.Add(attr.DisplayName, p.PropertyType);
                    }
                    else
                    {
                        dt.Columns.Add(p.Name, p.PropertyType);
                    }
                }
                else
                {
                    dt.Columns.Add(p.Name, p.PropertyType);
                }
            }
            foreach (var d in datas)
            {
                DataRow dr = dt.NewRow();
                foreach (var p in propertys)
                {
                    dr[p.Name] = p.GetValue(d);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static List<T> DataTableToList<T>(this DataTable dt, bool isUseDisplayName = false)
        {
            List<T> list = new List<T>();
            var propertys = typeof(T).GetProperties();
            foreach (DataRow item in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();
                foreach (var p in propertys)
                {
                    if (isUseDisplayName)
                    {
                        var attrs = p.GetCustomAttributes(
                            typeof(System.ComponentModel.DisplayNameAttribute),
                            false
                        );
                        if (attrs.Count() > 0)
                        {
                            var attr = attrs[0] as System.ComponentModel.DisplayNameAttribute;
                            p.SetValue(
                                t,
                                Convert.ChangeType(item[attr.DisplayName], p.PropertyType)
                            );
                        }
                        else
                        {
                            p.SetValue(t, Convert.ChangeType(item[p.Name], p.PropertyType));
                        }
                    }
                    else
                    {
                        p.SetValue(t, Convert.ChangeType(item[p.Name], p.PropertyType));
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 查找所有符合条件的下标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static List<int> FindAllIndex<T>(this List<T> values, Predicate<T> predicate)
        {
            var indexes = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                if (predicate(values[i]))
                    indexes.Add(i);
            }
            return indexes;
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
