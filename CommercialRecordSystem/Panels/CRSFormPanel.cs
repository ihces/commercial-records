using CommercialRecordSystem.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Panels
{
    class CRSFormPanel : CRSPanel
    {
        #region Properties
        private List<CRSTextBox> inputElements = null;
        #endregion
        public CRSFormPanel()
        {
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            inputElements = new List<CRSTextBox>();
            determineInputElements(this.Children);
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
            if (elementType.Equals(typeof(CRSTextBox)))
            {
                inputElements.Add(element as CRSTextBox);
            }
            if (elementType.Equals(typeof(CSRButton)))
            {
                CSRButton buttonBuff = element as CSRButton;
                if (buttonBuff.Validation)
                    buttonBuff.Click += new RoutedEventHandler(assignButtonCanExecute);
            }
        }

        private void assignButtonCanExecute(object sender, RoutedEventArgs e)
        { 
            bool validated = true;
            foreach (CRSTextBox element in inputElements)
            {
                if (element.Required || element.Validate)
                {
                    element.CheckIsValid(CRSTextBox.SUBMIT_BUTTON);
                    if (!element.IsValid)
                    {
                        validated = false;
                    }
                }
            }

            if (validated)
            {
                foreach (CRSTextBox element in inputElements)
                {
                    element.AnyClickHandled = false;
                }
            }

            (sender as CSRButton).Command.CanExecute(validated);
        }
    }
}
