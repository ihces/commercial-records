using CommercialRecordSystem.Common;
using CommercialRecordSystem.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CommercialRecordSystem.Views
{
    public class ViewBase : Page
    {
        private readonly Type viewModel;
        public ViewBase(Type viewModel) : base()
        {
            this.viewModel = viewModel;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FrameNavigation navigationInfo = null;
            if (null == e.Parameter)
                navigationInfo = new FrameNavigation(this.GetType());
            else
                navigationInfo = (FrameNavigation)e.Parameter;

            navigationInfo.PageFrame = this.Frame;
            this.DataContext = (FrameVMBase)Activator.CreateInstance(viewModel, this.Frame, navigationInfo);
        }
    }
}
