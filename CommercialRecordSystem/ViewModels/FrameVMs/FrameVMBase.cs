using CommercialRecordSystem.Common;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    abstract class FrameVMBase : VMBase
    {
        #region Properties
        private FrameNavigation navigation;
        public FrameNavigation Navigation
        {
            get
            {
                return navigation;
            }
        }
        #endregion

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
        protected virtual void goBackCmdHandler(object parameter)
        {
            Navigation.GoBack();
        }
        #endregion

        public FrameVMBase(Frame frame, FrameNavigation navigation)
        {
            this.navigation = navigation;
            goBackCmd = new ICommandImp(goBackCmdHandler);
        }
    }
}
