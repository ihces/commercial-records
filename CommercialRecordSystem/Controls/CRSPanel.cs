using CommercialRecordSystem.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace CommercialRecordSystem.Controls
{
    public class CRSPanel : Panel
    {

        #region Properties
        private List<CRSTextBox> inputElements = null;
        #endregion
        public CRSPanel()
        {
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            inputElements = new List<CRSTextBox>();
            foreach (UIElement element in this.Children)
            {
                if (element is Panel)
                {
                    Panel panel = (Panel)element;
                    foreach (UIElement panelElement in panel.Children)
                    {
                        addInputElements(panelElement);
                    }
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
                    buttonBuff.Click += new RoutedEventHandler(Button_OnClickHandler);
            }
        }

        private void Button_OnClickHandler(object sender, RoutedEventArgs e)
        { 
            bool validated = true;
            foreach (CRSTextBox element in inputElements)
            {
                if (element.Required)
                {
                    element.Validate();
                    if (!element.IsValid)
                    {
                        validated = false;
                    }
                }
            }

            (sender as CSRButton).Command.CanExecute(validated);
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement element in this.Children)
            {
                element.Measure(availableSize);
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Children.Count > 0)
            {
                double x = 0.0f;
                double y = 0.0f;

                foreach (UIElement element in this.Children)
                {
                    element.Arrange(new Rect(x, y, finalSize.Width, element.DesiredSize.Height));
                    y += element.DesiredSize.Height + 10.0f;
                }
            }

            return finalSize;
        }
    }
}
