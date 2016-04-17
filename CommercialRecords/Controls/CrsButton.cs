using CommercialRecords.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecords.Controls
{
    class CrsButton : Button, CrsButtonIntf
    {
        #region Properties
        #region FunctionalPermission
        public int FunctionalPermission
        {
            get
            {
                return (int)GetValue(FunctionalPermissionProperty);
            }
            set
            {
                SetValue(FunctionalPermissionProperty, value);
            }
        }

        public static readonly DependencyProperty FunctionalPermissionProperty =
            DependencyProperty.Register(
                "FunctionalPermission",
                typeof(int),
                typeof(CrsAppBarButton),
                new PropertyMetadata(255)
            );
        #endregion

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
                typeof(CrsButton),
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
                typeof(CrsButton),
                new PropertyMetadata(false, DisabledChangedHandler)
            );
        #endregion

        private int permission = 3;
        #endregion


        protected override void OnApplyTemplate()
        {
            if (FunctionalPermission >= 0)
            {
                permission = CrsAuthentication.getInstance().getPermission(FunctionalPermission);

                if (0 == (permission & 1))
                {
                    Visibility = Visibility.Collapsed;
                }
                else if (1 == permission)
                {
                    IsEnabled = false;
                }
            }
        }

        private static void DisabledChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsButton button = (CrsButton)obj;
            bool newVal = !(bool)e.NewValue;
            if (!(1 == button.permission && newVal))
            {
                button.IsEnabled = newVal;
            }
        }

        public void setClickHandler(Action<object, RoutedEventArgs> clickHandlerMethod)
        {
            Click += new RoutedEventHandler(clickHandlerMethod);
        }

        public void setCommandCanExecute(bool canExecute)
        {
            if (null != Command)
                Command.CanExecute(canExecute);
        }
    }
}
