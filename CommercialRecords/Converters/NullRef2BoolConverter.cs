using System;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class NullRef2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (null == value)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}