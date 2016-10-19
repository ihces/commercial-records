using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CommercialRecords.Common;
using Windows.Foundation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CommercialRecords.Controls
{
    public sealed partial class CrsUserAuthentication : UserControl
    {
        public CrsUserAuthentication()
        {
            this.InitializeComponent();
            this.DataContext = new CrsAuthentication.UserAuthControlVM(this);
        }

        #region IsOpen
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                "IsOpen",
                typeof(bool),
                typeof(CrsUserAuthentication),
                new PropertyMetadata(false, null)
            );
        #endregion

        /*#region AuthUser
        public UserVM AuthUser
        {
            get
            {
                return (UserVM)GetValue(AuthUserProperty);
            }
            set
            {
                SetValue(AuthUserProperty, value);
            }
        }

        public static readonly DependencyProperty AuthUserProperty =
            DependencyProperty.Register(
                "AuthUser",
                typeof(UserVM),
                typeof(CrsUserAuthentication),
                new PropertyMetadata(null, null)
            );
        #endregion*/

        #region AuthSuccess
        public bool AuthSuccess
        {
            get
            {
                return (bool)GetValue(AuthSuccessProperty);
            }
            set
            {
                SetValue(AuthSuccessProperty, value);
            }
        }

        public static readonly DependencyProperty AuthSuccessProperty =
            DependencyProperty.Register(
                "AuthSuccess",
                typeof(bool),
                typeof(CrsUserAuthentication),
                new PropertyMetadata(false, null)
            );
        #endregion

        #region AuthSize
        public Size AuthSize
        {
            get
            {
                return (Size)GetValue(AuthSizeProperty);
            }
            set
            {
                SetValue(AuthSizeProperty, value);
            }
        }

        public static readonly DependencyProperty AuthSizeProperty =
            DependencyProperty.Register(
                "AuthSize",
                typeof(Size),
                typeof(CrsUserAuthentication),
                new PropertyMetadata(null, null)
            );
        #endregion

        public void cleanForm()
        {
            passwordFormView.cleanForm();
        }
    }
}
