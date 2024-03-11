using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpBoxes.Reflection;

/// <summary>
/// 提供事件的辅助方法
/// </summary>
public static class EventHelper
{
    private static Dictionary<Type, List<FieldInfo>> _classEventFieldsCache =
        new Dictionary<Type, List<FieldInfo>>();

    /// <summary>
    /// 获取指定实例中指定事件的所有处理程序的调用方法。
    /// <list type="number">
    /// <item>获取实例的所有字段，包括非公开的和实例的字段。</item>
    /// <item>从这些字段中筛选出类型为指定处理程序类型的字段。</item>
    /// <item>对于每个筛选出的字段，如果字段的名称包含指定的事件名称，那么获取该字段的值。</item>
    /// <item>如果字段的值不为空，那么获取该值的类型的"Invoke"方法。</item>
    /// <item>如果"Invoke"方法存在，那么将该方法和字段的值作为一个元组添加到结果列表中。</item>
    /// </list>
    /// <example>
    /// <strong>以下代码演示了如何使用<see cref="GetEventHandlerRaiseMethods{TInstance}(TInstance, string, Type)"/>方法。</strong>
    /// <code>
    /// public interface INotifyValueChanged
    /// {
    ///     event EventHandler&lt;NotifyValueChangedEventArgs&gt; OnNotifyValueChanged;
    /// }
    /// var invokeMethod = instance.GetEventHandlerRaiseMethods("OnNotifyValueChanged", typeof(EventHandler&lt;NotifyValueChangedEventArgs&gt;)).FirstOrDefault();
    ///
    /// //invokeMethod.Invoke(this, new object[] { null, myEventArgs });
    /// invokeMethod.Method?.Invoke(
    /// invokeMethod.FieldValue,
    /// [
    ///     null,
    ///     new NotifyValueChangedEventArgs()
    ///  ]
    /// );
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="TInstance">实例的类型。</typeparam>
    /// <param name="instance">要获取事件处理程序的实例。</param>
    /// <param name="eventName">要获取的事件的名称。</param>
    /// <param name="handlerType">事件处理程序的类型。</param>
    /// <returns>包含调用方法和字段值的元组的列表。</returns>
    public static List<(
        MethodInfo Method,
        object FieldValue
    )> GetEventHandlerRaiseMethods<TInstance>(
        this TInstance instance,
        string eventName,
        Type handlerType
    )
    {
        List<FieldInfo> eventFields = new();
        if (
            _classEventFieldsCache.ContainsKey(typeof(TInstance))
            && _classEventFieldsCache[typeof(TInstance)].Count != 0
        )
        {
            eventFields = _classEventFieldsCache[typeof(TInstance)];
        }
        else
        {
            eventFields = typeof(TInstance)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .ToList();
            _classEventFieldsCache[typeof(TInstance)] = eventFields.ToList();
        }

        var listOfEvent = eventFields.Where(fi => fi.FieldType == handlerType).ToList();

        var listOfInvoke = new List<(MethodInfo Method, object FieldValue)>();
        foreach (var fi in listOfEvent)
        {
            if (!fi.Name.Contains(eventName))
                continue;
            var tempV = fi.GetValue(instance);
            if (tempV is null)
            {
                continue;
            }
            var invokeMethod = tempV.GetType().GetMethod("Invoke");
            if (invokeMethod != null)
            {
                listOfInvoke.Add((invokeMethod, tempV));
            }
        }
        return listOfInvoke;
    }
}
