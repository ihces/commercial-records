using CommercialRecords.Common;
using CommercialRecords.Constants;
using CommercialRecords.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace CommercialRecords.Controls
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

        #region DataPermission
        public int DataPermission
        {
            get
            {
                return (int)GetValue(DataPermissionProperty);
            }
            set
            {
                SetValue(DataPermissionProperty, value);
            }
        }

        public static readonly DependencyProperty DataPermissionProperty =
            DependencyProperty.Register(
                "DataPermission",
                typeof(int),
                typeof(CrsTextBox),
                new PropertyMetadata(255, null)
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
        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }

            private set
            {
                if (Validate || !value)
                    isValid = value;
            }
        }
        #endregion
        #region Validate
        public bool Validate
        {
            get
            {
                return (bool)GetValue(ValidateProperty);
            }
            set
            {
                SetValue(ValidateProperty, value);
            }
        }

        public static readonly DependencyProperty ValidateProperty =
            DependencyProperty.Register(
                "Validate",
                typeof(bool),
                typeof(CrsTextBox),
                new PropertyMetadata(true, ValidateChangedHandler)
            );
        #endregion
        #region LowerBound
        public object LowerBound
        {
            get
            {
                return GetValue(LowerBoundProperty);
            }
            set
            {
                SetValue(LowerBoundProperty, value);
            }
        }

        public static readonly DependencyProperty LowerBoundProperty =
            DependencyProperty.Register(
                "LowerBound",
                typeof(object),
                typeof(CrsTextBox),
                new PropertyMetadata(null, boundChangedHandler)
            );
        #endregion
        #region UpperBound
        public object UpperBound
        {
            get
            {
                return GetValue(UpperBoundProperty);
            }
            set
            {
                SetValue(UpperBoundProperty, value);
            }
        }

        public static readonly DependencyProperty UpperBoundProperty =
            DependencyProperty.Register(
                "UpperBound",
                typeof(object),
                typeof(CrsTextBox),
                new PropertyMetadata(null, boundChangedHandler)
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
        public enum INPUTTYPES { ALL, NAME, NUMBER, DOUBLE, MONEY, PHONENUMBER, DATE, DATETIME, PASSWORD }

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
                })},
            {INPUTTYPES.DATE, new InputTypeInfo(@"^(\d{2})\-(\d{2})\-(\d{4})$",
                "{0:dd-MM-yyyy}",
                delegate(string value){
                    DateTime returnVal = DateTime.Now;
                    DateTime.TryParse(value, out returnVal);
                    return returnVal;
                },
                delegate(string value){
                    return value;
                })},
            { INPUTTYPES.DATETIME, new InputTypeInfo(@"^(\d{2})\-(\d{2})\-(\d{4}) (\d{2}):(\d{2}):(\d{2})$",
                "{0:dd-MM-yyyy HH:mm:ss}",
                delegate(string value){
                    DateTime returnVal = DateTime.Now;
                    DateTime.TryParse(value, out returnVal);
                    return returnVal;
                },
                delegate(string value){
                    return value;
                })},
            {INPUTTYPES.PASSWORD, new InputTypeInfo(".*", "{0:g}",
                delegate(string value){
                    return value;
                },
                delegate(string value){
                    return value;
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
            if (string.IsNullOrWhiteSpace(getText()))
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
                    setText(getText().Trim());
                    string text = getText();

                    if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
                    {
                        DateTime dtBuff = (DateTime)InputTypeDic[InputType].ConvertFrom(text);
                        if (dtBuff < new DateTime(1900, 1, 1) || dtBuff > new DateTime(2100, 1, 1))
                        {
                            IsValid = false;
                            this.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                            return;
                        }
                    }

                    if (Regex.IsMatch(text, InputTypeDic[InputType].Pattern) && suitBounds())
                    {
                        IsValid = true;

                        if (!(null != Input && Input.Equals(InputTypeDic[InputType].ConvertFrom(text))))
                            this.Input = InputTypeDic[InputType].ConvertFrom(text);

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

        private bool suitBounds()
        {
            Double inputValue = 0.0, upperBound = 0.0, lowerBound = 0.0;

            if (Input is string)
                return true;

            
            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
            {
                DateTime dateBuff = DateTime.Now;
                DateTime.TryParse(getText(), out dateBuff);
                inputValue = dateBuff.Ticks;
                upperBound = (UpperBound != null && UpperBound is DateTime) ? ((DateTime)UpperBound).Ticks : inputValue;
                lowerBound = (LowerBound != null && LowerBound is DateTime) ? ((DateTime)LowerBound).Ticks : inputValue;
            }
            else if  ((
                InputType.Equals(INPUTTYPES.DOUBLE) || 
                InputType.Equals(INPUTTYPES.MONEY) || 
                InputType.Equals(INPUTTYPES.NUMBER)) && 
                App.IsNumeric(getText()))
            {
                inputValue = Double.Parse(getText());
                upperBound = (UpperBound != null && App.IsNumeric(UpperBound)) ? (double)UpperBound : inputValue;
                lowerBound = (LowerBound != null && App.IsNumeric(LowerBound)) ? (double)LowerBound : inputValue;
            }

            bool result = true;

            if (inputValue < lowerBound)
            {
                result = false;
            }

            if (inputValue > upperBound)
            {
                result = false;
            }

            return result;
        }
        
        protected void OnApplyTemplate()
        {
            if (DataPermission >= 0)
            {
                permission = CrsAuthentication.getInstance().getPermission(DataPermission);

                if (0 == permission)
                {
                    Visibility = Visibility.Collapsed;
                    return;
                }
                if (!isEmpty)
                    maskContentIfNotPermitted();
            }

            if (this.Required && !this.ReadOnly)
                RequiredSignVisibility = Visibility.Visible;
            if (InputType.Equals(INPUTTYPES.PASSWORD))
            {
                pwdBox.GotFocus += gotFocusHandler;
                pwdBox.LostFocus += LostFocusHandler;
                pwdBox.PasswordChanged += TextChangedHandler;
            }
            else {
                textbox.GotFocus += gotFocusHandler;
                textbox.LostFocus += LostFocusHandler;
                textbox.TextChanged += TextChangedHandler;

                if (Multiline)
                {
                    textbox.TextWrapping = TextWrapping.Wrap;
                    textbox.AcceptsReturn = true;
                }
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
                    if (!(InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME)))
                        Grid.SetColumn(iconContainer, 0);
                    break;
            }

            BorderBrush = ThemeBrush;
            BorderThickness = thicknessBuff;
            ApplyChanges4ReadyOnly();
            DateTimeSelectPopupContent.Margin = new Thickness(0, this.ActualHeight, 0, 0);

            CheckIsValid();

            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
                this.DateTimePopup.DataContext = new CrsTextBoxDateTimePopupVM(this);
        }

        private void gotFocusHandler(object sender, RoutedEventArgs e)
        {
            if (!ReadOnly)
            {
                if (isEmpty)
                {
                    setText(string.Empty);
                    setAsNotEmpty();
                }

                if (!IsValid)
                {
                    IsValid = true;
                    this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }
                else
                {
                    setText(InputTypeDic[InputType].ConvertForEdit(getText()));
                }

                AnyClickHandled = true;
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            CheckIsValid();
            if (IsValid && !isEmpty)
                setText(
                    string.Format(
                        InputTypeDic[InputType].StringFormat,
                        InputTypeDic[InputType].ConvertFrom(
                            getText().Trim())));
        }

        private void TextChangedHandler(object sender, RoutedEventArgs e)
        {
            CrsAuthentication authInstance = CrsAuthentication.getInstance();

            if (!authInstance.SessionControl.SessionStatus.Equals(CrsAuthentication.SESSION_STATUS.TIME_OUT))
            {
                authInstance.updateTimeoutDate();

                if (null != TextChanged)
                    TextChanged.Execute(isEmpty ? "" : getText());
            }
            else
            {
                authInstance.showAuthentication();
            }
        }

        private static CrsTextBox objRefForDateTimePopup = null;
        private int permission = 3;

        private void iconContainer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
            {
                DateTimePopup.IsOpen = !DateTimePopup.IsOpen;

                if (DateTimePopup.IsOpen)
                {
                    if (null != objRefForDateTimePopup && objRefForDateTimePopup != this)
                    {
                        objRefForDateTimePopup.DateTimePopup.IsOpen = false;
                        objRefForDateTimePopup.icon.Background = objRefForDateTimePopup.ThemeBrush;
                        objRefForDateTimePopup.iconText.Foreground = new SolidColorBrush(Colors.White);
                    }
                    objRefForDateTimePopup = this;
                    objRefForDateTimePopup.icon.Background = new SolidColorBrush(Colors.White);
                    objRefForDateTimePopup.iconText.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    icon.Background = this.ThemeBrush;
                    iconText.Foreground = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void dateTimePopup_OnLayoutUpdated(object sender, object e)
        {
            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
            {
                Point actualPosition = TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));

                if (actualPosition.Y + this.ActualHeight < Window.Current.Bounds.Height &&
                    actualPosition.Y - this.ActualHeight < 0)
                {
                    DateTimeSelectPopupContent.Margin = new Thickness(0, this.ActualHeight, 0, 0);
                }
                else if (actualPosition.Y + this.ActualHeight + DateTimeSelectPopupContent.ActualHeight > Window.Current.Bounds.Height)
                {
                    DateTimeSelectPopupContent.Margin = new Thickness(0, -DateTimeSelectPopupContent.ActualHeight, 0, 0);
                }
            }
        }

        private static void IconWidthChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsTextBox crsTextBox = (CrsTextBox)obj;

            if (crsTextBox.isTextContainerInitialized())
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

        private static void ValidateChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsTextBox CrsTextBox = (CrsTextBox)obj;

            if (CrsTextBox.Validate)
            {
                CrsTextBox.IsValid = true;
                CrsTextBox.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            }
            else
            {
                CrsTextBox.IsValid = false;
                CrsTextBox.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
            }
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
            CrsTextBox crsTextBox = (CrsTextBox)obj;

            if (crsTextBox.isTextContainerInitialized())
            {
                string newValueStr = string.Format(InputTypeDic[crsTextBox.InputType].StringFormat, e.NewValue);

                crsTextBox.setText(newValueStr);

                crsTextBox.setAsNotEmpty();
                crsTextBox.CheckIsValid();
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

        private static void boundChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CrsTextBox)d).CheckIsValid();
        }

        private void ApplyChanges4ReadyOnly()
        {
            if (!ReadOnly && !canBeWritten())
            {
                ReadOnly = true;
            }

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

            textbox.IsReadOnly = ReadOnly;
        }

        private void setAsEmpty()
        {
            isEmpty = true;
            if (InputType.Equals(INPUTTYPES.PASSWORD))
            {
                pwdBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
                pwdBox.FontStyle = Windows.UI.Text.FontStyle.Italic;
            }
            else
            {
                textbox.Text = remarkBuff;
                textbox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
                textbox.FontStyle = Windows.UI.Text.FontStyle.Italic;
            }
        }

        private void setAsNotEmpty()
        {
            isEmpty = false;

            if (InputType.Equals(INPUTTYPES.PASSWORD))
            {
                pwdBox.Foreground = ColorConsts.TEXTBOX_NOT_EMPTY_FOREGROUND;
                pwdBox.FontStyle = Windows.UI.Text.FontStyle.Normal;
            }
            else
            {
                textbox.Foreground = ColorConsts.TEXTBOX_NOT_EMPTY_FOREGROUND;
                textbox.FontStyle = Windows.UI.Text.FontStyle.Normal;
            }

            maskContentIfNotPermitted();
        }

        private void maskContentIfNotPermitted()
        {
            if (!canBeRead())
            {
                setText("****");
            }
        }

        private bool canBeRead()
        {
            return (permission & 2) > 0;
        }

        private bool canBeWritten()
        {
            return (permission & 3) > 0;
        }

        private void setText(string newText)
        {
            if (InputType.Equals(INPUTTYPES.PASSWORD))
            {
                pwdBox.Password = newText;
            }
            else
            {
                textbox.Text = newText;
            }
        }

        private string getText()
        {
            if (isTextContainerInitialized())
            {
                if (InputType.Equals(INPUTTYPES.PASSWORD))
                {
                    return pwdBox.Password;
                }
                else {
                    return textbox.Text;
                }
            }

            return null;
        }

        private bool isTextContainerInitialized()
        {
            if (InputType.Equals(INPUTTYPES.PASSWORD) && null != pwdBox)
            {
                return true;
            }

            if (!InputType.Equals(INPUTTYPES.PASSWORD) && null != textbox)
            {
                return true;
            }

            return false;
        }
    }
}
