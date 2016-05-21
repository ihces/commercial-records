using CommercialRecords.Common;
using System;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Converters
{
    class Value2RemarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string remark = CrsDictionary.getInstance().lookup2(parameter.ToString());
            
            return remark.Length > 0 ? remark : CrsDictionary.getInstance().lookup(parameter.ToString(), value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
