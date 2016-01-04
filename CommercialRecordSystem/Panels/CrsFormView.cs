using CommercialRecordSystem.Constants;
using CommercialRecordSystem.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.Panels
{
    class CrsFormView : CRSItemsPanel
    {
        #region Properties
        private List<CrsTextBox> inputElements = new List<CrsTextBox>();

        private int childrenCountBuff = 0;
        #endregion

        public CrsFormView()
        {
            
        }

        protected override void loadChildren()
        {
            base.loadChildren();
            if (Children.Count != childrenCountBuff)
            {
                determineInputElements(this.Children);
                adjustIconsArea();
                childrenCountBuff = Children.Count;
            }
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
            if (element is CrsButtonIntf)
            {
                CrsButtonIntf buttonBuff = element as CrsButtonIntf;

                if (buttonBuff.Validation)
                    buttonBuff.setClickHandler(assignButtonCanExecute);
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

            (sender as CrsButtonIntf).setCommandCanExecute(validated);
        }
    }
}
