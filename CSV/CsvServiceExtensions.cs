using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpBoxes.CsvServices
{
    public static class CsvServiceExtensions
    {
        public static IEnumerable<CsvDataBase> Append<T>(this CsvDataBase data, T t)
            where T : CsvDataBase
        {
            return new List<CsvDataBase>() { data, t };
        }

        public static IEnumerable<CsvDataBase> Append<T>(this IEnumerable<CsvDataBase> datas, T t)
            where T : CsvDataBase
        {
            return datas.Append(t);
        }

        #region Blank

        public static IEnumerable<CsvDataBase> AddBlank(this CsvDataBase data)
        {
            return data.Append(new CsvDataBlank());
        }

        public static IEnumerable<CsvDataBase> AddBlank(this IEnumerable<CsvDataBase> data)
        {
            return data.Append(new CsvDataBlank());
        }

        #endregion

        #region Custom

        public static IEnumerable<CsvDataBase> AddCustom(this CsvDataBase data, string datas)
        {
            return data.Append(new CsvDataCustom(datas));
        }

        public static IEnumerable<CsvDataBase> AddCustom(this CsvDataBase data, List<string> datas)
        {
            return data.Append(new CsvDataCustom(datas));
        }

        public static IEnumerable<CsvDataBase> AddCustom(
            this IEnumerable<CsvDataBase> data,
            params string[] datas
        )
        {
            return data.Append(new CsvDataCustom(datas));
        }

        public static IEnumerable<CsvDataBase> AddCustom(
            this IEnumerable<CsvDataBase> data,
            string datas
        )
        {
            return data.Append(new CsvDataCustom(datas));
        }

        public static IEnumerable<CsvDataBase> AddCustom(
            this IEnumerable<CsvDataBase> data,
            List<string> datas
        )
        {
            return data.Append(new CsvDataCustom(datas));
        }

        public static IEnumerable<CsvDataBase> AddCustom(
            this CsvDataBase data,
            params string[] datas
        )
        {
            return data.Append(new CsvDataCustom(datas));
        }

        #endregion

        #region Normal

        public static IEnumerable<CsvDataBase> AddNormal<T>(this CsvDataBase data, List<T> datas)
            where T : class
        {
            return data.Append(new CsvDataNormal<T>(datas));
        }

        public static IEnumerable<CsvDataBase> AddNormal<T>(this CsvDataBase data, T t)
            where T : class
        {
            return data.Append(new CsvDataNormal<T>(t));
        }

        public static IEnumerable<CsvDataBase> AddNormal<T>(
            this IEnumerable<CsvDataBase> data,
            List<T> datas
        )
            where T : class
        {
            return data.Append(new CsvDataNormal<T>(datas));
        }

        public static IEnumerable<CsvDataBase> AddNormal<T>(this IEnumerable<CsvDataBase> data, T t)
            where T : class
        {
            return data.Append(new CsvDataNormal<T>(t));
        }

        #endregion
    }
}
