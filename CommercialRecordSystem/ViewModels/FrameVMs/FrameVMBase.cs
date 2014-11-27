using CommercialRecordSystem.Common;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    abstract class FrameVMBase : VMBase
    {
        private readonly Frame pageFrame;

        #region Commands
        private readonly ICommand goBackCmd = null;
        public ICommand GoBackCmd
        {
            get
            {
                return goBackCmd;
            }
        }
        #endregion

        #region Command Handlers
        protected void goBackCmdHandler(object parameter)
        {
            GoBackFrame();
        }
        #endregion

        public FrameVMBase(Frame frame) 
        {
            pageFrame = frame;
            goBackCmd = new ICommandImp(goBackCmdHandler);
        }

        public virtual void GoBackFrame()
        {
            pageFrame.GoBack();
        }

        public virtual void GoForward()
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
