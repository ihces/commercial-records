using CommercialRecordSystem.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
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

        public ICommand TextChanged
        {
            get
            {
                return (ICommand)GetValue(TextChangedProperty);
            }
            set
            {
                SetValue(TextChangedProperty, value);
            }
        }

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.Register(
                "TextChanged",
                typeof(ICommand),
                typeof(CRSTextBox),
                new PropertyMetadata(null, null)
            );

        #endregion
        #region InputMaxLength
        public int InputMaxLength
        {
            get
            {
                return (int)GetValue(InputMaxLengthProperty);
            }
            set
            {
                SetValue(InputMaxLengthProperty, value);
            }
        }

        public static readonly DependencyProperty InputMaxLengthProperty =
            DependencyProperty.Register(
                "InputMaxLength",
                typeof(int),
                typeof(CRSTextBox),
                new PropertyMetadata(64, null)
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
        #region RequiredSignVisibility
        public Visibility RequiredSignVisibility
        {
            get
            {
                return (Visibility)GetValue(RequiredSignVisibilityProperty);
            }
            set
            {
                SetValue(RequiredSignVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty RequiredSignVisibilityProperty =
            DependencyProperty.Register(
                "RequiredSignVisibility",
                typeof(Visibility),
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
                new PropertyMetadata(null)
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
        public enum IconVisibilities { at_Right, at_Left, Collapsed };
        public IconVisibilities IconVisibility
        {
            get
            {
                return (IconVisibilities)GetValue(IconVisibilityProperty);
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
                new PropertyMetadata(IconVisibilities.at_Left)
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
                new PropertyMetadata(null, InputChangedHandler)
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
        #region ThemeBrush
        public Brush ThemeBrush
        {
            get
            {
                return (Brush)GetValue(ThemeBrushProperty);
            }
            set
            {
                SetValue(ThemeBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ThemeBrushProperty =
            DependencyProperty.Register(
                "ThemeBrush",
                typeof(Brush),
                typeof(CRSTextBox),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90)))
            );
        #endregion
        #endregion

        #region Input Type
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
            {INPUTTYPES.NAME, new InputTypeInfo(@"^[a-zA-ZçÇıİüÜöÖşŞğĞ]*[\s]*[a-zA-ZçÇıİüÜöÖşŞğĞ|]*$", "{0:g}", delegate(string value){ return value;})},
            {INPUTTYPES.NUMBER, new InputTypeInfo(@"^\d+$", "{0:#}", delegate(string value){ int returnVal = 0; int.TryParse(value, out returnVal); return returnVal;})},
            {INPUTTYPES.DOUBLE, new InputTypeInfo(@"^-?\d*\.?\d+$", "{0:#,#.#}", delegate(string value){ double returnVal = 0; double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); return returnVal;})},
            {INPUTTYPES.MONEY, new InputTypeInfo(@"^\d+([.,]\d{1,2})?$", "{0:#,#.#}", delegate(string value){ double returnVal = 0; double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); return returnVal;})},
            {INPUTTYPES.PHONENUMBER, new InputTypeInfo(@"^(((\+?\d)?\d)?\s?\d{3})?\s?\d{3}\s?\d{2}\s?\d{2}$", "{0:g}", 
                delegate(string value){
                    string numberBuff = value.Replace(" ", String.Empty);
                    string part1 = numberBuff.Length >= 4 ? numberBuff.Substring(numberBuff.Length - 4, 4) : "";
                    string part2 = numberBuff.Length >= 7 ? numberBuff.Substring(numberBuff.Length - 7, 3) + " ": "";
                    string part3 = numberBuff.Length >= 10 ? numberBuff.Substring(numberBuff.Length - 10, 3) + " ": "";
                    string part4 = numberBuff.Length >= 13 ? numberBuff.Substring(numberBuff.Length - 13, 3) + " " : "";
                    return part4 + part3 + part2 + part1;
                })}
        };
        #endregion

        #region Fields
        //protected static readonly string INPUT_CHANGE_HANDLER = "input_change_handler";
        public bool AnyClickHandled = false;
        private bool isEmpty;
        private TextBox textBox;
        private Grid icon;
        private bool updateInput = false;
        
        #endregion

        public CRSTextBox()
        {
            FontSize = 32;
            BorderThickness = new Thickness(3);
            FontWeight = FontWeights.SemiBold;
            Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            this.DefaultStyleKey = typeof(CRSTextBox);
            RequiredSignVisibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void CheckIsValid()//string callFrom = "")
        {
            if (ReadOnly)
            {
                IsValid = true;
                textBox.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            }
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    isEmpty = true;

                if (isEmpty)
                {
                    setAsEmpty();
                    if (Required)
                    {
                        IsValid = false;
                        if (AnyClickHandled)
                            this.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                    }

                    //if (!callFrom.Equals(INPUT_CHANGE_HANDLER))
                        this.Input = null;
                }
                else
                {
                    textBox.Text = textBox.Text.Trim();
                    string text = textBox.Text;
                    if (Regex.IsMatch(text, InputTypeDic[InputType].Pattern))
                    {
                        IsValid = true;
                        //if (!callFrom.Equals(INPUT_CHANGE_HANDLER))
                          //  justUpdateInput();

                        this.Input = InputTypeDic[InputType].ConvertFrom(textBox.Text);

                        this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                    }
                    else
                    {
                        IsValid = false;
                        this.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;

                        //if (!callFrom.Equals(INPUT_CHANGE_HANDLER))
                          //  justUpdateInput();

                        Input = null;
                    }
                }
            }
        }

        /*private void justUpdateInput()
        {
            updateInput = true;
        }*/

        protected override void OnApplyTemplate()
        {
            textBox = GetTemplateChild("textbox") as TextBox;
            icon = GetTemplateChild("icon") as Grid;

            BorderBrush = ThemeBrush;
            if (this.ReadOnly)
                this.BorderBrush = new SolidColorBrush(Color.FromArgb(0x90, 0x90, 0x90, 0x90));
            else
            if (this.Required && !this.ReadOnly)
                RequiredSignVisibility = Visibility.Visible;

            string newValueStr = string.Format(InputTypeDic[InputType].StringFormat, Input);
            if (string.IsNullOrWhiteSpace(newValueStr))
            {
                Input = null; // calls changed handler and set textbox as empty
                setAsEmpty();
            }
            else
            {
                textBox.Text = newValueStr;
                setAsNotEmpty();
            }

            if (Multiline)
            {
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.AcceptsReturn = true;
            }
            textBox.GotFocus += new RoutedEventHandler(gotFocusHandler);
            textBox.LostFocus += new RoutedEventHandler(LostFocusHandler);
            textBox.TextChanged += new TextChangedEventHandler(TextChangedHandler);

            if (Required)
                IsValid = false;
            else
                IsValid = true;

            this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;

            
            switch(IconVisibility)
            {
                case IconVisibilities.at_Left:
                    textBox.Margin = new Thickness(60,0,0,0);
                    icon.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    break;
                case IconVisibilities.at_Right:
                    textBox.Margin = new Thickness(0,0,60,0);
                    icon.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                    break;
                default:
                    icon.Visibility = Visibility.Collapsed;
                    break;
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
                    setAsNotEmpty();
                }

                if (!IsValid)
                {
                    IsValid = true;
                    this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }

                AnyClickHandled = true;
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            CheckIsValid();
        }

        private void TextChangedHandler(object sender, RoutedEventArgs e)
        {
            if (null != TextChanged)
                TextChanged.Execute(isEmpty? "": textBox.Text);
        }

        private static void ReadOnlyChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CRSTextBox csrTextBox = (CRSTextBox)obj;
            if ((bool)e.NewValue)
            {
                if (csrTextBox.Required)
                {
                    csrTextBox.RequiredSignVisibility = Visibility.Collapsed;
                }
                if (!csrTextBox.IsValid)
                {
                    csrTextBox.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }
                csrTextBox.BorderBrush = new SolidColorBrush(Color.FromArgb(0x90, 0x90, 0x90, 0x90));
            }
            else
            {
                if (csrTextBox.Required)
                {
                    csrTextBox.RequiredSignVisibility = Visibility.Visible;
                    if (csrTextBox.isEmpty && !csrTextBox.IsValid)
                        csrTextBox.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                }
                csrTextBox.BorderBrush = csrTextBox.ThemeBrush;
            }
        }

        private static void InputChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CRSTextBox csrTextBox = (CRSTextBox)obj;

            if (null != csrTextBox.textBox)
            {
                /*if (csrTextBox.updateInput)
                {
                    csrTextBox.updateInput = false;
                }
                else
                {*/
                    string newValueStr = string.Format(InputTypeDic[csrTextBox.InputType].StringFormat, e.NewValue);
                    csrTextBox.textBox.Text = newValueStr;
                //}
                csrTextBox.setAsNotEmpty();
                csrTextBox.CheckIsValid();//INPUT_CHANGE_HANDLER);
            }
        }

        private void setAsEmpty()
        {
            isEmpty = true;
            textBox.Text = EmptyMessage;
            textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
            textBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        private void setAsNotEmpty()
        {
            isEmpty = false;
            textBox.Foreground = ColorConsts.TEXTBOX_NOT_EMPTY_FOREGROUND;
            textBox.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }
    }
}
