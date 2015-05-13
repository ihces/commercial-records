using System;
using System.Linq;
using System.Windows.Input;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Storage.Pickers;
using System.IO;
using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.Views;
using CommercialRecordSystem.ViewModels.FrameVMs;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerInfoFrameVM : InfoFrameVMBase<CustomerVM, Customer>
    {
        public CustomerInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Müşteri", 1.25)
        {
            
        }
    }
}
