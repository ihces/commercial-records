﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Controls
{
    class CrsAppBarButton : AppBarButton, CrsButtonIntf
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
                typeof(CrsAppBarButton),
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
                typeof(CrsAppBarButton),
                new PropertyMetadata(false, DisabledChangedHandler)
            );
        #endregion
        #endregion

        private static void DisabledChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AppBarButton button = (AppBarButton)obj;
            button.IsEnabled = !(bool)e.NewValue;
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
