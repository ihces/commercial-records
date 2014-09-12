using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace CommercialRecordSystem.Controls
{
    public sealed class CRSTextBox : Control
    {
        public CRSTextBox()
        {
            this.DefaultStyleKey = typeof(CRSTextBox);
        }

        protected override void OnApplyTemplate()
        {
            TextBox d = GetTemplateChild("PART_TEXT") as TextBox;
            d.GotFocus += new RoutedEventHandler(gotFocusHandler);
            d.LostFocus += new RoutedEventHandler(LostFocusHandler);
            EmptyMessage = "Adam akıllı birşey gir";
            base.OnApplyTemplate();
        }

        private void gotFocusHandler(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Equals(EmptyMessage))
                textBox.Text = string.Empty;
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Trim().Length > 0)
                this.Text = textBox.Text;
            else
                textBox.Text = "Adam akıllı birşey gir";

        }

        #region IconHex
        public string IconHex
        {
            get
            {
                return (string)GetValue(IconHexProperty);
            }
            set
            {
                SetValue(IconHexProperty, value);
            }
        }

        public static readonly DependencyProperty IconHexProperty =
            DependencyProperty.Register(
                "IconHex",
                typeof(string),
                typeof(CRSTextBox),
                new PropertyMetadata(
                    false,
                    null
                )
            );
        #endregion

        #region Text
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value); 
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",                  
                typeof(string),             
                typeof(CRSTextBox),       
                new PropertyMetadata(     
                    false,                
                    null
                )
            );
        #endregion

        #region EmptyMessage
        public string EmptyMessage
        {
            get
            {
                return (string)GetValue(EmptyMessageProperty);
            }
            set
            {
                SetValue(EmptyMessageProperty, value);
            }
        }

        public static readonly DependencyProperty EmptyMessageProperty =
            DependencyProperty.Register(
                "EmptyMessage",
                typeof(string),
                typeof(CRSTextBox),
                new PropertyMetadata(
                    false,
                    null
                )
            );
        #endregion
    }
}
