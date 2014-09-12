using System;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class AmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            return String.Format("{0:#,#.###}", (double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {

            return 1;
        }
    }
}
