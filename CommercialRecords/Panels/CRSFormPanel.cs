using CommercialRecords.Constants;
using CommercialRecords.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecords.Panels
{
    class CRSFormPanel : CRSPanel
    {
        #region Properties
        private List<CrsTextBox> inputElements = new List<CrsTextBox>();
        #endregion
        public CRSFormPanel()
        {
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            determineInputElements(this.Children);
            adjustIconsArea();
        }

        private void adjustIconsArea() 
        {
            if (inputElements.Count > 0)
            {
                double minHeight = inputElements[0].Height;
                foreach (CrsTextBox textbox in inputElements)
                {
                    if (minHeight > textbox.Height)
                        minHeight = textbox.Height;
                }

                foreach (CrsTextBox textbox in inputElements)
                {
                    textbox.IconWidth = minHeight;
                    textbox.IconFontSize = minHeight / 2;
                }
            }
        }

        private void determineInputElements(ICollection<UIElement> elements)
        {
            foreach (UIElement element in elements)
            {
                if (element is Panel)
                {
                    determineInputElements(((Panel)element).Children);
                }
                else if (element is Border)
                {
                    ICollection<UIElement> elementCollBuff = new Collection<UIElement>();
                    elementCollBuff.Add(((Border)element).Child);
                    determineInputElements(elementCollBuff);
                }
                else
                {
                    addInputElements(element);
                }
            }
        }

        private void addInputElements(UIElement element)
        {
            Type elementType = element.GetType();
            if (elementType.Equals(typeof(CrsTextBox)))
            {
                inputElements.Add(element as CrsTextBox);
            }
            if (elementType.Equals(typeof(CrsButton)))
            {
                CrsButton buttonBuff = element as CrsButton;
                if (buttonBuff.Validation)
                    buttonBuff.Click += new RoutedEventHandler(assignButtonCanExecute);
            }
        }

        private void assignButtonCanExecute(object sender, RoutedEventArgs e)
        { 
            bool validated = true;
            foreach (CrsTextBox element in inputElements)
            {
                if (Visibility.Visible == element.Visibility)
                {
                    element.CheckIsValid();
                    if (!element.IsValid)
                    {
                        if (!element.AnyClickHandled)
                            element.Background = ColorConsts.TEXTBOX_BACKGROUND_INVALID;
                        validated = false;
                    }
                }
            }

            if (validated)
            {
                foreach (CrsTextBox element in inputElements)
                {
                    element.AnyClickHandled = false;
                }
            }

            if (null != (sender as CrsButton).Command)
            (sender as CrsButton).Command.CanExecute(validated);
        }
    }
}
