using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace CommercialRecordSystem.Controls
{
    public sealed class CRSTextBox : Control
    {
        public enum INPUTTYPES {ALL, NAME, NUMBER, DOUBLE, MONEY, PHONENUMBER} 

        delegate object ConvertFromDelegate(string str);
        private struct InputTypeInfo 
        {
            public string Pattern;
            public string StringFormat;
            public ConvertFromDelegate ConvertFrom;

            public InputTypeInfo(string pattern, string stringFormat, ConvertFromDelegate convertFrom)
            {
                Pattern = pattern;
                StringFormat = stringFormat;
                ConvertFrom = convertFrom;
            }
        }

        private static readonly Dictionary<INPUTTYPES, InputTypeInfo> InputTypeDic = new Dictionary<INPUTTYPES, InputTypeInfo>() { 
            {INPUTTYPES.ALL, new InputTypeInfo(".*", "{0:g}", delegate(string value){ return value;})},
            {INPUTTYPES.NAME, new InputTypeInfo(@"^[a-zA-Z]*[\s]*[a-zA-Z|]*$", "{0:g}", delegate(string value){ return value;})},
            {INPUTTYPES.NUMBER, new InputTypeInfo(@"^\d+$", "{0:#}", delegate(string value){ int returnVal = 0; int.TryParse(value, out returnVal); return returnVal;})},
            {INPUTTYPES.DOUBLE, new InputTypeInfo(@"^-?\d*\.?\d+$", "{0:#,#.#}", delegate(string value){ double returnVal = 0; double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); return returnVal;})},
            {INPUTTYPES.MONEY, new InputTypeInfo(@"^\d+([.,]\d{1,2})?$", "{0:c}", delegate(string value){ double returnVal = 0; double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); return returnVal;})},
            {INPUTTYPES.PHONENUMBER, new InputTypeInfo(@"^((\+?\d)?\d)?\s?(\d{3})?\s?\d{3}\s?\d{2}\s?\d{2}$", "{0:g}", 
                delegate(string value){
                    string numberBuff = value.Replace(" ", String.Empty);
                    string part1 = numberBuff.Length >= 4 ? numberBuff.Substring(numberBuff.Length - 4, 4) : "";
                    string part2 = numberBuff.Length >= 7 ? numberBuff.Substring(numberBuff.Length - 7, 3) + " ": "";
                    string part3 = numberBuff.Length >= 10 ? numberBuff.Substring(numberBuff.Length - 10, 3) + " ": "";
                    string part4 = numberBuff.Length >= 13 ? numberBuff.Substring(numberBuff.Length - 13, 3) + " " : "";
                    return part4 + part3 + part2 + part1;
                })}
        };

        #region Properties
        #region InputType
        public INPUTTYPES InputType
        {
            get
            {
                return (INPUTTYPES)GetValue(InputTypeProperty);
            }
            set
            {
                SetValue(InputTypeProperty, value);
            }
        }

        public static readonly DependencyProperty InputTypeProperty =
            DependencyProperty.Register(
                "InputType",
                typeof(INPUTTYPES),
                typeof(CRSTextBox),
                new PropertyMetadata(INPUTTYPES.ALL, null)
            );
        #endregion
        #region MaxSize
        public int MaxSize
        {
            get
            {
                return (int)GetValue(MaxSizeProperty);
            }
            set
            {
                SetValue(MaxSizeProperty, value);
            }
        }

        public static readonly DependencyProperty MaxSizeProperty =
            DependencyProperty.Register(
                "MaxSize",
                typeof(int),
                typeof(CRSTextBox),
                new PropertyMetadata(32, null)
            );
        #endregion
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
                new PropertyMetadata(false, ReadOnlyChangedHandler)
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
        #region Input
        public object Input
        {
            get
            {
                return (object)GetValue(InputProperty);
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register(
                "Input",
                typeof(object),
                typeof(CRSTextBox),
                new PropertyMetadata(new object())
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

        private static void ReadOnlyChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CRSTextBox csrTextBox = (CRSTextBox)obj;
            if ((bool)e.NewValue)
                csrTextBox.BorderThickness = new Thickness(0);
            else
                csrTextBox.BorderThickness = new Thickness(3);
        }

        public CRSTextBox()
        {
            this.BorderThickness = new Thickness(3);
            FontSize = 32;
            FontWeight = FontWeights.SemiBold;
            
            this.DefaultStyleKey = typeof(CRSTextBox);
        }

        protected override void OnApplyTemplate()
        {
            this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
            if (this.ReadOnly)
                this.BorderThickness = new Thickness(0);
            
            textBox = GetTemplateChild("textbox") as TextBox;
            
            if (string.IsNullOrEmpty((string)this.Input))
            {
                isEmpty = true;
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
                textBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
                textBox.Text = EmptyMessage;
            }
            else
            {
                isEmpty = false;
                IsValid = true;
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                textBox.FontStyle = Windows.UI.Text.FontStyle.Normal;
                textBox.Text = string.Format(InputTypeDic[InputType].StringFormat, this.Input);
            }
            
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
                    textBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                    textBox.FontStyle = Windows.UI.Text.FontStyle.Normal;
                    isEmpty = false;
                }

                if (!IsValid)
                    this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
            }
        }

        public void Validate()
        {
            if (!ReadOnly)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    isEmpty = true;

                if (isEmpty)
                {
                    IsValid = false;
                    textBox.Text = EmptyMessage;
                    if (Required)
                    {
                        this.Background = new SolidColorBrush(Color.FromArgb(0x88, 0xad, 0x10, 0x3c));
                    }
                    textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
                    textBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
                }
                else
                {
                    textBox.Text = textBox.Text.Trim();
                    string text = textBox.Text;
                    if (Regex.IsMatch(text, InputTypeDic[InputType].Pattern))
                    {
                        IsValid = true;
                        this.Input = InputTypeDic[InputType].ConvertFrom(textBox.Text);
                        this.Background = new SolidColorBrush(Color.FromArgb(0x28, 0xff, 0xff, 0xff));
                    }
                    else
                    {
                        IsValid = false;
                        this.Background = new SolidColorBrush(Color.FromArgb(0x88, 0xad, 0x10, 0x3c));
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
