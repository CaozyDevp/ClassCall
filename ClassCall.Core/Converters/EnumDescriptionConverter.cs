using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ClassCall.Core.Extensions;

namespace ClassCall.Core.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        /// <summary>
        /// 获取枚举类型的Description
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return EnumExtension.GetDescription(value);
        }

        /// <summary>
        /// 该方法尚未实现
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
