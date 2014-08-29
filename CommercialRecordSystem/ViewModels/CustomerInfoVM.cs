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

namespace CommercialRecordSystem.ViewModels
{
    class CustomerInfoVM : VMBase
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
            Customer newCustomer = new Customer();
            newCustomer.Name = CurrentCustomer.Name;
            newCustomer.Surname = CurrentCustomer.Surname;
            newCustomer.Sincerity = CurrentCustomer.Sincerity;
            newCustomer.Address = CurrentCustomer.Address;
            newCustomer.PhoneNumber = CurrentCustomer.PhoneNumber;
            newCustomer.MobileNumber = CurrentCustomer.MobileNumber;
            newCustomer.ProfilePhotoFileName = CurrentCustomer.ProfilePhotoFileName;
            newCustomer.LastTransactDate = CurrentCustomer.LastTransactDate;
            newCustomer.AccountCost = CurrentCustomer.AccountCost;
            newCustomer.CreatedDate = CurrentCustomer.CreatedDate;
            newCustomer.ModifiedDate = CurrentCustomer.ModifiedDate;

            saveCustomer(newCustomer);

            //LoadingVisibility = Visibility.Collapsed;
        }

        private void delCustomerCmdHandler(object parameter)
        {
            var messageDialog = new MessageDialog("Seçili müşteri kalıcı olarak silmek istediğinizden emin misiniz?", "Müşteri Silme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Hayır", null));
            messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            deleteCustomer();
        }

        private async void loadPhotoViaBrowserCmdHandler(object parameter)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.SettingsIdentifier = "PicturePicker";
            filePicker.CommitButtonText = "Seç";

            StorageFile selectedFile = await filePicker.PickSingleFileAsync();
            if (selectedFile != null)
            {
                copyImgToLocalFolderNShow(selectedFile);
            }
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

        public CustomerInfoVM()
        {
            saveCustomerCmd = new ICommandImp(saveCustomerCmdHandler);
            delCustomerCmd = new ICommandImp(delCustomerCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);
        }

        public CustomerInfoVM(int customerId)
        {
            CurrentCustomer = getCustomer(customerId);
        }

        protected CustomerVM getCustomer(int customerId) 
        {
            CustomerVM customer = new CustomerVM();
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var customerBuff = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();

                customer.Id = customerBuff.Id;
                customer.Name = customerBuff.Name;
                customer.Surname = customerBuff.Surname;
                customer.Address = customerBuff.Address;
                customer.PhoneNumber = customerBuff.PhoneNumber;
                customer.MobileNumber = customerBuff.MobileNumber;
                customer.ProfileImgSource = new Uri(Path.Combine(App.ProfileImgFolder.Path, customerBuff.ProfilePhotoFileName));
            }
            return customer;
        }

        public string saveCustomer(Customer customer)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                try
                {
                    Customer existingCustomer = (db.Table<Customer>().Where(
                        c => c.Id == customer.Id)).SingleOrDefault();

                    if (existingCustomer != null)
                    {

                        int success = db.Update(customer);
                    }
                    else
                    {
                        int success = db.Insert(customer);
                        result = "Success"; 
                    }
                }
                catch
                {
                    result = "This customer was not saved.";
                }
            }
            return result;
        }

        public string deleteCustomer()
        {
            LoadingVisibility = Visibility.Visible;

            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                var existingCustomer = (db.Table<Customer>().Where(
                    c => c.Id == CurrentCustomer.Id)).Single();

                if (db.Delete(existingCustomer) > 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "This project was not removed";
                }
            }
            LoadingVisibility = Visibility.Collapsed;
            return result;
        }
    }
}
