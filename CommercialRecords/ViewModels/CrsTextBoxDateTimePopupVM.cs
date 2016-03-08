

using CommercialRecords.Common;
using CommercialRecords.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CommercialRecords.ViewModels
{
    public class CrsTextBoxDateTimePopupVM : VMBase
    {
        private CrsTextBox ContextOwner;

        public class CalendarDayButton : VMBase
        {
            private Brush background;
            public Brush Background
            {
                get
                {
                    return background;
                }
                set
                {
                    background = value;
                    RaisePropertyChanged("Background");
                }
            }

            private Brush foreground;
            public Brush Foreground
            {
                get
                {
                    return foreground;
                }
                set
                {
                    foreground = value;
                    RaisePropertyChanged("Foreground");
                }
            }

            public string DateStr
            {
                get
                {
                    return Date.Day.ToString();
                }
            }

            private DateTime date;
            public DateTime Date
            {
                get
                {
                    return date;
                }
                set
                {
                    date = value;
                    RaisePropertyChanged("Date");
                    RaisePropertyChanged("DateStr");
                }
            }

            public CalendarDayButton()
            {
                Background = new SolidColorBrush(Colors.Transparent);
                Date = DateTime.Now;
                Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private ObservableCollection<ObservableCollection<CalendarDayButton>> visibleDays;
        public ObservableCollection<ObservableCollection<CalendarDayButton>> VisibleDays
        {
            get
            {
                return visibleDays;
            }
            set
            {
                visibleDays = value;
                RaisePropertyChanged("VisibleDays");
            }
        }

        private ObservableCollection<int> minuteSecondArr;
        public ObservableCollection<int> MinuteSecondArr
        {
            get
            {
                return minuteSecondArr;
            }
            set
            {
                minuteSecondArr = value;
                RaisePropertyChanged("MinuteSecondArr");
            }
        }

        private ObservableCollection<int> hourArr;
        public ObservableCollection<int> HourArr
        {
            get
            {
                return hourArr;
            }
            set
            {
                hourArr = value;
                RaisePropertyChanged("HourArr");
            }
        }

        private ObservableCollection<int> monthArr;
        public ObservableCollection<int> MonthArr
        {
            get
            {
                return monthArr;
            }
            set
            {
                monthArr = value;
                RaisePropertyChanged("MonthArr");
            }
        }

        private ObservableCollection<int> yearArr;
        public ObservableCollection<int> YearArr
        {
            get
            {
                return yearArr;
            }
            set
            {
                yearArr = value;
                RaisePropertyChanged("YearArr");
            }
        }

        private readonly ICommand goPreviousMonthCmd;
        public ICommand GoPreviousMonthCmd
        {
            get
            {
                return goPreviousMonthCmd;
            }
        }

        private readonly ICommand goNextMonthCmd;
        public ICommand GoNextMonthCmd
        {
            get
            {
                return goNextMonthCmd;
            }
        }

        private readonly ICommand selectDayCmd;
        public ICommand SelectDayCmd
        {
            get
            {
                return selectDayCmd;
            }
        }

        private int monthValue = DateTime.Now.Month;
        public int MonthValue
        {
            get
            {
                return monthValue;
            }
            set
            {
                if (monthValue != value)
                {
                    monthValue = value;
                    initMonthDays(ContextOwner.Input);
                }

                RaisePropertyChanged("MonthValue");
            }
        }

        private int yearValue = DateTime.Now.Year;
        public int YearValue
        {
            get
            {
                return yearValue;
            }
            set
            {
                if (yearValue != value)
                {
                    yearValue = value;
                    initMonthDays(ContextOwner.Input);
                }

                RaisePropertyChanged("YearValue");
            }
        }

        private int hourValue = DateTime.Now.Hour;
        public int HourValue
        {
            get
            {
                return hourValue;
            }
            set
            {
                hourValue = value;
                updateTimeOfInput();
                RaisePropertyChanged("HourValue");
            }
        }

        public ObservableCollection<int> DaysOfWeek
        {
            get
            {
                return new ObservableCollection<int>(new int[7]{ 1, 2, 3, 4, 5, 6, 7 });
            }
        }

        private int minuteValue = DateTime.Now.Minute;
        public int MinuteValue
        {
            get
            {
                return minuteValue;
            }
            set
            {
                minuteValue = value;
                updateTimeOfInput();
                RaisePropertyChanged("MinuteValue");
            }
        }

        private int secondValue = DateTime.Now.Second;
        public int SecondValue
        {
            get
            {
                return secondValue;
            }
            set
            {
                secondValue = value;
                updateTimeOfInput();
                RaisePropertyChanged("SecondValue");
            }
        }

        private void updateTimeOfInput()
        {
            if (null != this.ContextOwner.Input)
            {
                DateTime dtBuff = (DateTime)this.ContextOwner.Input;

                this.ContextOwner.Input = new DateTime(dtBuff.Year, dtBuff.Month, dtBuff.Day,
                HourValue, MinuteValue, SecondValue);
            }
        }

        private bool dateTimePopupIsOpen;
        public bool DateTimePopupIsOpen
        {
            get
            {
                return dateTimePopupIsOpen;
            }
            set
            {
                dateTimePopupIsOpen = value;
                if (dateTimePopupIsOpen)
                {
                    initDateTimePopup(ContextOwner.Input);
                }
                RaisePropertyChanged("DateTimePopupIsOpen");
            }
        }

        private CalendarDayButton selectedCalDay;

        public CrsTextBoxDateTimePopupVM(CrsTextBox ContextOwner)
        {
            this.ContextOwner = ContextOwner;

            goPreviousMonthCmd = new ICommandImp(goPreviousMonthCmd_handler);
            goNextMonthCmd = new ICommandImp(goNextMonthCmd_handler);
            selectDayCmd = new ICommandImp(selectDayCmd_handler);

            if (this.ContextOwner.InputType.Equals(CrsTextBox.INPUTTYPES.DATETIME))
                initDateTimePopup(ContextOwner.Input);
        }

        private void selectDayCmd_handler(object obj)
        {
            if (selectedCalDay.Date.Day == DateTime.Now.Day && selectedCalDay.Date.Month == DateTime.Now.Month && selectedCalDay.Date.Year == DateTime.Now.Year)
                selectedCalDay.Background = new SolidColorBrush(Color.FromArgb(0x48, 0, 0, 0));
            else
                selectedCalDay.Background = new SolidColorBrush(Colors.Transparent);

            selectedCalDay = (CalendarDayButton)obj;

            selectedCalDay.Background = new SolidColorBrush(Color.FromArgb(0x48, 0xFF, 0xFF, 0xFF));

            this.ContextOwner.Input = new DateTime(selectedCalDay.Date.Year, selectedCalDay.Date.Month, selectedCalDay.Date.Day,
                HourValue, MinuteValue, SecondValue);
        }

        private void goPreviousMonthCmd_handler(object obj)
        {
            DateTime dtBuff = selectedCalDay.Date;
            dtBuff = dtBuff.AddMonths(-1);
            if (dtBuff >= (new DateTime(1900, 1, 1)))
            {
                monthValue = dtBuff.Month;
                yearValue = dtBuff.Year;
                RaisePropertyChanged("MonthValue");
                RaisePropertyChanged("YearValue");
                initMonthDays(dtBuff);
            }
        }

        private void goNextMonthCmd_handler(object obj)
        {
            DateTime dtBuff = selectedCalDay.Date;
            dtBuff = dtBuff.AddMonths(1);
            if (dtBuff <= (new DateTime(2100, 12, 1)))
            {
                monthValue = dtBuff.Month;
                yearValue = dtBuff.Year;
                RaisePropertyChanged("MonthValue");
                RaisePropertyChanged("YearValue");
                initMonthDays(dtBuff);
            }
        }

        private void initDateTimePopup(object selectedDateTimeObj)
        {
            DateTime selectedDateTime = null == selectedDateTimeObj ? DateTime.Now : (DateTime)selectedDateTimeObj;

            YearArr = new ObservableCollection<int>();
            for (int i = 1900; i <= 2100; ++i)
                YearArr.Add(i);

            YearValue = selectedDateTime.Year;

            MonthArr = new ObservableCollection<int>();
            for (int i = 1; i <= 12; ++i)
                MonthArr.Add(i);

            MonthValue = selectedDateTime.Month;

            HourArr = new ObservableCollection<int>();

            for (int i = 0; i < 24; ++i)
                HourArr.Add(i);

            HourValue = selectedDateTime.Hour;

            MinuteSecondArr = new ObservableCollection<int>();
            for (int i = 0; i < 60; ++i)
                MinuteSecondArr.Add(i);

            MinuteValue = selectedDateTime.Minute;
            SecondValue = selectedDateTime.Second;

            initMonthDays(selectedDateTime);
        }

        private void initMonthDays(object selectedDateTimeObj)
        {
            DateTime selectedDateTime = null == selectedDateTimeObj ? DateTime.Now : (DateTime)selectedDateTimeObj;
            VisibleDays = new ObservableCollection<ObservableCollection<CalendarDayButton>>();

            DateTime currentMonth = new DateTime(YearValue, MonthValue, 1);

            while (DayOfWeek.Monday != currentMonth.DayOfWeek)
            {
                currentMonth = currentMonth.AddDays(-1);
            }

            for (int i = 1; i <= 6; ++i)
            {
                ObservableCollection<CalendarDayButton> weekBuff = new ObservableCollection<CalendarDayButton>();
                for (int j = 1; j <= 7; ++j)
                {
                    CalendarDayButton dayButton = new CalendarDayButton();
                    dayButton.Date = currentMonth;

                    dayButton.Foreground = currentMonth.Month == MonthValue ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Color.FromArgb(0xff, 0xcc, 0xcc, 0xcc));

                    dayButton.Background = new SolidColorBrush(Colors.Transparent);

                    if (currentMonth.Day == DateTime.Now.Day && currentMonth.Month == DateTime.Now.Month && currentMonth.Year == DateTime.Now.Year)
                    {
                        dayButton.Background = new SolidColorBrush(Color.FromArgb(0x48, 0, 0, 0));
                    }
                    if (selectedDateTime.Day == currentMonth.Day && selectedDateTime.Month == currentMonth.Month)
                    {
                        dayButton.Background = new SolidColorBrush(Color.FromArgb(0x48, 0xFF, 0xFF, 0xFF));
                        selectedCalDay = dayButton;
                    }

                    weekBuff.Add(dayButton);

                    currentMonth = currentMonth.AddDays(1);
                }
                VisibleDays.Add(weekBuff);
            }
        }
    }
}
