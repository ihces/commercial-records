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
    class CustomerInfoFrameVM : FrameVMBase
    {
        #region Properties

        private CustomerVM currentCustomer = new CustomerVM();
        public CustomerVM CurrentCustomer
        {
            get
            {
                return currentCustomer;
            }
            set
            {
                currentCustomer = value;
                RaisePropertyChanged("CurrentCustomer");
            }
        }

        private bool delButtonCanEnable = false;
        public bool DelButtonCanEnable
        {
            get
            {
                return delButtonCanEnable;
            }
            set
            {
                delButtonCanEnable = value;
                RaisePropertyChanged("DelButtonCanEnable");
            }
        }

        private Visibility loadingVisibility = Visibility.Collapsed;
        public Visibility LoadingVisibility
        {
            get
            {
                return loadingVisibility;
            }
            set
            {
                loadingVisibility = value;
                RaisePropertyChanged("LoadingVisibility");
            }
        }
        #endregion

        #region Commands
        private readonly ICommand saveCustomerCmd = null;
        public ICommand SaveCustomerCmd
        {
            get
            {
                return saveCustomerCmd;
            }
        }

        private readonly ICommand delCustomerCmd = null;
        public ICommand DelCustomerCmd
        {
            get
            {
                return delCustomerCmd;
            }
        }

        private readonly ICommand capturePhotoFromCamCmd = null;
        public ICommand CapturePhotoFromCamCmd
        {
            get
            {
                return capturePhotoFromCamCmd;
            }
        }

        private readonly ICommand loadPhotoViaFileBrowserCmd = null;

        public ICommand LoadPhotoViaFileBrowserCmd
        { 
            get
            {
                return loadPhotoViaFileBrowserCmd;
            }
        }
        #endregion

        #region Command Handlers
        private void saveCustomerCmdHandler(object parameter)
        {
            LoadingVisibility = Visibility.Visible;


            int result = CurrentCustomer.save();
            string message = null;
            if (result > 0)
            {
                if (0 == CurrentCustomer.Id) // new customer
                {
                    DelButtonCanEnable = true;
                    CurrentCustomer.Id = result;
                }
                CurrentCustomer.Dirty = false;
                message = "Müşteri Bilgileri Başarı ile Kaydedildi.";
            }
            else
                message = "Müşteri Bilgileri kaydedilemedi.";

            var messageDialog = new MessageDialog(message, "Müşteri Bilgilerini Kaydetme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Tamam", null));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
            //LoadingVisibility = Visibility.Collapsed;
        }

        private void delCustomerCmdHandler(object parameter)
        {
            var messageDialog = new MessageDialog("Seçili müşteri kalıcı olarak silmek istediğinizden emin misiniz?", "Müşteri Silme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Hayır", null));
            messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.DelCustCommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
        }

        private void DelCustCommandInvokedHandler(IUICommand command)
        {
            CurrentCustomer.delete();
            Navigation.GoBack();
        }

        private async void loadPhotoViaBrowserCmdHandler(object parameter)
        {
            Navigation.Navigate<ImagePicker>();
        }

        private async void capturePhotoFromCamCmdHandler(object parameter)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            Size aspectRatio = new Size(240, 300);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile capturedPhoto = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (capturedPhoto != null)
            {
                copyImgToLocalFolderNShow(capturedPhoto);
            }
        }

        private async void copyImgToLocalFolderNShow(StorageFile file)
        {
            string fileName = "photo_" + DateTime.Now.Ticks + ".jpg";

            CurrentCustomer.ProfilePhotoFileName = fileName;

            await file.CopyAsync(App.ProfileImgFolder, fileName);
            // show photo
            CurrentCustomer.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, fileName));
        }
        #endregion

        public CustomerInfoFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            saveCustomerCmd = new ICommandImp(saveCustomerCmdHandler);
            delCustomerCmd = new ICommandImp(delCustomerCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);

            if (null != navigation.Forward && navigation.Forward.Is<ImagePicker>())
            {
                if (null != navigation.Message)
                {
                    CurrentCustomer.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, (string)navigation.Message));
                }
            }
            else if (null != navigation.Message) 
            {
                DelButtonCanEnable = true;
                CurrentCustomer.get((int)navigation.Message);
            }
        }
    }
}
