using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.CsvServices
{
    /// <summary>
    /// CsvDataNormal类，继承自<see cref="CsvDataBase"/>抽象类。
    /// 该类主要用于处理常规的CSV数据。
    /// </summary>
    internal class CsvDataNormal<T> : CsvDataBase
    {
        /// <summary>
        /// DataTable对象，用于存储CSV数据。
        /// </summary>
        private DataTable dt;

        /// <summary>
        /// 构造函数，接收一个<see cref="List{T}"/>作为CSV数据。
        /// </summary>
        /// <param name="datas">数据列表，每个元素代表CSV中的一条数据。</param>
        public CsvDataNormal(List<T> datas)
        {
            dt = CsvOprHelper.ToDT(datas);
        }

        /// <summary>
        /// 构造函数，接收一个数据对象作为CSV数据。
        /// </summary>
        /// <param name="data">数据对象，代表CSV中的一条数据。</param>
        public CsvDataNormal(T data)
        {
            dt = CsvOprHelper.ToDT(data);
        }

        /// <summary>
        /// 重写Append方法，将dt中的每条数据追加到<see cref="StringBuilder"/>对象。
        /// 每条数据后面都会添加一个逗号，最后一条数据后的逗号会被移除，然后添加一个换行符。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>对象，用于存储和处理字符串。</param>
        /// <returns>追加了dt中数据的<see cref="StringBuilder"/>对象。</returns>
        public override StringBuilder Append(StringBuilder sb)
        {
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                sb.Append(dc.ColumnName + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(Environment.NewLine);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append(dr[dc.ColumnName].ToString() + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(Environment.NewLine);
            }
            return sb;
        }

        /// <summary>
        /// 重写GetString方法，但该方法尚未实现。
        /// </summary>
        /// <returns>抛出<see cref="System.NotImplementedException"/>异常。</returns>
        /// <exception cref="System.NotImplementedException">当方法未实现时抛出此异常。</exception>
        public override string GetString()
        {
            throw new NotImplementedException();
        }
    }
}
