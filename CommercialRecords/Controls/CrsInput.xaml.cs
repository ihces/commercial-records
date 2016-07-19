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
    public class InputChangedEventArgs : EventArgs
    {
        public object NewValue { get; }
        public object OldValue { get; }

        InputChangedEventArgs(object newValue, object oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }

    public sealed partial class CrsInput : UserControl
    {
        public CrsInput()
        {
            this.InitializeComponent();

            FontSize = 28;
            BorderThickness = new Thickness(3);
            ThemeBrush = new SolidColorBrush(Colors.BlueViolet);
            FontWeight = FontWeights.SemiBold;
            Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            this.DefaultStyleKey = typeof(CrsInput);
            RequiredSignVisibility = Visibility.Collapsed;

            Grid.SetColumn(iconContainer, 1);
            this.Loaded += CrsInput_Loaded;
        }

        public void reset()
        {
            if (!validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX))
                Input = string.Empty;

            IsValid = true;
            AnyClickHandled = false;
            Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
        }

        private void CrsInput_Loaded(object sender, RoutedEventArgs e)
        {
            OnApplyTemplate();
            UCLoaded = true;
            CheckIsValid();
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
                typeof(CrsInput),
                new PropertyMetadata(INPUTTYPES.ALL, InputTypeChangedHandler)
            );
        #endregion
        #region TextChanged
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
                typeof(CrsInput),
                new PropertyMetadata(null, null)
            );

        public void setKeyUpHandler(Action<object, KeyRoutedEventArgs> inputKeyDownHandler)
        {
            switch (InputType)
            {
                case INPUTTYPES.MULTISELECT:
                    comboBox.KeyUp += new KeyEventHandler(inputKeyDownHandler);
                    break;
                case INPUTTYPES.PASSWORD:
                    pwdBox.KeyUp += new KeyEventHandler(inputKeyDownHandler);
                    break;
                default:
                    textbox.KeyUp += new KeyEventHandler(inputKeyDownHandler);
                    break;
            }
        }
        #endregion
        #region InputTemplate
        public DataTemplate InputTemplate
        {
            get
            {
                return (DataTemplate)GetValue(InputTemplateProperty);
            }
            set
            {
                SetValue(InputTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty InputTemplateProperty =
            DependencyProperty.Register(
                "InputTemplate",
                typeof(DataTemplate),
                typeof(CrsInput),
                new PropertyMetadata(null, null)
            );
        #endregion
        #region InputsSource
        public object InputsSource
        {
            get
            {
                return GetValue(InputsSourceProperty);
            }
            set
            {
                SetValue(InputsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty InputsSourceProperty =
            DependencyProperty.Register(
                "InputsSource",
                typeof(object),
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
                new PropertyMetadata(32, null)
            );
        #endregion
        #region ToolTipPopupIsOpen
        public bool ToolTipPopupIsOpen
        {
            get
            {
                return (bool)GetValue(ToolTipPopupIsOpenProperty);
            }
            set
            {
                SetValue(ToolTipPopupIsOpenProperty, value);
            }
        }

        public static readonly DependencyProperty ToolTipPopupIsOpenProperty =
            DependencyProperty.Register(
                "ToolTipPopupIsOpen",
                typeof(bool),
                typeof(CrsInput),
                new PropertyMetadata(false, null)
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                if (Validate || !value)
                    SetValue(IsValidProperty, value);
            }
        }

        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register(
                "IsValid",
                typeof(bool),
                typeof(CrsInput),
                new PropertyMetadata(true, null)
            );
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
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
                typeof(CrsInput),
                new PropertyMetadata(IconVisibilities.at_Left)
            );
        #endregion
        #region Input
        public event EventHandler InputChanged;
        private void OnInputChanged(InputChangedEventArgs e)
        {
            if (null == InputChanged) 
                InputChanged(this, e);
        }

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
                typeof(CrsInput),
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
                typeof(CrsInput),
                new PropertyMetadata("Bu alanı doldurunuz", RemarkChangedHandler)
            );

        #endregion
        #region ToolTipHeader
        public string ToolTipHeader
        {
            get
            {
                return (string)GetValue(ToolTipHeaderProperty);
            }
            set
            {
                SetValue(ToolTipHeaderProperty, value);
            }
        }

        public static readonly DependencyProperty ToolTipHeaderProperty =
            DependencyProperty.Register(
                "ToolTipHeader",
                typeof(string),
                typeof(CrsInput),
                new PropertyMetadata("", null)
            );
        #endregion
        #region ToolTipContent
        public string ToolTipContent
        {
            get
            {
                return (string)GetValue(ToolTipContentProperty);
            }
            set
            {
                toolTipVizDuration = (value.Length < 30 ? 30 : value.Length / 10);
                SetValue(ToolTipContentProperty, value);
            }
        }

        public static readonly DependencyProperty ToolTipContentProperty =
            DependencyProperty.Register(
                "ToolTipContent",
                typeof(string),
                typeof(CrsInput),
                new PropertyMetadata("", null)
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
                typeof(CrsInput),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90)))
            );
        #endregion
        #endregion

        #region Input Type
        public enum INPUTTYPES { ALL, NAME, NUMBER, DOUBLE, MONEY, PHONENUMBER, DATE, DATETIME, PASSWORD, MULTISELECT }

        delegate object ConvertFromDelegate(string str);
        delegate string ConvertForEditDelegate(string str);

        private struct InputTypeInfo
        {
            public string Pattern;
            public string FormatDesc;
            public string StringFormat;
            public ConvertFromDelegate ConvertFrom;
            public ConvertForEditDelegate ConvertForEdit;


            public InputTypeInfo(string pattern, string formatDesc, string stringFormat, ConvertFromDelegate convertFrom, ConvertForEditDelegate convertForEdit)
            {
                Pattern = pattern;
                FormatDesc = formatDesc;
                StringFormat = stringFormat;
                ConvertFrom = convertFrom;
                ConvertForEdit = convertForEdit;
            }
        }

        private const string moneyPatternEng = @"^\$?\ ?[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)$";
        private const string moneyPatternTur = @"^([1-9]{1}[\d]{0,2}(\.[\d]{3})*(\,[\d]{0,2})?(\ ?(₺))?|[1-9]{1}[\d]{0,}(\,[\d]{0,2})?(\ ?(₺))?|0(\,[\d]{0,2})?(\ ?(₺))?|(\,[\d]{1,2})?(\ ?(₺))?)$";

        private static readonly Dictionary<INPUTTYPES, InputTypeInfo> InputTypeDic = new Dictionary<INPUTTYPES, InputTypeInfo>() {
            {INPUTTYPES.ALL, new InputTypeInfo(".*", CrsDictionary.getInstance().lookup("inputFormat", "all"), "{0:g}", delegate(string value){ return value;}, delegate(string value){return value;})},
            {INPUTTYPES.NAME, new InputTypeInfo(@"^[a-zA-ZçÇıİüÜöÖşŞğĞ]*[\s]*[a-zA-ZçÇıİüÜöÖşŞğĞ|]*$", CrsDictionary.getInstance().lookup("inputFormat", "name"), "{0:g}", delegate(string value){ return value;}, delegate(string value){return value;})},
            {INPUTTYPES.NUMBER, new InputTypeInfo(@"^\d+$", CrsDictionary.getInstance().lookup("inputFormat", "number"), "{0:#}", delegate(string value){ int returnVal = 0; int.TryParse(value, out returnVal); return returnVal;}, delegate(string value){return value;})},
            {INPUTTYPES.DOUBLE, new InputTypeInfo(@"^-?\d*[\.\,]?\d+$" , CrsDictionary.getInstance().lookup("inputFormat", "double"),
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
            {INPUTTYPES.MONEY, new InputTypeInfo(CrsDictionary.CurrentLanguage == CrsDictionary.TURKISH?moneyPatternTur:moneyPatternEng, CrsDictionary.getInstance().lookup("inputFormat", "money"),
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
            {INPUTTYPES.PHONENUMBER, new InputTypeInfo(@"^(((\+?\d)?\d)?\s?\d{3})?\s?\d{3}\s?\d{2}\s?\d{2}$", CrsDictionary.getInstance().lookup("inputFormat", "phone"), "{0:N}",
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
            {INPUTTYPES.DATE, new InputTypeInfo(@"^(\d{2})\-(\d{2})\-(\d{4})$" , CrsDictionary.getInstance().lookup("inputFormat", "date"),
                "{0:dd-MM-yyyy}",
                delegate(string value){
                    DateTime returnVal = DateTime.Now;
                    DateTime.TryParse(value, out returnVal);
                    return returnVal;
                },
                delegate(string value){
                    return value;
                })},
            { INPUTTYPES.DATETIME, new InputTypeInfo(@"^(\d{2})\-(\d{2})\-(\d{4}) (\d{2}):(\d{2}):(\d{2})$" , CrsDictionary.getInstance().lookup("inputFormat", "datetime"),
                "{0:dd-MM-yyyy HH:mm:ss}",
                delegate(string value){
                    DateTime returnVal = DateTime.Now;
                    DateTime.TryParse(value, out returnVal);
                    return returnVal;
                },
                delegate(string value){
                    return value;
                })},
            {INPUTTYPES.PASSWORD, new InputTypeInfo(".*" , CrsDictionary.getInstance().lookup("inputFormat", "all"), "{0:g}",
                delegate(string value){
                    return value;
                },
                delegate(string value){
                    return value;
                })},
            {INPUTTYPES.MULTISELECT, new InputTypeInfo(".*", CrsDictionary.getInstance().lookup("inputFormat", "all"), "{0:g}", delegate(string value){ return value;}, delegate(string value){return value;})}
        };
        #endregion
        #region Fields
        //protected static readonly string INPUT_CHANGE_HANDLER = "input_change_handler";
        public bool AnyClickHandled = false;
        private bool isEmpty, UCLoaded = false, isFocussed = false, toolTipVisible = false;
        private int toolTipVizDurationCnt = 0, toolTipVizDuration = 30, toolTipDelayToShow = 30;
        private string remarkBuff = string.Empty;
        private static CrsInput objRefForDateTimePopup = null;
        private int permission = 3;
        DispatcherTimer timer4ToolTip;
        private INPUTCONTAINERS validInputContainer = INPUTCONTAINERS.TEXTBOX; // 1: textbox, 2: passwordBox, 3: comboBox
        private enum INPUTCONTAINERS { TEXTBOX, PASSWORDBOX, COMBOBOX }
        #endregion

        public void CheckIsValid()
        {
            if ((!validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX) && string.IsNullOrWhiteSpace(getText())) ||
                (validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX) && null == comboBox.SelectedItem && UCLoaded))
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
                else if (validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX))
                {
                    IsValid = true;
                    this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                }
                else {
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
            else if ((
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

            if (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX) && Multiline)
            {
                textbox.TextWrapping = TextWrapping.Wrap;
                textbox.AcceptsReturn = true;
            }

            if (validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX) && null != Input)
            {
                InputChangedHandler(this, null);
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

            timer4ToolTip = new DispatcherTimer();
            timer4ToolTip.Tick += timer4ToolTipEventHandler;
            timer4ToolTip.Interval = new TimeSpan(0, 0, 0, 0, 100);

            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
                this.DateTimePopup.DataContext = new CrsInputDateTimePopupVM(this);
        }

        private void timer4ToolTipEventHandler(object sender, object e)
        {
            if (toolTipVisible)
            {
                if (!ToolTipPopupIsOpen)
                {
                    if (toolTipVizDurationCnt < toolTipDelayToShow)
                        toolTipVizDurationCnt++;
                    else
                    {
                        toolTipVizDurationCnt = 0;
                        ToolTipPopupIsOpen = true;
                    }
                }
                else {
                    if (ToolTipPopupContent.Opacity <= 0.9)
                    {
                        ToolTipPopupContent.Opacity += 0.2;
                    }
                    else if (ToolTipPopupContent.Opacity > 0.9)
                    {
                        if (toolTipVizDurationCnt < toolTipVizDuration)
                        {
                            toolTipVizDurationCnt++;
                        }
                        else
                        {
                            toolTipVisible = false;
                        }
                    }
                }
            }
            else {
                if (ToolTipPopupContent.Opacity >= 0.1)
                {
                    ToolTipPopupContent.Opacity -= 0.2;
                }
                else if (ToolTipPopupContent.Opacity < 0.1)
                {
                    closeToolTip();
                }
            }
        }

        private void showToolTip4Notice()
        {
            ToolTipPopupIsOpen = true;
            toolTipContentTextBlock.Text = "asdlid";
            toolTipVisible = true;
            toolTipDelayToShow = 5;
            toolTipVizDuration = 500;
            toolTipVizDurationCnt = 0;
            timer4ToolTip.Start();
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs args)
        {
            if (!isFocussed && !ToolTipPopupIsOpen && !DateTimePopup.IsOpen && IsValid && !string.IsNullOrWhiteSpace(ToolTipContent))
            {
                toolTipContentTextBlock.Text = ToolTipContent;
                toolTipVizDurationCnt = 0;
                toolTipVizDuration = 30;
                toolTipDelayToShow = 5;
                ToolTipPopupContent.Opacity = 0.0;
                toolTipVisible = true;
                timer4ToolTip.Start();
            }
        }

        protected override void OnPointerExited(PointerRoutedEventArgs args)
        {
            if (!isFocussed)
            {
                closeToolTip();
            }
        }

        private void closeToolTip()
        {
            ToolTipPopupIsOpen = false;
            if (null != timer4ToolTip)
                timer4ToolTip.Stop();
        }

        private void gotFocusHandler(object sender, RoutedEventArgs e)
        {
            if (!ReadOnly)
            {
                closeToolTip();

                if (isEmpty)
                {
                    setText(string.Empty);
                    setAsNotEmpty();
                }

                if (!IsValid)
                {
                    IsValid = true;
                    this.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
                    if (AnyClickHandled)
                        showToolTip4Notice();
                }
                else if (!validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX))
                {
                    setText(InputTypeDic[InputType].ConvertForEdit(getText()));
                }

                AnyClickHandled = true;
                isFocussed = true;
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e)
        {
            isFocussed = false;

            if (ToolTipPopupIsOpen)
                closeToolTip();

            CheckIsValid();
            if (IsValid && !isEmpty && !validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX))
                setText(
                    string.Format(
                        InputTypeDic[InputType].StringFormat,
                        InputTypeDic[InputType].ConvertFrom(
                            getText().Trim())));
        }

        private void InputChangedViaUIHandler(object sender, RoutedEventArgs e)
        {
            if (ToolTipPopupIsOpen)
                closeToolTip();

            CrsAuthentication authInstance = CrsAuthentication.getInstance();

            if (!authInstance.SessionControl.SessionStatus.Equals(CrsAuthentication.SESSION_STATUS.TIME_OUT))
            {
                authInstance.updateTimeoutDate();

                if (validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX))
                {
                    Input = comboBox.SelectedItem;
                }
                else
                {
                    if (null != TextChanged)
                        TextChanged.Execute(isEmpty ? "" : getText());
                }
            }
            else
            {
                authInstance.showAuthentication();
            }
        }

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
            if (!CommonUIFunctions.isVisible(this))
            {
                DateTimePopup.IsOpen = false;
                return;
            }

            if (InputType.Equals(INPUTTYPES.DATE) || InputType.Equals(INPUTTYPES.DATETIME))
            {
                Point actualPosition = TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));
                if (actualPosition.Y + this.ActualHeight + DateTimeSelectPopupContent.ActualHeight > Window.Current.Bounds.Height)
                {
                    DateTimeSelectPopupContent.Margin = new Thickness(0, -DateTimeSelectPopupContent.ActualHeight, 0, 0);
                }
                else
                {
                    DateTimeSelectPopupContent.Margin = new Thickness(0, this.ActualHeight, 0, 0);
                }
            }
        }

        private void toolTipPopup_OnLayoutUpdated(object sender, object e)
        {
            OnInputChanged(new InputChangedEventArgs(1, 1));
            if (!CommonUIFunctions.isVisible(this))
            {
                closeToolTip();
                return;
            }

            Point actualPosition = TransformToVisual(Window.Current.Content).TransformPoint(new Point(0, 0));
            if (actualPosition.Y > this.ToolTipPopupContent.ActualHeight)
            {
                ToolTipPopupContent.Margin = new Thickness(0, -ToolTipPopupContent.ActualHeight, 0, 0);
            }
            else
            {
                ToolTipPopupContent.Margin = new Thickness(0, this.ActualHeight, 0, 0);
            }
        }

        private static void IconWidthChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsInput CrsInput = (CrsInput)obj;

            if (CrsInput.isTextContainerInitialized())
            {
                Thickness thicknessBuff = new Thickness(CrsInput.BorderThickness.Top);

                switch (CrsInput.IconVisibility)
                {
                    case IconVisibilities.at_Left:
                        thicknessBuff.Left = 0;
                        Grid.SetColumn(CrsInput.iconContainer, 1);
                        break;
                    case IconVisibilities.at_Right:
                        thicknessBuff.Right = 0;
                        Grid.SetColumn(CrsInput.iconContainer, 3);
                        break;
                    default:
                        break;
                }

                CrsInput.BorderThickness = thicknessBuff;
            }
        }

        private static void ReadOnlyChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((CrsInput)obj).ApplyChanges4ReadyOnly();
        }

        private static void ValidateChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsInput CrsInput = (CrsInput)obj;

            if (CrsInput.Validate)
            {
                CrsInput.IsValid = true;
                CrsInput.Background = ColorConsts.TEXTBOX_BACKGROUND_VALID;
            }
            else
            {
                CrsInput.IsValid = false;
                CrsInput.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
            }
        }

        private static void IconChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsInput CrsInput = (CrsInput)obj;

            if (null != CrsInput.iconText)
            {
                IconConsts.SegoeIcons iconBuff = (IconConsts.SegoeIcons)e.NewValue;

                if (null != iconBuff)
                    CrsInput.iconText.Text = IconConsts.iconStr(iconBuff);
            }
        }

        private static void InputTypeChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CrsInput crsInput = (CrsInput)obj;

            switch (crsInput.InputType)
            {
                case INPUTTYPES.PASSWORD:
                    crsInput.validInputContainer = INPUTCONTAINERS.PASSWORDBOX;
                    crsInput.pwdBox.GotFocus += crsInput.gotFocusHandler;
                    crsInput.pwdBox.LostFocus += crsInput.LostFocusHandler;
                    crsInput.pwdBox.PasswordChanged += crsInput.InputChangedViaUIHandler;
                    break;
                case INPUTTYPES.MULTISELECT:
                    crsInput.validInputContainer = INPUTCONTAINERS.COMBOBOX;
                    crsInput.comboBox.SelectionChanged += crsInput.InputChangedViaUIHandler;
                    crsInput.comboBox.GotFocus += crsInput.gotFocusHandler;
                    crsInput.comboBox.LostFocus += crsInput.LostFocusHandler;
                    break;
                default:
                    crsInput.validInputContainer = INPUTCONTAINERS.TEXTBOX;
                    crsInput.textbox.GotFocus += crsInput.gotFocusHandler;
                    crsInput.textbox.LostFocus += crsInput.LostFocusHandler;
                    crsInput.textbox.TextChanged += crsInput.InputChangedViaUIHandler;
                    break;
            }
        }

        private static void InputChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            CrsInput CrsInput = (CrsInput)obj;


            CrsInput.OnInputChanged(new InputChangedEventArgs(args.NewValue, args.OldValue));
             
            object newValue = CrsInput.Input;
            if (args != null)
                newValue = args.NewValue;

            if (CrsInput.validInputContainer.Equals(INPUTCONTAINERS.COMBOBOX) && 0 < CrsInput.comboBox.Items.Count)
            {
                for (int i = 0; i < CrsInput.comboBox.Items.Count; ++i)
                {
                    if (CrsInput.comboBox.Items[i].Equals(newValue))
                    {
                        CrsInput.comboBox.SelectedItem = CrsInput.comboBox.Items[i];

                        if (null == CrsInput.comboBox.SelectedItem)
                            CrsInput.setAsEmpty();
                        else
                            CrsInput.setAsNotEmpty();
                        break;
                    }
                }
            }
            else if (CrsInput.isTextContainerInitialized())
            {
                string newValueStr = string.Format(InputTypeDic[CrsInput.InputType].StringFormat, newValue);

                CrsInput.setText(newValueStr);
                CrsInput.setAsNotEmpty();
                CrsInput.CheckIsValid();
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

            ((CrsInput)d).remarkBuff = remarkTemp;
        }

        private static void boundChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CrsInput)d).CheckIsValid();
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
            if (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX))
            {
                textbox.Text = remarkBuff;
                textbox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x90, 0x90, 0x90));
                textbox.FontStyle = Windows.UI.Text.FontStyle.Italic;
            }
        }

        private void setAsNotEmpty()
        {
            isEmpty = false;

            if (validInputContainer.Equals(INPUTCONTAINERS.PASSWORDBOX))
            {
                pwdBox.Foreground = ColorConsts.TEXTBOX_NOT_EMPTY_FOREGROUND;
                pwdBox.FontStyle = Windows.UI.Text.FontStyle.Normal;
            }
            else if (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX))
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
            if (validInputContainer.Equals(INPUTCONTAINERS.PASSWORDBOX))
            {
                pwdBox.Password = newText;
            }
            else if (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX))
            {
                textbox.Text = newText;
            }
        }

        private string getText()
        {
            if (isTextContainerInitialized())
            {
                if (validInputContainer.Equals(INPUTCONTAINERS.PASSWORDBOX))
                {
                    return pwdBox.Password;
                }
                else if (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX))
                {
                    return textbox.Text;
                }
            }

            return null;
        }

        public Control getActiveControl()
        {
            switch (InputType)
            {
                case INPUTTYPES.PASSWORD:
                    return pwdBox;
                case INPUTTYPES.MULTISELECT:
                    return comboBox;
                default:
                    return textbox;
            }
        }

        private bool isTextContainerInitialized()
        {
            if ((validInputContainer.Equals(INPUTCONTAINERS.PASSWORDBOX) && null != pwdBox) ||
                (validInputContainer.Equals(INPUTCONTAINERS.TEXTBOX) && null != textbox))
            {
                return true;
            }

            return false;
        }

        public FocusState GetFocusState()
        {
            switch (InputType)
            {
                case INPUTTYPES.PASSWORD:
                    return pwdBox.FocusState;
                case INPUTTYPES.MULTISELECT:
                    return comboBox.FocusState;
                default:
                    return textbox.FocusState;
            }
        }

        public void SetFocusState(FocusState focusState)
        {
            if (!focusState.Equals(FocusState.Unfocused))
            {
                switch (InputType)
                {
                    case INPUTTYPES.PASSWORD:
                        pwdBox.Focus(focusState);
                        break;
                    case INPUTTYPES.MULTISELECT:
                        comboBox.Focus(focusState);
                        break;
                    default:
                        textbox.Focus(focusState);
                        break;
                }
            }
        }
    }
}
