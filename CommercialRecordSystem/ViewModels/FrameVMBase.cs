using System;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    abstract class FrameVMBase : VMBase
    {
        private readonly Frame pageFrame;

        public FrameVMBase(Frame frame) 
        {
            pageFrame = frame;
        }

        public void GoBackFrame()
        {
            pageFrame.GoBack();
        }

        public void GoForward()
        {
            pageFrame.GoForward();
        }

        public bool Navigate<T>(object parameter = null)
        {
            var type = typeof(T);

            return Navigate(type, parameter);
        }

        public bool Navigate(Type source, object parameter = null)
        {
            return pageFrame.Navigate(source, parameter);
        }
    }
}
