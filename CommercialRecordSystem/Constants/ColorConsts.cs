using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CommercialRecordSystem.Constants
{
    class ColorConsts
    {
        public static readonly Brush TEXTBOX_BACKGROUND_VALID = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));

        public static readonly Brush TEXTBOX_BACKGROUND_INVALID = new SolidColorBrush(Color.FromArgb(0x88, 0xad, 0x10, 0x3c));

        public static readonly Brush TEXTBOX_NOT_EMPTY_FOREGROUND = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
    }
}
