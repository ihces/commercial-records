using CommercialRecords.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace CommercialRecords.Panels
{
    class CRSRadioButtonPanel : CRSItemsPanel
    {
        #region Properties
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
                typeof(CRSRadioButtonPanel),
                new PropertyMetadata(-1, CheckedIndexChangedHandler)
            );
        #endregion

        #region CheckedItem
        public object CheckedItem
        {
            get
            {
                return (object)GetValue(CheckedItemProperty);
            }
            set
            {
                SetValue(CheckedItemProperty, value);
            }
        }

        public static readonly DependencyProperty CheckedItemProperty =
            DependencyProperty.Register(
                "CheckedItem",
                typeof(object),
                typeof(CRSRadioButtonPanel),
                new PropertyMetadata(-1, CheckedItemChangedHandler)
            );
        #endregion

        private bool checkedChangedExternally = true;
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
        { }

        protected override void loadChildren()
        {
            base.loadChildren();

            foreach (RadioButton item in this.Children)
                item.Tapped += radioButtonChecked;

            checkedIndexChangedHandler(this);
            if (itemSourceDefined)
                checkedItemChangedHandler(this);
        }

        void radioButtonChecked(object sender, RoutedEventArgs e)
        {
            checkedChangedExternally = false;
            int index = this.Children.IndexOf(((RadioButton)sender));
            if (itemSourceDefined)
                CheckedItem = Items[index];
            else
                CheckedIndex = index;
        }

        private static void CheckedIndexChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            checkedIndexChangedHandler((CRSRadioButtonPanel)obj); 
        }

        private static void CheckedItemChangedHandler(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            CRSRadioButtonPanel panel = (CRSRadioButtonPanel)obj;
            if (panel.itemSourceDefined)
                checkedItemChangedHandler(panel);
        }

        private static void checkedItemChangedHandler(CRSRadioButtonPanel panel)
        {
            int checkedIndex = -1;

            if (panel.Children.Count > 0 && -1 < (checkedIndex = panel.Items.IndexOf(panel.CheckedItem)))
                panel.CheckedIndex = checkedIndex;
        }

        private static void checkedIndexChangedHandler(CRSRadioButtonPanel panel)
        {
            if (panel.Children.Count > 0)
            {
                if (panel.checkedChangedExternally)
                {
                    if (0 > panel.CheckedIndex)
                        panel.CheckedIndex = 0;
                    if (panel.Children.Count < panel.CheckedIndex)
                        panel.CheckedIndex = panel.Children.Count - 1;

                    if (0 < panel.Children.Count)
                        ((RadioButton)panel.Children[panel.CheckedIndex]).IsChecked = true;
                }

                if (null != panel.CheckedCommand && -1 < panel.CheckedIndex)
                    panel.CheckedCommand.Execute(panel.Children[panel.CheckedIndex]);

                //reset
                panel.checkedChangedExternally = true;
            }
        }
    }
}
