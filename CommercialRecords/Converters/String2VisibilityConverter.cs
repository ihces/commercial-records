using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class String2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (null == value || string.IsNullOrWhiteSpace(value.ToString()))
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (string)value;
        }
    }
}
