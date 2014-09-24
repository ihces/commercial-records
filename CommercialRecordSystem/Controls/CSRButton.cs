using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Controls
{
    class CSRButton : Button
    {
        #region Properties
        #region Validation
        public bool Validation
        {
            get
            {
                return (bool)GetValue(ValidationProperty);
            }
            set
            {
                SetValue(ValidationProperty, value);
            }
        }

        public static readonly DependencyProperty ValidationProperty =
            DependencyProperty.Register(
                "Validation",
                typeof(bool),
                typeof(CSRButton),
                new PropertyMetadata(false)
            );
        #endregion

        #region Disabled
        public bool Disabled
        {
            get
            {
                return (bool)GetValue(DisabledProperty);
            }
            set
            {
                SetValue(DisabledProperty, value);
            }
        }

        public static readonly DependencyProperty DisabledProperty =
            DependencyProperty.Register(
                "Disabled",
                typeof(bool),
                typeof(CSRButton),
                new PropertyMetadata(false, DisabledChangedHandler)
            );
        #endregion
        #endregion

        private static void DisabledChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Button button = (Button)obj;
            button.IsEnabled = !(bool)e.NewValue;
        }
    }
}
