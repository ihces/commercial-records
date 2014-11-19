using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Double2MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format("{0:c}", (double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double returnVal = 0; 
            double.TryParse((string)value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); 
            return returnVal;
        }
    }
}
