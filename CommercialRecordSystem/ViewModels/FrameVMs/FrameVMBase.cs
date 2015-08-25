using CommercialRecordSystem.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CommercialRecordSystem.ViewModels
{
    abstract class FrameVMBase : VMBase
    {
        #region Properties
        protected FrameNavigation navigation;
        public FrameNavigation Navigation
        {
            get
            {
                return navigation;
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                pageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        private bool showPageTitle = true;
        public bool ShowPageTitle
        {
            get
            {
                return showPageTitle;
            }
            set
            {
                showPageTitle = value;
                RaisePropertyChanged("ShowPageTitle");
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

        public FrameVMBase(FrameNavigation navigation)
        {
            this.navigation = navigation;
            this.navigation.Navigated += navigation_Navigated;

            if (!navigation.NavigateType.Equals(FrameNavigation.NAVIGATE_TYPE.NAVIGATE))
            {
                if (null != navigation.Forward && null != navigation.Forward.Back &&
                    navigation.Forward.Back.PageFrame.GetType().Equals(this.Navigation.PageFrame.GetType()))
                {
                    initPrevData(navigation.Forward.Back.PropertyData);
                }
                else if (null != navigation.Back && null != navigation.Back.Forward &&
                    navigation.Back.Forward.PageFrame.GetType().Equals(this.Navigation.PageFrame.GetType()))
                {
                    initPrevData(navigation.Back.Forward.PropertyData);
                }
            }
            goBackCmd = new ICommandImp(goBackCmdHandler);
        }

        private void navigation_Navigated(object sender, EventArgs e)
        {
            recordInputData();
        }

        private void recordInputData()
        {
            IEnumerable<PropertyInfo> properties = this.GetType().GetRuntimeProperties();
            this.navigation.PropertyData = new Dictionary<string, object>();
            foreach (PropertyInfo property in properties)
            {
                if ("Navigation" != property.Name && !(property.PropertyType.Equals(typeof(ICommand))))
                    this.navigation.PropertyData.Add(property.Name, property.GetValue(this));
            }

        }

        private void initPrevData(Dictionary<string, object> dataBuff)
        {
            PropertyInfo propertyBuff = null;
            foreach (string key in dataBuff.Keys) 
            {
                propertyBuff = this.GetType().GetRuntimeProperty(key);
                if (propertyBuff.CanWrite)
                    propertyBuff.SetValue(this, dataBuff[key]);
            }
        }
    }
}
