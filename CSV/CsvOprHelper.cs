using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.CsvServices
{
    /// <summary>
    /// CsvOprHelper静态类，提供CSV数据的操作方法。
    /// </summary>
    public static class CsvOprHelper
    {
        /// <summary>
        /// 将<see cref="List{T}"/>转换为<see cref="DataTable"/>。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="datas">数据列表。</param>
        /// <param name="isUseDisplayName">是否使用DisplayName属性作为列名，默认为false。</param>
        /// <returns>转换后的<see cref="DataTable"/>。</returns>
        public static DataTable ToDT<T>(this List<T> datas, bool isUseDisplayName = false)
        {
            DataTable dt = new DataTable();
            var type = typeof(T);
            var props = type.GetProperties();
            Dictionary<string, string> name_displayName = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                if (isUseDisplayName)
                {
                    var displayName = prop.GetCustomAttributes(
                        typeof(System.ComponentModel.DisplayNameAttribute),
                        true
                    );
                    if (displayName.Length > 0)
                    {
                        var str = (
                            (System.ComponentModel.DisplayNameAttribute)displayName[0]
                        ).DisplayName;
                        dt.Columns.Add(str);
                        name_displayName.Add(prop.Name, str);
                    }
                    else
                    {
                        dt.Columns.Add(prop.Name);
                        name_displayName.Add(prop.Name, prop.Name);
                    }
                }
                else
                {
                    dt.Columns.Add(prop.Name);
                    name_displayName.Add(prop.Name, prop.Name);
                }
            }
            foreach (var item in datas)
            {
                DataRow dr = dt.NewRow();
                foreach (var prop in props)
                {
                    dr[name_displayName[prop.Name]] = prop.GetValue(item);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 将单个数据对象转换为<see cref="DataTable"/>。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="data">数据对象。</param>
        /// <param name="isUseDisplayName">是否使用DisplayName属性作为列名，默认为false。</param>
        /// <returns>转换后的<see cref="DataTable"/>。</returns>
        public static DataTable ToDT<T>(this T data, bool isUseDisplayName = false)
        {
            DataTable dt = new DataTable();
            var type = typeof(T);
            var props = type.GetProperties();
            Dictionary<string, string> name_displayName = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                if (isUseDisplayName)
                {
                    var displayName = prop.GetCustomAttributes(
                        typeof(System.ComponentModel.DisplayNameAttribute),
                        true
                    );
                    if (displayName.Length > 0)
                    {
                        var str = (
                            (System.ComponentModel.DisplayNameAttribute)displayName[0]
                        ).DisplayName;
                        dt.Columns.Add(str);
                        name_displayName.Add(prop.Name, str);
                    }
                    else
                    {
                        dt.Columns.Add(prop.Name);
                        name_displayName.Add(prop.Name, prop.Name);
                    }
                }
                else
                {
                    dt.Columns.Add(prop.Name);
                    name_displayName.Add(prop.Name, prop.Name);
                }
            }

            DataRow dr = dt.NewRow();
            foreach (var prop in props)
            {
                dr[name_displayName[prop.Name]] = prop.GetValue(data);
            }
            dt.Rows.Add(dr);

            return dt;
        }

        /// <summary>
        /// 将<see cref="DataTable"/>转换为CSV格式的<see cref="StringBuilder"/>。
        /// </summary>
        /// <param name="dt"><see cref="DataTable"/>对象。</param>
        /// <returns>转换后的CSV格式的<see cref="StringBuilder"/>。</returns>
        public static StringBuilder ToCSV(this DataTable dt)
        {
            var sb = new StringBuilder();

            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                sb.Append(dc.ColumnName + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("\r\n");
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append(dr[dc.ColumnName].ToString() + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("\r\n");
            }

            return sb;
        }

        /// <summary>
        /// 将<see cref="List{CsvDataBase}"/>转换为CSV格式的<see cref="StringBuilder"/>。
        /// </summary>
        /// <param name="csvDatas">CSV数据列表。</param>
        /// <returns>转换后的CSV格式的<see cref="StringBuilder"/>。</returns>
        public static StringBuilder ToCSV(List<CsvDataBase> csvDatas)
        {
            var sb = new StringBuilder();

            foreach (var item in csvDatas)
            {
                sb = item.Append(sb);
            }

            return sb;
        }

        /// <summary>
        /// 将<see cref="StringBuilder"/>保存为文件。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>对象。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当写入文件时发生错误。</exception>
        public static void SaveToFile(this StringBuilder sb, string filename)
        {
            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 将<see cref="StringBuilder"/>追加到文件。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>对象。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当写入文件时发生错误。</exception>
        public static void AppendDataToFile(StringBuilder sb, string filename)
        {
            File.AppendAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 将<see cref="CsvDataBase"/>追加到文件。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="data">数据对象。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当写入文件时发生错误。</exception>
        public static void AppendDataToFile<T>(T data, string filename)
            where T : CsvDataBase
        {
            var rawText = File.ReadAllText(filename);
            var sb = new StringBuilder(rawText);
            sb = data.Append(sb);
            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 将<see cref="List{CsvDataBase}"/>追加到文件。
        /// </summary>
        /// <typeparam name="T">数据类型。</typeparam>
        /// <param name="datas">数据列表。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当写入文件时发生错误。</exception>
        public static void AppendDataToFile<T>(List<T> datas, string filename)
            where T : CsvDataBase
        {
            var rawText = File.ReadAllText(filename);
            var sb = new StringBuilder(rawText);
            foreach (var data in datas)
            {
                sb = data.Append(sb);
            }
            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 从文件中移除第一个满足条件的行。
        /// </summary>
        /// <param name="match">条件。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当读取或写入文件时发生错误。</exception>
        public static void RemoveFirstSpecificRowInFile(Predicate<string> match, string filename)
        {
            var rawText = File.ReadAllLines(filename).ToList();
            rawText.RemoveAt(rawText.FindIndex(match));
        }

        /// <summary>
        /// 从文件中移除所有满足条件的行。
        /// </summary>
        /// <param name="match">条件。</param>
        /// <param name="filename">文件名。</param>
        /// <exception cref="IOException">当读取或写入文件时发生错误。</exception>
        public static void RemoveAllSpecificRowInFile(Predicate<string> match, string filename)
        {
            var rawText = File.ReadAllLines(filename).ToList();

            rawText.RemoveAll(match);
            File.WriteAllLines(filename, rawText, Encoding.UTF8);
        }
    }
}
