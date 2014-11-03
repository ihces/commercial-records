using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
namespace CommercialRecordSystem.Converters
{
    class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }
    }
}
