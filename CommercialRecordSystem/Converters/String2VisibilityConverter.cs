using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class String2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string strBuff = (string)value;
            Visibility visibility = Visibility.Visible;

            if (string.IsNullOrWhiteSpace(strBuff))
                visibility = Visibility.Collapsed;
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (string)value;
        }
    }
}
