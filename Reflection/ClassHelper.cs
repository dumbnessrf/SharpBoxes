using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharpBoxes.Reflection;

/// <summary>
/// 提供类的辅助方法，包括设置属性的 Display Name 和 Description...
/// </summary>
public static class ClassHelper
{
    /// <summary>
    /// 设置指定类型中指定属性的显示名称。
    /// </summary>
    /// <typeparam name="T">属性所属的类型。</typeparam>
    /// <param name="propertyName">要设置显示名称的属性的名称。</param>
    /// <param name="newDisplayName">要设置的新显示名称。</param>
    public static void SetDisplayName<T>(string propertyName, string newDisplayName)
    {
        // 获取指定类型中指定属性的 PropertyDescriptor
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(T))[propertyName];

        // 获取属性的 DisplayNameAttribute
        DisplayNameAttribute attribute =
            descriptor.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

        // 获取 DisplayNameAttribute 的私有字段 "_displayName"
        FieldInfo field = attribute
            ?.GetType()
            .GetField("_displayName", BindingFlags.NonPublic | BindingFlags.Instance);

        // 将 "_displayName" 字段的值设置为新的显示名称
        field?.SetValue(attribute, newDisplayName);
    }

    /// <summary>
    /// 设置指定类型中指定属性的描述。
    /// </summary>
    /// <typeparam name="T">属性所属的类型。</typeparam>
    /// <param name="propertyName">要设置描述的属性的名称。</param>
    /// <param name="newDesc">要设置的新描述。</param>
    public static void SetDescription<T>(string propertyName, string newDesc)
    {
        // 获取指定类型中指定属性的 PropertyDescriptor
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(T))[propertyName];

        // 获取属性的 DescriptionAttribute
        DescriptionAttribute attribute =
            descriptor.Attributes[typeof(DescriptionAttribute)] as DescriptionAttribute;

        // 获取 DescriptionAttribute 的私有字段 "description"
        FieldInfo field = attribute
            ?.GetType()
            .GetField("description", BindingFlags.NonPublic | BindingFlags.Instance);

        // 将 "description" 字段的值设置为新的描述
        field?.SetValue(attribute, newDesc);
    }

    /// <summary>
    /// 设置指定类型中指定属性的类别。
    /// </summary>
    /// <typeparam name="T">属性所属的类型。</typeparam>
    /// <param name="propertyName">要设置类别的属性的名称。</param>
    /// <param name="newCate">要设置的新类别。</param>
    public static void SetCategory<T>(string propertyName, string newCate)
    {
        // 获取指定类型中指定属性的 PropertyDescriptor
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(typeof(T))[propertyName];

        // 获取属性的 CategoryAttribute
        CategoryAttribute attribute =
            descriptor.Attributes[typeof(CategoryAttribute)] as CategoryAttribute;

        // 获取 CategoryAttribute 的私有字段 "categoryValue"
        FieldInfo field = attribute
            ?.GetType()
            .GetField("categoryValue", BindingFlags.NonPublic | BindingFlags.Instance);

        // 将 "categoryValue" 字段的值设置为新的类别
        field?.SetValue(attribute, newCate);
    }

    /// <summary>
    /// 获取指定实例的指定字段的值。
    /// </summary>
    /// <typeparam name="TInstance">实例的类型。</typeparam>
    /// <typeparam name="TResult">字段的值的类型。</typeparam>
    /// <param name="t">要获取字段值的实例。</param>
    /// <param name="name">要获取的字段的名称。</param>
    /// <returns>字段的值，如果字段存在并且可以转换为指定类型，则返回字段的值；否则，返回默认值。</returns>
    /// <exception cref="ArgumentNullException">当<paramref name="t"/>为null时引发。</exception>
    /// <exception cref="ArgumentException">当指定的字段在类型中不存在时引发。</exception>
    public static TResult GetFieldValue<TInstance, TResult>(this TInstance t, string name)
    {
        if (t == null)
        {
            throw new ArgumentNullException(nameof(t));
        }

        var field = typeof(TInstance).GetField(
            name,
            BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.NonPublic
        );
        if (field == null)
        {
            throw new ArgumentException(
                $"字段'{name}'在类型'{typeof(TInstance).FullName}'中找不到。",
                nameof(name)
            );
        }

        var value = field.GetValue(t);
        if (value is TResult result)
        {
            return result;
        }

        return default;
    }

    /// <summary>
    /// 获取指定实例的指定属性的值。
    /// </summary>
    /// <typeparam name="TInstance">实例的类型。</typeparam>
    /// <typeparam name="TResult">属性值的类型。</typeparam>
    /// <param name="t">要获取属性值的实例。</param>
    /// <param name="name">要获取的属性的名称。</param>
    /// <returns>指定属性的值，如果属性不存在或值无法转换为指定类型，则返回默认值。</returns>
    /// <exception cref="ArgumentNullException">当<paramref name="t"/>为null时引发。</exception>
    /// <exception cref="ArgumentException">当指定的属性在类型<typeparamref name="TInstance"/>中找不到时引发。</exception>
    public static TResult GetPropertyValue<TInstance, TResult>(this TInstance t, string name)
    {
        if (t == null)
        {
            throw new ArgumentNullException(nameof(t));
        }

        var property = typeof(TInstance).GetProperty(
            name,
            BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.NonPublic
        );
        if (property == null)
        {
            throw new ArgumentException(
                $"属性'{name}'在类型'{typeof(TInstance).FullName}'中找不到。",
                nameof(name)
            );
        }

        var value = property.GetValue(t);
        if (value is TResult result)
        {
            return result;
        }

        return default;
    }
}
