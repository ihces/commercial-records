using CommercialRecordSystem.Common;
using CommercialRecordSystem.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CommercialRecordSystem.Controls
{
    public sealed partial class CrsTextBox : UserControl
    {
        public CrsTextBox()
        {
            this.InitializeComponent();

            FontSize = 28;
            BorderThickness = new Thickness(3);
            ThemeBrush = new SolidColorBrush(Colors.BlueViolet);
            FontWeight = FontWeights.SemiBold;
            Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            this.DefaultStyleKey = typeof(CrsTextBox);
            RequiredSignVisibility = Windows.UI.Xaml.Visibility.Collapsed;

            Grid.SetColumn(iconContainer, 1);
            this.Loaded += CrsTextBox_Loaded;
        }

        private void CrsTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            OnApplyTemplate();
        }

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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
                new PropertyMetadata(true)
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
                typeof(CrsTextBox),
                new PropertyMetadata(false)
            );
        #endregion
        #region Icon
        public IconConsts.SegoeIcons Icon
        {
            get
            {
                return (IconConsts.SegoeIcons)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                "Icon",
                typeof(IconConsts.SegoeIcons),
                typeof(CrsTextBox),
                new PropertyMetadata(IconConsts.SegoeIcons.HOME, IconChangedHandler)
            );
        #endregion
        #region IconWidth
        public double IconWidth
        {
            get
            {
                return (double)GetValue(IconWidthProperty);
            }
            set
            {
                SetValue(IconWidthProperty, value);
            }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register(
                "IconWidth",
                typeof(double),
                typeof(CrsTextBox),
                new PropertyMetadata(60.0d, IconWidthChangedHandler)
            );
        #endregion
        #region IconFontSize
        public double IconFontSize
        {
            get
            {
                return (double)GetValue(IconFontSizeProperty);
            }
            set
            {
                SetValue(IconFontSizeProperty, value);
            }
        }

        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register(
                "IconFontSize",
                typeof(double),
                typeof(CrsTextBox),
                new PropertyMetadata(30.0d)
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
                typeof(CrsTextBox),
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
                typeof(CrsTextBox),
                new PropertyMetadata(null, InputChangedHandler)
            );
        #endregion
        #region Remark
        public string Remark
        {
            get
            {
                return (string)GetValue(RemarkProperty);
            }
            set
            {
                SetValue(RemarkProperty, value);
            }
        }

        public static readonly DependencyProperty RemarkProperty =
            DependencyProperty.Register(
                "Remark",
                typeof(string),
                typeof(CrsTextBox),
                new PropertyMetadata("Bu alanı doldurunuz", RemarkChangedHandler)
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
                typeof(CrsTextBox),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90)))
            );
        #endregion
        #endregion

        #region Input Type
        public enum INPUTTYPES { ALL, NAME, NUMBER, DOUBLE, MONEY, PHONENUMBER }

        delegate object ConvertFromDelegate(string str);
        delegate string ConvertForEditDelegate(string str);

        private struct InputTypeInfo
        {
            public string Pattern;
            public string StringFormat;
            public ConvertFromDelegate ConvertFrom;
            public ConvertForEditDelegate ConvertForEdit;


            public InputTypeInfo(string pattern, string stringFormat, ConvertFromDelegate convertFrom, ConvertForEditDelegate convertForEdit)
            {
                Pattern = pattern;
                StringFormat = stringFormat;
                ConvertFrom = convertFrom;
                ConvertForEdit = convertForEdit;
            }
        }

        private const string moneyPatternEng = @"^\$?\ ?[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$";
        private const string moneyPatternTur = @"^([1-9]{1}[\d]{0,2}(\.[\d]{3})*(\,[\d]{0,2})?(\ ?(₺))?|[1-9]{1}[\d]{0,}(\,[\d]{0,2})?(\ ?(₺))?|0(\,[\d]{0,2})?(\ ?(₺))?|(\,[\d]{1,2})?(\ ?(₺))?)$";

        private static readonly Dictionary<INPUTTYPES, InputTypeInfo> InputTypeDic = new Dictionary<INPUTTYPES, InputTypeInfo>() { 
            {INPUTTYPES.ALL, new InputTypeInfo(".*", "{0:g}", delegate(string value){ return value;}, delegate(string value){return value;})},
            {INPUTTYPES.NAME, new InputTypeInfo(@"^[a-zA-ZçÇıİüÜöÖşŞğĞ]*[\s]*[a-zA-ZçÇıİüÜöÖşŞğĞ|]*$", "{0:g}", delegate(string value){ return value;}, delegate(string value){return value;})},
            {INPUTTYPES.NUMBER, new InputTypeInfo(@"^\d+$", "{0:#}", delegate(string value){ int returnVal = 0; int.TryParse(value, out returnVal); return returnVal;}, delegate(string value){return value;})},
            {INPUTTYPES.DOUBLE, new InputTypeInfo(@"^-?\d*[\.\,]?\d+$", 
                "{0:0.00}", 
                delegate(string value){ 
                    double returnVal = 0; 
                    double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); 
                    return returnVal;
                }, 
                delegate(string value){
                        double returnVal = 0; 
                        double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal);
                        return string.Format(CultureInfo.CurrentCulture, "{0:0.00}", returnVal);
                })
            },
            {INPUTTYPES.MONEY, new InputTypeInfo(CrsDictionary.CurrentLanguage == CrsDictionary.TURKISH?moneyPatternTur:moneyPatternEng, 
                "{0:C2}",
                delegate(string value){ 
                    double returnVal = 0; 
                    double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out returnVal); 
                    return returnVal;
                }, 
                delegate(string value){
                    double returnVal = 0; 
                    double.TryParse(value, NumberStyles.Currency, CultureInfo.CurrentCulture, out returnVal);
                    return string.Format(CultureInfo.CurrentCulture, "{0:0.00}", returnVal);
                })},
            {INPUTTYPES.PHONENUMBER, new InputTypeInfo(@"^(((\+?\d)?\d)?\s?\d{3})?\s?\d{3}\s?\d{2}\s?\d{2}$", "{0:N}", 
                delegate(string value){
                    string numberBuff = value.Replace(" ", String.Empty);
                    string part1 = numberBuff.Length >= 4 ? numberBuff.Substring(numberBuff.Length - 4, 4) : "";
                    string part2 = numberBuff.Length >= 7 ? numberBuff.Substring(numberBuff.Length - 7, 3) + " ": "";
                    string part3 = numberBuff.Length >= 10 ? numberBuff.Substring(numberBuff.Length - 10, 3) + " ": "";
                    string part4 = numberBuff.Length >= 13 ? numberBuff.Substring(numberBuff.Length - 13, 3) + " " : "";
                    return part4 + part3 + part2 + part1;
                }, 
                delegate(string value){
                    return value.Replace(" ", String.Empty);
                })}
        };
        #endregion

        #region Fields
        //protected static readonly string INPUT_CHANGE_HANDLER = "input_change_handler";
        public bool AnyClickHandled = false;
        private bool isEmpty;
        private string remarkBuff = string.Empty;

        #endregion

        public void CheckIsValid()
        {
            if (string.IsNullOrWhiteSpace(textbox.Text))
            {
                isEmpty = true;
                setAsEmpty();
            }

            if (ReadOnly)
            {
                IsValid = true;
                this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            }
            else
            {
                if (isEmpty)
                {
                    if (Required)
                    {
                        IsValid = false;
                        if (AnyClickHandled)
                            this.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                    }

                    this.Input = null;
                }
                else
                {
                    textbox.Text = textbox.Text.Trim();
                    string text = textbox.Text;
                    if (Regex.IsMatch(text, InputTypeDic[InputType].Pattern))
                    {
                        IsValid = true;

                        this.Input = InputTypeDic[InputType].ConvertFrom(textbox.Text);

                        this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                    }
                    else
                    {
                        IsValid = false;
                        this.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                    }
                }
            }
        }

        protected void OnApplyTemplate()
        {
            textbox.GotFocus += gotFocusHandler;
            textbox.LostFocus += LostFocusHandler;
            textbox.TextChanged += TextChangedHandler;

            if (this.Required && !this.ReadOnly)
                RequiredSignVisibility = Visibility.Visible;

            if (Multiline)
            {
                textbox.TextWrapping = TextWrapping.Wrap;
                textbox.AcceptsReturn = true;
            }

            Thickness thicknessBuff = new Thickness(this.BorderThickness.Top);

            switch (IconVisibility)
            {
                case IconVisibilities.at_Left:
                    Grid.SetColumn(iconContainer, 1);
                    thicknessBuff.Left = 0;
                    this.BorderThickness = thicknessBuff;
                    icon.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                    break;
                case IconVisibilities.at_Right:
                    Grid.SetColumn(iconContainer, 3);
                    thicknessBuff.Right = 0;
                    icon.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                    break;
                default:
                    Grid.SetColumn(iconContainer, 0);
                    break;
            }

            BorderBrush = ThemeBrush;
            BorderThickness = thicknessBuff;
            ApplyChanges4ReadyOnly();

            CheckIsValid();
        }

        private void gotFocusHandler(object sender, RoutedEventArgs e)
        {
            if (!ReadOnly)
            {
                if (isEmpty)
                {
                    textbox.Text = string.Empty;
                    setAsNotEmpty();
                }

                if (!IsValid)
                {
                    IsValid = true;
                    this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }
                else
                {
                    textbox.Text = InputTypeDic[InputType].ConvertForEdit(textbox.Text);
                }

                AnyClickHandled = true;
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            CheckIsValid();
            if (IsValid && !isEmpty)
                textbox.Text = string.Format(InputTypeDic[InputType].StringFormat, InputTypeDic[InputType].ConvertFrom(textbox.Text.Trim()));
            
        }

        private void TextChangedHandler(object sender, RoutedEventArgs e)
        {
            if (null != TextChanged)
                TextChanged.Execute(isEmpty ? "" : textbox.Text);
        }

        private static void IconWidthChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsTextBox crsTextBox = (CrsTextBox)obj;

            if (null != crsTextBox.textbox)
            {
                Thickness thicknessBuff = new Thickness(crsTextBox.BorderThickness.Top);

                switch (crsTextBox.IconVisibility)
                {
                    case IconVisibilities.at_Left:
                        thicknessBuff.Left = 0;
                        Grid.SetColumn(crsTextBox.iconContainer, 1);
                        break;
                    case IconVisibilities.at_Right:
                        thicknessBuff.Right = 0;
                        Grid.SetColumn(crsTextBox.iconContainer, 3);
                        break;
                    default:
                        break;
                }

                crsTextBox.BorderThickness = thicknessBuff;
            }
        }

        private static void ReadOnlyChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CrsTextBox)obj).ApplyChanges4ReadyOnly();
        }

        private static void IconChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsTextBox CrsTextBox = (CrsTextBox)obj;

            if (null != CrsTextBox.iconText)
            {
                IconConsts.SegoeIcons iconBuff = (IconConsts.SegoeIcons)e.NewValue;

                if (null != iconBuff)
                    CrsTextBox.iconText.Text = IconConsts.iconStr(iconBuff);
            }
        }

        private static void InputChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsTextBox CrsTextBox = (CrsTextBox)obj;

            if (null != CrsTextBox.textbox)
            {
                string newValueStr = string.Format(InputTypeDic[CrsTextBox.InputType].StringFormat, e.NewValue);

                CrsTextBox.textbox.Text = newValueStr;

                CrsTextBox.setAsNotEmpty();
                CrsTextBox.CheckIsValid();
            }
        }

        private static void RemarkChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string remarkTemp = e.NewValue.ToString();

            if (null != remarkTemp && remarkTemp.Length > 0 && remarkTemp[0] == '#')
            {
                string[] remarkTokens = remarkTemp.Substring(1).Split(new char[] { '|' });

                if (remarkTokens.Length == 2)
                {
                    remarkTemp = CrsDictionary.getInstance().lookup(remarkTokens[0], remarkTokens[1]);
                }
            }

            ((CrsTextBox)d).remarkBuff = remarkTemp;
        }

        private void ApplyChanges4ReadyOnly()
        {
            if (ReadOnly)
            {
                if (Required)
                {
                    RequiredSignVisibility = Visibility.Collapsed;
                }
                if (!IsValid)
                {
                    Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }
                BorderBrush = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            }
            else
            {
                if (Required)
                {
                    RequiredSignVisibility = Visibility.Visible;
                    if (isEmpty && !IsValid && AnyClickHandled)
                        Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                }
                BorderBrush = ThemeBrush;
            }
        }

        private void setAsEmpty()
        {
            isEmpty = true;
            textbox.Text = remarkBuff;
            textbox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
            textbox.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        private void setAsNotEmpty()
        {
            isEmpty = false;
            textbox.Foreground = ColorConsts.TEXTBOX_NOT_EMPTY_FOREGROUND;
            textbox.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }
    }
}
