using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoxes.CsvServices
{
    /// <summary>
    /// CsvDataCustom类，继承自<see cref="CsvDataBase"/>抽象类。
    /// 该类主要用于处理自定义的CSV数据。
    /// </summary>
    public class CsvDataCustom : CsvDataBase
    {
        /// <summary>
        /// 数据列表，存储CSV数据。
        /// </summary>
        public List<string> Datas { get; }

        /// <summary>
        /// 构造函数，接收一个<see cref="List{string}"/>作为CSV数据。
        /// </summary>
        /// <param name="datas">字符串列表，每个字符串代表CSV中的一条数据。</param>
        public CsvDataCustom(List<string> datas)
        {
            Datas = datas;
        }

        /// <summary>
        /// 构造函数，接收一个字符串数组作为CSV数据。
        /// </summary>
        /// <param name="datas">字符串数组，每个字符串代表CSV中的一条数据。</param>
        public CsvDataCustom(params string[] datas)
        {
            Datas = new List<string>(datas);
        }

        /// <summary>
        /// 构造函数，接收一个字符串作为CSV数据。
        /// 如果字符串中包含逗号，则会被分割成多条数据；如果不包含逗号，则会被当作一条数据。
        /// </summary>
        /// <param name="datas">字符串，代表CSV中的数据。</param>
        public CsvDataCustom(string datas)
        {
            Datas = new List<string>();
            if (datas.Contains(","))
            {
                Datas.AddRange(datas.Split(','));
            }
            else
            {
                Datas.Add(datas);
            }
        }

        /// <summary>
        /// 重写Append方法，将Datas中的每条数据追加到<see cref="StringBuilder"/>对象。
        /// 每条数据后面都会添加一个逗号，最后一条数据后的逗号会被移除，然后添加一个换行符。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>对象，用于存储和处理字符串。</param>
        /// <returns>追加了Datas中数据的<see cref="StringBuilder"/>对象。</returns>
        public override StringBuilder Append(StringBuilder sb)
        {
            foreach (var data in Datas)
            {
                sb.Append(data + ",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(Environment.NewLine);
            return sb;
        }

        /// <summary>
        /// 重写GetString方法，但该方法尚未实现。
        /// </summary>
        /// <returns>抛出<see cref="System.NotImplementedException"/>异常。</returns>
        /// <exception cref="System.NotImplementedException">当方法未实现时抛出此异常。</exception>
        public override string GetString()
        {
            throw new System.NotImplementedException();
        }
    }
}
