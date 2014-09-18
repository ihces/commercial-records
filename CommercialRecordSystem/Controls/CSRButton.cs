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
        #endregion

        
    }
}
