using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SharpBoxes.Validation;

namespace SharpBoxes.WPFHelpers.Dialogs
{
    public record NumericRange(double Min, double Max)
    {
        public bool InRange(double value) => value >= Min && value <= Max;
        public bool IsValid => Min <= Max &&!(Min==0 && Max==0);
    }
    public class InputDialogViewModel : ViewModelBase
    {
        //private string _stringValue;
        //private double _doubleValue;
        //private float _floatValue;
        private string _caption;
        private string _description;
        //private int _intValue;

        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                SetProperty(ref _input, value);
            }
        }

        public NumericRange _NumericRange { get; set; } = new NumericRange(0, 0);
        public object Value => InputType switch
        {
            EInputType.String => Input,
            EInputType.Float =>float.Parse(Input),
            EInputType.Double => double.Parse(Input),
            EInputType.Int => int.Parse(Input),
            _ =>Input
        };

        public EInputType InputType { get; set; } = EInputType.String;
        
        /*
         public int IntValue
         {
             get { return _intValue; }
             set
             {
                 OnPropertyChanging(_intValue, value);
                 _intValue = value;
                 OnPropertyChanged();
             }
         }

         public float FloatValue
         {
             get { return _floatValue; }
             set
             {
                 OnPropertyChanging(_intValue, value);
                 _floatValue = value;
                 OnPropertyChanged();
             }
         }
         public double DoubleValue
         {
             get { return _doubleValue; }
             set
             {
                 OnPropertyChanging(_intValue, value);
                 _doubleValue = value;
                 OnPropertyChanged();
             }
         }
         public string StringValue
         {
             get { return _stringValue; }
             set
             {
                 OnPropertyChanging(_intValue, value);
                 _stringValue = value;
                 OnPropertyChanged();
             }
         }*/

        public InputDialogViewModel()
        {
            OnValidate = s =>
            {
                bool res=true;
   
                switch (InputType)
                {
                    case EInputType.String:
                        return true;
                    case EInputType.Float:
                         res = float.TryParse(s.NewValue.ToString(), out var value1);
                        if (res is false)
                        {
                            SetErrors(s.PropertyName, ["输入格式不正确，无法正确解析为float类型数据格式"]);
                        }
                        else
                        {
                            if (_NumericRange.IsValid)
                            {
                                res = ValidationHelper.InRange(value1, _NumericRange.Min, _NumericRange.Max, out var message);
                                if (res is false)
                                    SetErrors(s.PropertyName, [message]);
                            }
                        }
                         return res;
                    case EInputType.Double:
                         res= double.TryParse(s.NewValue.ToString(), out var value2);
                        if (res is false)
                        {
                            SetErrors(s.PropertyName, ["输入格式不正确，无法正确解析为double类型数据格式"]);
                        }
                        else
                        {
                            if (_NumericRange.IsValid)
                            {
                                res = ValidationHelper.InRange(value2, _NumericRange.Min, _NumericRange.Max, out var message);
                                if (res is false)
                                    SetErrors(s.PropertyName, [message]);
                            }
                        }
                         break;
                    case EInputType.Int:
                         res= int.TryParse(s.NewValue.ToString(), out var value3);
                        if (res is false)
                        {
                            SetErrors(s.PropertyName, ["输入格式不正确，无法正确解析为int类型数据格式"]);
                        }
                        else
                        {
                        if (_NumericRange.IsValid)
                         {
                             res = ValidationHelper.InRange(value3, _NumericRange.Min, _NumericRange.Max, out var message);
                             if (res is false)
                                 SetErrors(s.PropertyName, [message]);
                            }
                        }
                        break;
                  
                }

             
                return res;
            };
            EnableValidation<InputDialogViewModel>();
        }

        public override void Dispose()
        {
            DisableValidation();
        }
    }
}
