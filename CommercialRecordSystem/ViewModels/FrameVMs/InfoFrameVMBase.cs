

using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using CommercialRecordSystem.Views;
using System;
using System.IO;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
namespace CommercialRecordSystem.ViewModels.FrameVMs
{
    class InfoFrameVMBase<E, F>: FrameVMBase where E : InfoDataVMBase<F>, new() where F : InfoModelBase, new()
    {
        #region Properties

        private string infoTitle;
        public string InfoTitle
        {
            get
            {
                return infoTitle;
            }
            set
            {
                infoTitle = value;
                RaisePropertyChanged("InfoTitle");
            }
        }

        private Boolean showImageLogo = true;
        public Boolean ShowImageLogo
        {
            get
            {
                return showImageLogo;
            }
            set
            {
                showImageLogo = value;
                RaisePropertyChanged("ShowImageLogo");
            }
        }

        private string infoName;
        private double aspectRatio;

        private E currentInfo = new E();
        public E CurrentInfo
        {
            get
            {
                return currentInfo;
            }
            set
            {
                currentInfo = value;
                RaisePropertyChanged("CurrentInfo");
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
        private readonly ICommand saveInfoCmd = null;
        public ICommand SaveInfoCmd
        {
            get
            {
                return saveInfoCmd;
            }
        }

        private readonly ICommand delInfoCmd = null;
        public ICommand DelInfoCmd
        {
            get
            {
                return delInfoCmd;
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
        private void saveInfoCmdHandler(object parameter)
        {
            LoadingVisibility = Visibility.Visible;

            int result = CurrentInfo.save();
            string message = null;
            if (result > 0)
            {
                if (0 == CurrentInfo.Id) // new Info
                {
                    DelButtonCanEnable = true;
                    CurrentInfo.Id = result;
                }
                CurrentInfo.Dirty = false;
                message = infoName + " Bilgileri Başarı ile Kaydedildi.";
            }
            else
                message = infoName + " Bilgileri Kaydedilemedi.";

            var messageDialog = new MessageDialog(message, "Kaydetme İşlemi");

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

        private void delInfoCmdHandler(object parameter)
        {
            var messageDialog = new MessageDialog(infoName + " Kaydı Kalıcı Olarak Silinecektir. Emin misiniz?", "Kayıt Silme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Hayır", null));
            messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.DelInfoCommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
        }

        private void DelInfoCommandInvokedHandler(IUICommand command)
        {
            CurrentInfo.delete();
            Navigation.GoBack();
        }

        private async void loadPhotoViaBrowserCmdHandler(object parameter)
        {
            object [] imagePickData = new object[2];
            imagePickData[0] = CurrentInfo.ImageSourceFolder;
            imagePickData[1] = aspectRatio;
            Navigation.Navigate<ImagePicker>(imagePickData);
        }

        private async void capturePhotoFromCamCmdHandler(object parameter)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.PhotoSettings.CroppedAspectRatio = new Size(240, 240*aspectRatio);

            StorageFile capturedPhoto = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (capturedPhoto != null)
            {
                copyImgToLocalFolderNShow(capturedPhoto);
            }
        }

        private async void copyImgToLocalFolderNShow(StorageFile file)
        {
            string fileName = "photo_" + DateTime.Now.Ticks + ".jpg";
            await file.CopyAsync(CurrentInfo.ImageSourceFolder, fileName);

            CurrentInfo.ImageFileName = fileName;

            // show photo
            ShowImageLogo = false;
            CurrentInfo.ImageFileSource = new Uri(Path.Combine(CurrentInfo.ImageSourceFolder.Path, fileName));
        }
        #endregion

        public InfoFrameVMBase(FrameNavigation navigation, string infoName, double aspectRatio)
            : base(navigation)
        {
            this.infoName = infoName;
            this.InfoTitle = "Yeni " + infoName;
            this.aspectRatio = aspectRatio;

            saveInfoCmd = new ICommandImp(saveInfoCmdHandler);
            delInfoCmd = new ICommandImp(delInfoCmdHandler);
            loadPhotoViaFileBrowserCmd = new ICommandImp(loadPhotoViaBrowserCmdHandler);
            capturePhotoFromCamCmd = new ICommandImp(capturePhotoFromCamCmdHandler);

            if (null != navigation.Forward && navigation.Forward.Is<ImagePicker>())
            {
                if (null != navigation.Message)
                {
                    CurrentInfo.ImageFileName = (string)navigation.Message;
                    ShowImageLogo = false;
                    CurrentInfo.ImageFileSource = new Uri(Path.Combine(CurrentInfo.ImageSourceFolder.Path, (string)navigation.Message));
                }
            }
            else if (null != navigation.Message)
            {
                DelButtonCanEnable = true;

                this.InfoTitle = "Kayıtlı " + infoName;
                CurrentInfo.get((int)navigation.Message);

                if (!string.IsNullOrWhiteSpace(CurrentInfo.ImageFileName))
                    ShowImageLogo = false;
            }
        }
    }
}
