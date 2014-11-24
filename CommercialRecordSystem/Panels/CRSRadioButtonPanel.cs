using CommercialRecordSystem.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Panels
{
    class CRSRadioButtonPanel : CRSPanel
    {
        #region Properties
        private List<string> itemList = new List<string>();

        #region CheckedIndex
        public int CheckedIndex
        {
            get
            {
                return (int)GetValue(CheckedIndexProperty);
            }
            set
            {
                SetValue(CheckedIndexProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedIndexProperty =
            DependencyProperty.Register(
                "CheckedIndex",
                typeof(int),
                typeof(CRSPanel),
                new PropertyMetadata(-1, CheckedIndexHandler)
            );
        #endregion

        private bool checkedIndexChangedExternally = true;
        #endregion

        #region CheckedCommand
        public ICommand CheckedCommand
        {
            get
            {
                return (ICommand)GetValue(CheckedCommandProperty);
            }
            set
            {
                SetValue(CheckedCommandProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedCommandProperty =
            DependencyProperty.Register(
                "CheckedCommand",
                typeof(ICommand),
                typeof(CRSRadioButtonPanel),
                new PropertyMetadata(null, null)
            );
        #endregion

        public CRSRadioButtonPanel()
        {
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (RadioButton item in this.Children)
            {
                itemList.Add(item.Name); 
                item.Checked += radioButtonChecked;
                
                if ((bool)item.IsChecked)
                    CheckedIndex = count;

                count++;
            }
        }

        void radioButtonChecked(object sender, RoutedEventArgs e)
        {
            checkedIndexChangedExternally = false;
            CheckedIndex = itemList.IndexOf(((RadioButton)sender).Name);
        }

        private static void CheckedIndexHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CRSRadioButtonPanel panel = (CRSRadioButtonPanel)obj;

            if (panel.checkedIndexChangedExternally)
            { 
                if (0 > panel.CheckedIndex)
                    panel.CheckedIndex = 0;
                if (panel.Children.Count < panel.CheckedIndex)
                    panel.CheckedIndex = panel.Children.Count - 1;

                if (0 < panel.Children.Count)
                    ((RadioButton)panel.Children[panel.CheckedIndex]).IsChecked = true;
            }

            if (null != panel.CheckedCommand && -1 < panel.CheckedIndex)
                panel.CheckedCommand.Execute(panel.itemList[panel.CheckedIndex]);

            //reset
            panel.checkedIndexChangedExternally = true;
        }
    }
}
