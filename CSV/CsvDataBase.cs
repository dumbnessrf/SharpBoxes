using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoxes.CsvServices
{
    /// <summary>
    /// CSV数据的抽象基类。
    /// <example>
    /// 下面的示例演示了如何使用<see cref="CsvDataBase"/>类。
    /// <code>
    /// //模拟数据
    /// var datas1 = ClassInfo.FakeMany(2);
    /// var datas2 = Student.FakeMany(10);
    /// //创建CSV文件并添加数据
    /// CsvOprHelper
    ///     .ToCSV(
    ///         new List&lt;CsvDataBase&gt;()
    ///         {
    ///             new CsvDataNormal&lt;ClassInfo&gt;(datas1),
    ///             new CsvDataBlank(),
    ///             new CsvDataBlank(),
    ///             new CsvDataBlank(),
    ///             new CsvDataNormal&lt;Student&gt;(datas2),
    ///             new CsvDataBlank(),
    ///         }
    ///     )
    ///     .SaveToFile(@"C:\Users\zheng\Desktop\工作簿1.xlsx");
    /// //在已有的CSV文件中添加数据
    /// CsvOprHelper.AppendDataToFile(
    ///     new List&lt;CsvDataBase&gt;()
    ///     {
    ///         new CsvDataCustom("a", "b", "c"),
    ///         new CsvDataBlank(),
    ///         new CsvDataCustom(new[] { "e", "f", "g" }),
    ///         new CsvDataBlank(),
    ///         new CsvDataCustom("h,i,j"),
    ///         new CsvDataNormal&lt;ClassInfo&gt;(datas1),
    ///         new CsvDataBlank(),
    ///         new CsvDataNormal&lt;Student&gt;(datas2),
    ///         new CsvDataBlank(),
    ///     },
    ///     @"C:\Users\zheng\Desktop\工作簿1.xlsx"
    /// );
    /// </code>
    ///
    ///
    /// </example>
    ///
    /// <example>
    /// 更方便的写法
    /// <code>
    /// CsvOprHelper.AppendDataToFile(
    ///     new CsvDataCustom("a", "b", "c")
    ///         .AddBlank()
    ///         .AddCustom([ "e", "f", "g" ])
    ///         .AddBlank()
    ///         .AddCustom("h,i,j")
    ///         .AddNormal(datas1)
    ///         .AddBlank()
    ///         .AddNormal(datas2)
    ///         .ToList(),
    ///     @"C:\Users\zheng\Desktop\工作簿1.xlsx"
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public abstract class CsvDataBase
    {
        /// <summary>
        /// 获取CSV数据的字符串表示形式。
        /// </summary>
        /// <returns>表示CSV数据的字符串。</returns>
        public abstract string GetString();

        /// <summary>
        /// 将CSV数据追加到提供的StringBuilder。
        /// </summary>
        /// <param name="sb">要追加CSV数据的StringBuilder。</param>
        /// <returns>追加了CSV数据后的StringBuilder。</returns>
        public abstract StringBuilder Append(StringBuilder sb);
    }
}
