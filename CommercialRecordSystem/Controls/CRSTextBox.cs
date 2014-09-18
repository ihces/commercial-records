using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace CommercialRecordSystem.Controls
{
    public sealed class CRSTextBox : Control
    {
        #region Properties
        #region ReadOnly
        public bool ReadOnly
        {
            get
            {
                return (bool)GetValue(ReadOnlyProperty);
            }
            set
            {
                SetValue(ReadOnlyProperty, value);
            }
        }

        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register(
                "ReadOnly",
                typeof(bool),
                typeof(CRSTextBox),
                new PropertyMetadata(false)
            );
        #endregion
        #region Required
        public bool Required
        {
            get
            {
                return (bool)GetValue(RequiredProperty);
            }
            set
            {
                SetValue(RequiredProperty, value);
            }
        }

        public static readonly DependencyProperty RequiredProperty =
            DependencyProperty.Register(
                "Required",
                typeof(bool),
                typeof(CRSTextBox),
                new PropertyMetadata(false)
            );
        #endregion
        #region IsValid
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }

        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register(
                "IsValid",
                typeof(bool),
                typeof(CRSTextBox),
                new PropertyMetadata(false)
            );
        #endregion
        #region Multiline
        public bool Multiline
        {
            get
            {
                return (bool)GetValue(MultilineProperty);
            }
            set
            {
                SetValue(MultilineProperty, value);
            }
        }

        public static readonly DependencyProperty MultilineProperty =
            DependencyProperty.Register(
                "Multiline",
                typeof(bool),
                typeof(CRSTextBox),
                new PropertyMetadata(false)
            );
        #endregion
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
                new PropertyMetadata("")
            );
        #endregion
        #region IconVisibility
        public Visibility IconVisibility
        {
            get
            {
                return (Visibility)GetValue(IconVisibilityProperty);
            }
            set
            {
                SetValue(IconVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register(
                "IconVisibility",
                typeof(Visibility),
                typeof(CRSTextBox),
                new PropertyMetadata(Visibility.Collapsed)
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
                new PropertyMetadata("")
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
                new PropertyMetadata("Bu alanı doldurunuz")
            );
        #endregion

        private bool isEmpty;

        private TextBox textBox;
        #endregion

        public CRSTextBox()
        {
            this.BorderThickness = new Thickness(3);
            FontSize = 32;
            FontWeight = FontWeights.SemiBold;
            isEmpty = true;
            
            this.DefaultStyleKey = typeof(CRSTextBox);
        }

        protected override void OnApplyTemplate()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
            if (this.ReadOnly)
                this.BorderThickness = new Thickness(0);
            textBox = GetTemplateChild("textbox") as TextBox;
            textBox.Text = EmptyMessage;
            textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xb0, 0xb0, 0xb0));
            textBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
            textBox.GotFocus += new RoutedEventHandler(gotFocusHandler);
            textBox.LostFocus += new RoutedEventHandler(LostFocusHandler);
            
            if (Multiline)
            {
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.AcceptsReturn = true;
            }
            
            base.OnApplyTemplate();
        }

        private void gotFocusHandler(object sender, RoutedEventArgs e)
        {
            if (!ReadOnly)
            {
                if (isEmpty)
                {
                    textBox.Text = string.Empty;
                    if (Required)
                        this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
                }
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                textBox.FontStyle = Windows.UI.Text.FontStyle.Normal;

                isEmpty = false;
                IsValid = true;
            }
        }

        public void Validate()
        {
            if (!ReadOnly)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    isEmpty = true;
                    IsValid = false;
                }

                if (isEmpty)
                {
                    textBox.Text = EmptyMessage;
                    if (Required)
                    {
                        this.Background = new SolidColorBrush(Color.FromArgb(0x88, 0xad, 0x10, 0x3c));
                    }
                    textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xb0, 0xb0, 0xb0));
                    textBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
                }
                else
                {
                    this.Text = textBox.Text;
                    if (Required)
                    {
                        this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
                    }
                }
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            Validate();
        }
    }
}
