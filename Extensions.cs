using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBoxes
{
    public static class DataStructHelpers
    {
        #region 字典相关
        public static Dictionary<TKey, TValue> ToDict<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> data
        )
        {
            return data.ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion
    }

    public static class StringHelpers
    {
        #region 字符串相关
        public static string ToNumericStrFormatted(this string str)
        {
            return str.TryToDouble(3).TryToFloat(3);
        }

        public static string TryToDouble(this string str, int precision)
        {
            double result;
            if (double.TryParse(str, out result))
            {
                return result.ToString("F" + precision);
            }
            return str;
        }

        public static string TryToFloat(this string str, int precision)
        {
            float result;
            if (float.TryParse(str, out result))
            {
                return result.ToString("F" + precision);
            }
            return str;
        }

        public static double ToDouble(this string str)
        {
            return Convert.ToDouble(str);
        }

        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static float ToFloat(this string str)
        {
            return Convert.ToSingle(str);
        }

        /// <summary>
        /// 将一个字符串转换为字节数组
        /// </summary>
        /// <param name="text">待转换的字符串</param>
        /// <returns>字节数组，其中包含输入字符串的编码数据</returns>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// 将一个字节数组转换为字符串
        /// </summary>
        /// <param name="bytes">待转换的字节数组</param>
        /// <returns>字符串，其中包含输入字节数组的编码数据</returns>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion
    }

    public static class NumericHelpers
    {
        #region 数值格式化
        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double ToIntUp(this double d)
        {
            return Math.Ceiling(d);
        }

        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double ToIntDown(this double d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float ToIntUp(this float d)
        {
            return Convert.ToSingle(Math.Ceiling(d));
        }

        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float ToIntDown(this float d)
        {
            return Convert.ToSingle(Math.Floor(d));
        }

        public static int ToInt(this double d)
        {
            return Convert.ToInt32(d);
        }

        public static int ToInt(this float d)
        {
            return Convert.ToInt32(d);
        }

        #endregion

        #region 数值平均、中值等

        public static double Mean(this IEnumerable<double> ds)
        {
            return ds.Sum() / ds.Count();
        }

        public static float Mean(this IEnumerable<float> ds)
        {
            return ds.Sum() / ds.Count();
        }

        public static int Mean(this IEnumerable<int> ds)
        {
            return ds.Sum() / ds.Count();
        }

        public static double Median(this IEnumerable<double> ds)
        {
            if (ds.Count() == 1 || ds.Count() == 2)
            {
                return ds.First();
            }
            return ds.OrderBy(x => x).ElementAt(ds.Count() / 2);
        }

        public static float Median(this IEnumerable<float> ds)
        {
            if (ds.Count() == 1 || ds.Count() == 2)
            {
                return ds.First();
            }
            return ds.OrderBy(x => x).ElementAt(ds.Count() / 2);
        }

        public static int Median(this IEnumerable<int> ds)
        {
            if (ds.Count() == 1 || ds.Count() == 2)
            {
                return ds.First();
            }
            return ds.OrderBy(x => x).ElementAt(ds.Count() / 2);
        }
        #endregion

        /// <summary>
        /// 对数值<paramref name="t"/>进行四舍五入，仅对<see cref="double"/>和<see cref="float"/>类型起作用
        /// </summary>
        /// <param name="t"></param>
        /// <param name="precisionNumber">要舍弃的小数点位数</param>
        /// <returns></returns>
        public static double ToPrecision(this double t, int precisionNumber = 1)
        {
            return Math.Round(t, precisionNumber);
        }

        /// <summary>
        /// 对数值<paramref name="t"/>进行四舍五入，仅对<see cref="double"/>和<see cref="float"/>类型起作用
        /// </summary>
        /// <param name="t"></param>
        /// <param name="precisionNumber">要舍弃的小数点位数</param>
        /// <returns></returns>
        public static float ToPrecision(this float t, int precisionNumber = 1)
        {
            return Convert.ToSingle(Math.Round(t, precisionNumber));
        }
    }
}
