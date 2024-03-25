using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.CsvServices
{
    /// <summary>
    /// CsvDataBlank类，继承自CsvDataBase抽象类。
    /// 该类主要用于处理空白的CSV数据。
    /// </summary>
    internal class CsvDataBlank : CsvDataBase
    {
        /// <summary>
        /// 重写Append方法，将GetString方法的返回值追加到StringBuilder对象。
        /// 该方法的主要作用是将获取到的CSV数据字符串添加到StringBuilder对象中，以便于后续的数据处理和操作。
        /// </summary>
        /// <param name="sb">StringBuilder对象，用于存储和处理字符串。</param>
        /// <returns>追加了GetString方法返回值的StringBuilder对象。</returns>
        public override StringBuilder Append(StringBuilder sb)
        {
            return sb.Append(GetString());
        }

        /// <summary>
        /// 重写GetString方法，返回一个新行。
        /// 该方法的主要作用是生成一个新的行字符串，用于表示CSV数据中的空白行。
        /// </summary>
        /// <returns>新行字符串，表示CSV数据中的空白行。</returns>
        public override string GetString()
        {
            return Environment.NewLine;
        }
    }
}
