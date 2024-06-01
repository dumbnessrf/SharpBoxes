using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpBoxes.Validation
{
    /// <summary>
    /// <para>ValidationHelper是一个静态类，提供了一系列的验证方法。</para>
    /// <para>这些方法可以用于验证参数的值是否满足特定的条件，例如是否为null，是否在指定的范围内，是否满足特定的格式等。</para>
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// <para>如果条件为false，则抛出<see cref="ArgumentException"/>异常。</para>
        /// <para>这个方法主要用于在方法的开始处检查参数的值是否满足预期的条件。</para>
        /// </summary>
        [DebuggerHidden]
        public static void Assert(
            bool condition,
            string message,
            [CallerArgumentExpression("condition")] string conditionExpression = null
        )
        {
            if (!condition)
                throw new ArgumentException(message: message, paramName: conditionExpression);
        }

        /// <summary>
        /// <para>检查参数的值是否小于指定的限制。</para>
        /// <para>如果参数的值不小于限制，则抛出<see cref="ArgumentOutOfRangeException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void MustLessThan<T>(
            T argument,
            T limit,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("limit")] string limitExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            var isLess = argument.CompareTo(limit) < 0;
            if (!isLess)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: argumentExpression,
                    message: $"{argumentExpression} ({argument}) must be less than {limitExpression} ({limit})."
                );
            }
        }

        /// <summary>
        /// <para>检查参数的值是否小于指定的限制，并返回一个消息。</para>
        /// <para>如果参数的值不小于限制，则返回false和一个描述错误的消息。</para>
        /// </summary>
        [DebuggerHidden]
        public static bool MustLessThan<T>(
            T argument,
            T limit,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("limit")] string limitExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            message = "Validate OK";
            var isLess = argument.CompareTo(limit) < 0;
            if (isLess is false)
            {
                message =
                    $"{argumentExpression} ({argument}) must be less than {limitExpression} ({limit}).";
            }
            return isLess;
        }

        /// <summary>
        /// <para>检查参数的值是否大于指定的限制。</para>
        /// <para>如果参数的值不大于限制，则抛出<see cref="ArgumentOutOfRangeException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void MustMoreThan<T>(
            T argument,
            T limit,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("limit")] string limitExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            var isMore = argument.CompareTo(limit) > 0;
            if (!isMore)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: argumentExpression,
                    message: $"{argumentExpression} ({argument}) must be more than {limitExpression} ({limit})."
                );
            }
        }

        /// <summary>
        /// <para>检查参数的值是否大于指定的限制，并返回一个消息。</para>
        /// <para>如果参数的值不大于限制，则返回false和一个描述错误的消息。</para>
        /// </summary>
        [DebuggerHidden]
        public static bool MustMoreThan<T>(
            T argument,
            T limit,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("limit")] string limitExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            message = "Validate OK";
            var isMore = argument.CompareTo(limit) > 0;
            if (isMore is false)
            {
                message =
                    $"{argumentExpression} ({argument}) must be more than {limitExpression} ({limit}).";
            }
            return isMore;
        }

        /// <summary>
        /// <para>检查参数的值是否在指定的范围内。</para>
        /// <para>如果参数的值不在指定的范围内，则抛出<see cref="ArgumentOutOfRangeException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void InRange<T>(
            T argument,
            T low,
            T high,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("low")] string lowExpression = null,
            [CallerArgumentExpression("high")] string highExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            var isLow = argument.CompareTo(low) < 0;
            if (isLow)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: argumentExpression,
                    message: $"{argumentExpression} ({argument}) cannot be less than {lowExpression} ({low})."
                );
            }
            var isHigh = argument.CompareTo(high) > 0;
            if (isHigh)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: argumentExpression,
                    message: $"{argumentExpression} ({argument}) cannot be greater than {highExpression} ({high})."
                );
            }
        }

        /// <summary>
        /// <para>检查参数的值是否在指定的范围内，并返回一个消息。</para>
        /// <para>如果参数的值不在指定的范围内，则返回false和一个描述错误的消息。</para>
        /// </summary>
        public static bool InRange<T>(
            T argument,
            T low,
            T high,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null,
            [CallerArgumentExpression("low")] string lowExpression = null,
            [CallerArgumentExpression("high")] string highExpression = null
        )
            where T : struct, IComparable<T>, IComparable
        {
            message = "Validate OK";
            var isLow = argument.CompareTo(low) < 0;

            if (isLow)
            {
                message =
                    $"{argumentExpression} ({argument}) cannot be less than {lowExpression} ({low}).";
                return false;
            }
            var isHigh = argument.CompareTo(high) > 0;
            if (isHigh)
            {
                message =
                    $"{argumentExpression} ({argument}) cannot be greater than {highExpression} ({high}).";
            }

            return !isHigh;
        }

        /// <summary>
        /// <para>检查参数是否为null。</para>
        /// <para>如果参数为null，则抛出<see cref="ArgumentNullException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void ThrowIfNull<T>(
            T argument,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
            where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(paramName: argumentExpression);
        }

        /// <summary>
        /// <para>检查参数是否为null，并返回一个消息。</para>
        /// <para>如果参数为null，则返回true和一个描述错误的消息。</para>
        /// </summary>
        [DebuggerHidden]
        public static bool IsNull<T>(
            T argument,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
            where T : class
        {
            message = $"{argumentExpression} is null!";
            return argument == null;
        }

        /// <summary>
        /// <para>检查数组的长度是否为0。</para>
        /// <para>如果数组的长度为0，则抛出<see cref="ArgumentException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void ArrayLengthNotEqualZero<T>(
            T[] argument,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
        {
            if (argument.Length == 0)
                throw new ArgumentException(
                    message: $"the array {argumentExpression} length == 0!",
                    paramName: argumentExpression
                );
        }

        /// <summary>
        /// <para>检查数组的长度是否为0，并返回一个消息。</para>
        /// <para>如果数组的长度为0，则返回false和一个描述错误的消息。</para>
        /// </summary>
        [DebuggerHidden]
        public static bool ArrayLengthNotEqualZero<T>(
            T[] argument,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
        {
            message = "Validate OK";

            bool v = argument.Length != 0;
            if (v is false)
            {
                message = $"the array {argumentExpression} length == 0!";
            }
            return v;
        }

        /// <summary>
        /// <para>检查集合的元素数量是否为0。</para>
        /// <para>如果集合的元素数量为0，则抛出<see cref="ArgumentException"/>异常。</para>
        /// </summary>
        [DebuggerHidden]
        public static void CollectionCountNotEqualZero<T>(
            ICollection<T> argument,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
        {
            if (argument.Count == 0)
                throw new ArgumentException(
                    message: $"the collection {argumentExpression} count == 0!",
                    paramName: argumentExpression
                );
        }

        /// <summary>
        /// <para>检查集合的元素数量是否为0，并返回一个消息。</para>
        /// <para>如果集合的元素数量为0，则返回false和一个描述错误的消息。</para>
        /// </summary>
        [DebuggerHidden]
        public static bool CollectionCountNotEqualZero<T>(
            ICollection<T> argument,
            out string message,
            [CallerArgumentExpression("argument")] string argumentExpression = null
        )
        {
            message = "Validate OK";

            bool v = argument.Count != 0;
            if (v is false)
            {
                message = $"the collection {argumentExpression} count == 0!";
            }
            return v;
        }
    }
}
