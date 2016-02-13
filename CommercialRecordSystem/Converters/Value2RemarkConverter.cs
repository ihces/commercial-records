using CommercialRecordSystem.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CommercialRecordSystem.Converters
{
    class Value2RemarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string remarkKeyBuff = parameter.ToString();
            if (remarkKeyBuff.Length > 0 && remarkKeyBuff[0] == '#')
            {
                string[] remarkTokens = remarkKeyBuff.Substring(1).Split(new char[] { '|' });

                if (remarkTokens.Length == 2)
                {
                    return CrsDictionary.getInstance().lookup(remarkTokens[0], remarkTokens[1]);
                }
            }

            return CrsDictionary.getInstance().lookup(parameter.ToString(), value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
