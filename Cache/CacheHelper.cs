using System;
using System.Collections;
using System.Collections.Concurrent;

namespace SharpBoxes.Cache
{
    /// <summary>
    /// WPF的<see cref="System.Windows.Media"/>及其子类如<see cref="System.Windows.Media.Imaging.BitmapSource"/>等对象在使用完毕后，由于没有实现<see cref="IDisposable"/>接口，具体待定
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> Caches = new();

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCache(string key, object value)
        {
            if (Caches.ContainsKey(key))
            {
                Caches[key] = value;
            }
            else
            {
                Caches.TryAdd(key, value);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存的值</returns>
        public static T GetCache<T>(string key)
        {
            var res = Caches.TryGetValue(key, out var value);
            if (res)
            {
                return (T)value;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        public static void RemoveCache(string key)
        {
            if (!Caches.ContainsKey(key))
                return;
            //检查值是否可以清除，例如：IDictionary,IList,Array等
            if (Caches[key] is IList)
            {
                (Caches[key] as IList)?.Clear();
            }
            if (Caches[key] is IDictionary)
            {
                (Caches[key] as IDictionary)?.Clear();
            }
            if (Caches[key] is Array)
            {
                Array array = Caches[key] as Array;

                for (int i = 0; i < array!.Length; i++)
                {
                    array.SetValue(null, i);
                }
            }
            //检查值是否可以被释放
            if (Caches[key] is IDisposable)
            {
                (Caches[key] as IDisposable)?.Dispose();
            }

            Caches[key] = null;
            Caches.TryRemove(key, out _);
        }
    }
}
