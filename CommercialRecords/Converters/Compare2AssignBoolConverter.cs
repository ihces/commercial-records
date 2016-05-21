using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class Compare2AssignBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int compareOp = 0;

            if (language.Length > 1)
            {
                if ('<' == language[0])
                    compareOp = -1;
                else if ('>' == language[0])
                    compareOp = 1;
            }

            if (value == null && parameter == null)
                return true;

            if ((value == null || parameter == null) || !value.GetType().Equals(parameter.GetType()))
                return false;


            Double value1 = 0.0, value2 = 0.0;

            if (value is DateTime)
            {
                value1 = ((DateTime)value).Ticks;
                value2 = ((DateTime)parameter).Ticks;
            }

            if (compareOp == value1.CompareTo(value2))
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
