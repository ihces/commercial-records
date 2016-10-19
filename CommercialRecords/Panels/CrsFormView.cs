using CommercialRecords.Constants;
using CommercialRecords.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace CommercialRecords.Panels
{
    class CrsFormView : CRSItemsPanel
    {
        #region Properties
        #region CaptureEnter
        public bool CaptureEnter
        {
            get
            {
                return (bool)GetValue(CaptureEnterProperty);
            }
            set
            {
                SetValue(CaptureEnterProperty, value);
            }
        }

        public static readonly DependencyProperty CaptureEnterProperty =
            DependencyProperty.Register(
                "CaptureEnter",
                typeof(bool),
                typeof(CRSItemsPanel),
                new PropertyMetadata(false)
            );
        #endregion
        private List<CrsInput> inputElements = new List<CrsInput>();
        private Button submitButton = null;

        private int childrenCountBuff = 0;
        #endregion

        public CrsFormView()
        {

        }

        public void cleanForm()
        {
            foreach (CrsInput input in inputElements)
                input.reset();
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
                foreach (CrsInput textbox in inputElements)
                {
                    if (minHeight > textbox.Height)
                        minHeight = textbox.Height;
                }

                foreach (CrsInput textbox in inputElements)
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
            if (element is CrsInput)
            {
                CrsInput inputBuff = element as CrsInput;
                inputElements.Add(inputBuff);
                //inputBuff.Input;

                if (CaptureEnter)
                    inputBuff.setKeyUpHandler(inputKeyDownHandler);
            }
            if (element is CrsButtonIntf)
            {
                CrsButtonIntf buttonBuff = element as CrsButtonIntf;

                if (buttonBuff.Validation)
                {
                    submitButton = element as Button;
                    buttonBuff.setClickHandler(assignButtonCanExecute);
                }
            }
        }

        private void assignButtonCanExecute(object sender, RoutedEventArgs e)
        {
            (sender as CrsButtonIntf).setCommandCanExecute(validateInputs());

            for (int i = 0; i < inputElements.Count; ++i)
            {
                if (inputElements[i].Visibility.Equals(Visibility.Visible) &&
                    !inputElements[i].ReadOnly &&
                    !inputElements[i].InputType.Equals(CrsInput.INPUTTYPES.MULTISELECT))
                {
                    inputElements[i].SetFocusState(FocusState.Keyboard);
                    break;
                }
            }
        }

        private bool validateInputs()
        {
            bool validated = true;
            foreach (CrsInput element in inputElements)
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
                foreach (CrsInput element in inputElements)
                {
                    element.AnyClickHandled = false;
                }
            }

            return validated;
        }

        private void inputKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.Equals(Windows.System.VirtualKey.Enter))
            {
                Control activeControl = (Control)sender;

                int inputBuffOrder = -1;
                for (int i = 0; i < inputElements.Count; ++i)
                {
                    if (-1 == inputBuffOrder)
                    {
                        if (inputElements[i].getActiveControl() == activeControl)
                        {
                            inputBuffOrder = i;
                            continue;
                        }
                    }
                    else if (inputElements[i].Visibility.Equals(Visibility.Visible) &&
                        !inputElements[i].ReadOnly && 
                        !inputElements[i].InputType.Equals(CrsInput.INPUTTYPES.MULTISELECT))
                    {
                        inputElements[i].SetFocusState(FocusState.Keyboard);
                        break;
                    }
                }

                if (-1 != inputBuffOrder && !inputElements[inputBuffOrder].GetFocusState().Equals(FocusState.Unfocused))
                {
                    ButtonAutomationPeer peer = new ButtonAutomationPeer(submitButton);

                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    invokeProv.Invoke();
                }
            }
        }
    }
}
