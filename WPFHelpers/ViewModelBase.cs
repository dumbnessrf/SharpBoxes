using SharpBoxes.Dlls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SharpBoxes.WPFHelpers
{
    /// <summary>
    /// 用于实现MVVM模式的基类
    /// <para></para>
    /// <example>
    /// <code>
    /// internal class MainViewModel : SharpBoxes.ViewModelBase
    /// {
    ///     private string name;
    ///     public string Name
    ///     {
    ///         get { return name; }
    ///         set
    ///         {
    ///             name = value;
    ///             OnPropertyChanged();
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        PropertyChangedEventHandler _propertyChanged;

        /// <summary>
        /// 在属性值更改后发生
        /// </summary>
        public event EventHandler<PropertyUpdateEventArgs> AfterPropertyUpdated;
        /// <summary>
        /// 在属性值更改前发生
        /// </summary>
        public event EventHandler<PropertyUpdateEventArgs> BeforePropertyUpdated;

        /// <summary>
        /// 用于验证属性改变的委托，返回<c>false</c>则不会触发属性改变事件，返回<c>true</c>则会触发属性改变事件
        /// </summary>
        protected Func<PropertyUpdateEventArgs, bool> OnValidate;

        //public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _propertyChanged += value;
            remove => _propertyChanged -= value;
        }
        /// <summary>
        /// 属性改变前事件
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">属性旧值</param>
        /// <param name="newValue">属性将要更改的值</param>
        protected void OnPropertyChanging(
            object oldValue = null,
            object newValue = null,
            [CallerMemberName] string propertyName = null
        )
        {
            if (
                OnValidate != null
                && _validationProperties.Contains(propertyName)
                && _isValidationEnabled
            )
            {
                var validateOk = OnValidate?.Invoke(
                    new PropertyUpdateEventArgs(propertyName, oldValue, newValue)
                );
                if (validateOk is false)
                {
                    return;
                }
                else
                {
                    SetErrors(propertyName, []);


                }
            }
            BeforePropertyUpdated?.Invoke(
                this,
                new PropertyUpdateEventArgs(propertyName, oldValue, newValue)
            );
        }

        /// <summary>
        /// 属性改变事件
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">属性旧值</param>
        /// <param name="newValue">属性将要更改的值</param>
        protected void OnPropertyChanged(
            object oldValue = null,
            object newValue = null,
            [CallerMemberName] string propertyName = null
        )
        {

            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            AfterPropertyUpdated?.Invoke(
                this,
                new PropertyUpdateEventArgs(propertyName, oldValue, newValue)
            );
        }
        /// <summary>
        /// 自动更新属性值，并且唤起事件<see cref="BeforePropertyUpdated"/>和<see cref="AfterPropertyUpdated"/>，倘若启用了Validation，内部也会自动调用<see cref="OnValidate"/>
        /// </summary>
        /// <para></para>
        /// <example>
        /// <code>
        /// public string Input
        /// {
        ///     get { return _input; }
        ///     set
        ///     {
        ///         SetProperty(ref _input, value);
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="prop">属性变量</param>
        /// <param name="newValue">属性值</param>
        /// <param name="propertyName">属性名称，通过<c>CallerMemberName</c>自动传入</param>
        protected void SetProperty<T>(ref T prop,T newValue, [CallerMemberName] string propertyName = null)
        {
            OnPropertyChanging(prop, newValue, propertyName);
            prop = newValue;
            OnPropertyChanged(prop, newValue, propertyName);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose() { }

        #region Validation
        private bool _isValidationEnabled = false;
        private List<string> _validationProperties = new();
        
        /// <summary>
        /// 启用验证,在构造函数中调用<see cref="EnableValidation{T}"/>来启用验证
        /// <remarks>在析构函数中需要调用<see cref="DisableValidation"/>来关闭验证，销毁验证资源</remarks>
        /// <example>
        /// <code>
        /// EnableValidation&lt;InputDialogViewModel&gt;();
        /// </code>
        /// </example>
        /// </summary>
        public void EnableValidation<T>()
            where T : class
        {
            ErrorsChanged += this.ViewModelBase_ErrorsChanged;
            _isValidationEnabled = true;
            _validationProperties = LibLoadHelper
                .FindSpecifiedPropertyExceptAttributeFromType(
                    typeof(T),
                    typeof(DoNotValidateAttribute)
                )
                .Select(s => s.Name)
                .ToList();
        }

        private void ViewModelBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var propName = e.PropertyName;
        }
        /// <summary>
        /// 在析构函数中需要调用<see cref="DisableValidation"/>来关闭验证，销毁验证资源
        /// </summary>
        public void DisableValidation()
        {
            _isValidationEnabled = false;
            _validationProperties.Clear();
            ErrorsChanged -= this.ViewModelBase_ErrorsChanged;
        }

        /// <summary>
        /// 指向错误信息的集合，如果有错误则返回true
        /// </summary>
        [Browsable(false)]
        public bool HasErrors => errors.Count > 0;

        /// <summary>
        /// 每当Errors集合发生变化时，激活ErrorsChanged事件
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// 存储错误信息
        /// </summary>
        private readonly Dictionary<string, List<string>> errors = new();

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName) is false)
            {
                return null;
            }
            return errors[propertyName];
        }

        private List<string> errorCollections;

        /// <summary>
        /// 获取错误信息的集合
        /// </summary>
        [DoNotValidate]
        [Browsable(false)]
        public List<string> ErrorCollections
        {
            get { return errorCollections; }
            set
            {
                errorCollections = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 获取第一个错误信息
        /// </summary>
        [Browsable(false)]
        public string FirstError => errors?.FirstOrDefault().Value?.FirstOrDefault();

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="_errors">错误信息</param>
        protected void SetErrors(string name, List<string> _errors)
        {
            if (_errors.Count == 0)
            {
                errors.Remove(name);
            }
            else
            {
                if (errors.ContainsKey(name))
                {
                    errors[name] = _errors;
                }
                else
                {
                    errors.Add(name, _errors);
                }
            }

            RaiseErrorsChanged(name);
        }

        /// <summary>
        /// 激活ErrorsChanged事件
        /// </summary>
        /// <param name="name"></param>
        private void RaiseErrorsChanged(string name)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(name));
            }
            ErrorCollections = errors.SelectMany(s => s.Value).ToList();
        }

        #endregion
    }

    /// <summary>
    /// 标记了该特性的属性不会被验证
    /// <example>
    /// <code>
    /// [DoNotValidate]
    /// public List&lt;string&gt; ErrorCollections
    /// {
    ///     get { return errorCollections; }
    ///     set
    ///     {
    ///         errorCollections = value;
    ///         OnPropertyChanged();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotValidateAttribute : Attribute { }
    
    /// <summary>
    /// 属性更改事件参数
    /// </summary>
    [DebuggerStepThrough]
    public sealed class PropertyUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// 解构
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">属性旧值</param>
        /// <param name="newValue">属性将要更改的值</param>
        public void Deconstruct(out string propertyName, out object oldValue, out object newValue)
        {
            propertyName = PropertyName;
            oldValue = OldValue;
            newValue = NewValue;
        }

        /// <summary>
        /// 属性更改事件参数
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">属性旧值</param>
        /// <param name="newValue">属性将要更改的值</param>
        [DebuggerStepThrough]
        public PropertyUpdateEventArgs(
            string propertyName,
            object oldValue = null,
            object newValue = null
        )
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
/// <summary>
/// 属性名称
/// </summary>
        public string PropertyName { get; set; }
/// <summary>
/// 属性旧值
/// </summary>
        public object OldValue { get; set; }
/// <summary>
/// 属性将要更改的值
/// </summary>
        public object NewValue { get; set; }
    }
}
