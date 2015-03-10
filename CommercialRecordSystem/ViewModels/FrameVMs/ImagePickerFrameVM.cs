
using CommercialRecordSystem.Common;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
namespace CommercialRecordSystem.ViewModels.FrameVMs
{
    class ImagePickerFrameVM : FrameVMBase
    {
        #region Properties
        private Uri selectedImageSrc;
        public Uri SelectedImageSrc
        {
            get
            {
                return selectedImageSrc;
            }
            set
            {
                selectedImageSrc = value;
                RaisePropertyChanged("SelectedImageSrc");
            }
        }

        public const string TOP_LEFT_CORNER = "topLeftCorner";
        public const string TOP_RIGHT_CORNER = "topRightCorner";
        public const string BOTTOM_LEFT_CORNER = "bottomLeftCorner";
        public const string BOTTOM_RIGHT_CORNER = "bottomRightCorner";

        public string fileName = null;

        private double xMin; // left
        private double xMax; // right
        private double yMin; // top
        private double yMax; // bottom

        private string tempFileName;

        private StorageFile selectedImage;

        private double leftX;
        public double LeftX
        {
            get
            {
                return leftX;
            }
            set
            {
                
                leftX = value;
                RaisePropertyChanged("LeftX");
            }
        }

        private double rightX;
        public double RightX
        {
            get
            {
                return rightX;
            }
            set
            {
                rightX = value;
                RaisePropertyChanged("RightX");
            }
        }

        private double topY;
        public double TopY
        {
            get
            {
                return topY;
            }
            set
            {
                topY = value;
                RaisePropertyChanged("TopY");
            }
        }

        private double bottomY;
        public double BottomY
        {
            get
            {
                return bottomY;
            }
            set
            {
                bottomY = value;
                RaisePropertyChanged("BottomY");
            }
        }

        private Rect outerRect;
        public Rect OuterRect
        {
            get
            {
                return outerRect;
            }
            set
            {
                outerRect = value;
                RaisePropertyChanged("OuterRect");
            }
        }

        private Rect selectedRect;
        public Rect SelectedRect
        {
            get
            {
                return selectedRect;
            }
            set
            {
                selectedRect = value;
                RaisePropertyChanged("SelectedRect");
            }
        }

        public double MinEdgeSize { get; set; }

        public double AspectRatio { get; set; }

        #endregion
        #region Commands

        private readonly ICommand doneCmd;
        public ICommand DoneCmd
        {
            get
            {
                return doneCmd;
            }
        }

        private readonly ICommand selectedRegionManipulatedCmd;
        public ICommand SelectedRegionManipulatedCmd
        {
            get
            {
                return selectedRegionManipulatedCmd;
            }
        }
        #endregion
        #region Command Handlers
        public void changeCornerCoor(string cornerName, Point currentPosition, Point previousPosition)
        {
            double pointerChangeX = currentPosition.X - previousPosition.X;
            double pointerChangeY = currentPosition.Y - previousPosition.Y;
            
            if (cornerName.Equals(TOP_LEFT_CORNER))
                {
                    double width = SelectedRect.Width - pointerChangeX;
                    double height = SelectedRect.Height - pointerChangeY;

                    if (width * AspectRatio < height)
                    {
                        height = width * AspectRatio;
                    }
                    else if (width * AspectRatio > height)
                    {
                        width = height / AspectRatio;
                    }

                    pointerChangeX = width - SelectedRect.Width;
                    pointerChangeY = height - SelectedRect.Height;

                    double leftXBuff = previousPosition.X - pointerChangeX;
                    double topYBuff = previousPosition.Y - pointerChangeY;
                    if (leftXBuff + MinEdgeSize > RightX)
                        leftXBuff = RightX - MinEdgeSize;
                    if (topYBuff + MinEdgeSize * AspectRatio > BottomY)
                        topYBuff = BottomY - MinEdgeSize * AspectRatio;

                    LeftX = leftXBuff;
                    TopY = topYBuff;
                }

                if (cornerName.Equals(TOP_RIGHT_CORNER))
                {
                    double width = SelectedRect.Width + pointerChangeX;
                    double height = SelectedRect.Height - pointerChangeY;

                    if (width * AspectRatio < height)
                    {
                        height = width * AspectRatio;
                    }
                    else if (width * AspectRatio > height)
                    {
                        width = height / AspectRatio;
                    }

                    pointerChangeX = width - SelectedRect.Width;
                    pointerChangeY = height - SelectedRect.Height;

                    double rightXBuff = previousPosition.X + pointerChangeX;
                    double topYBuff = previousPosition.Y - pointerChangeY;
                    if (rightXBuff - MinEdgeSize < LeftX)
                        rightXBuff = LeftX + MinEdgeSize;
                    if (topYBuff + MinEdgeSize * AspectRatio > BottomY)
                        topYBuff = BottomY - MinEdgeSize * AspectRatio;

                    RightX = rightXBuff;
                    TopY = topYBuff;
                }

                if (cornerName.Equals(BOTTOM_LEFT_CORNER))
                {
                    double width = SelectedRect.Width - pointerChangeX;
                    double height = SelectedRect.Height + pointerChangeY;

                    if (width * AspectRatio < height)
                    {
                        height = width * AspectRatio;
                    }
                    else if (width * AspectRatio > height)
                    {
                        width = height / AspectRatio;
                    }

                    pointerChangeX = width - SelectedRect.Width;
                    pointerChangeY = height - SelectedRect.Height;

                    double leftXBuff = previousPosition.X - pointerChangeX;
                    double bottomYBuff = previousPosition.Y + pointerChangeY;
                    if (leftXBuff + MinEdgeSize > RightX)
                        leftXBuff = RightX - MinEdgeSize;
                    if (bottomYBuff - MinEdgeSize * AspectRatio < TopY)
                        bottomYBuff = TopY + MinEdgeSize * AspectRatio;

                    LeftX = leftXBuff;
                    BottomY = bottomYBuff;
                }

                if (cornerName.Equals(BOTTOM_RIGHT_CORNER))
                {
                    double width = SelectedRect.Width + pointerChangeX;
                    double height = SelectedRect.Height + pointerChangeY;

                    if (width * AspectRatio < height)
                    {
                        height = width * AspectRatio;
                    }
                    else if (width * AspectRatio > height)
                    {
                        width = height / AspectRatio;
                    }

                    pointerChangeX = width - SelectedRect.Width;
                    pointerChangeY = height - SelectedRect.Height;

                    double rightXBuff = previousPosition.X + pointerChangeX;
                    double bottomYBuff = previousPosition.Y + pointerChangeY;
                    if (rightXBuff - MinEdgeSize < LeftX)
                        rightXBuff = LeftX + MinEdgeSize;
                    if (bottomYBuff - MinEdgeSize * AspectRatio < TopY)
                        bottomYBuff = TopY + MinEdgeSize * AspectRatio;
                    RightX = rightXBuff;
                    BottomY = bottomYBuff;
                }

            UpdateSelectedRect();
        }

        private void selectedRegionManipulatedCmd_execute(object obj)
        {
            ManipulationDeltaRoutedEventArgs e = (ManipulationDeltaRoutedEventArgs)obj;

            double transX = e.Delta.Translation.X, 
                   transY = e.Delta.Translation.Y;

            if ((LeftX + transX) < 0)
            {
                transX = -LeftX;
            }
            if ((RightX + transX) > Navigation.PageFrame.ActualWidth)
            {
                transX = Navigation.PageFrame.ActualWidth - RightX;
            }
            if ((TopY + transY) < 0)
            {
                transY = -TopY;
            }
            if ((BottomY + transY) > Navigation.PageFrame.ActualHeight)
            {
                transY = Navigation.PageFrame.ActualHeight - BottomY;
            }

            LeftX += transX;
            RightX += transX;
            TopY += transY;
            BottomY += transY;

            UpdateSelectedRect();

            e.Handled = true;
        }

        private void ReSelectImageCommandInvokedHandler(IUICommand command)
        {
            Navigation.GoBack();
        }

        private void doneCmd_execute(object obj)
        {
            
            SaveCroppedBitmapAsync();
        }

        protected override void goBackCmdHandler(object parameter)
        {
            System.IOFile.Delete();
            Navigation.GoBack();
        }
        #endregion

        public ImagePickerFrameVM(FrameNavigation navigation)
            : base(navigation) 
        {
            doneCmd = new ICommandImp(doneCmd_execute);
            selectedRegionManipulatedCmd = new ICommandImp(selectedRegionManipulatedCmd_execute);
            AspectRatio = 1.25;
            MinEdgeSize = 240;
            pickImage();
        }

        private async void pickImage()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.SettingsIdentifier = "PicturePicker";
            filePicker.CommitButtonText = "Seç";

            selectedImage = await filePicker.PickSingleFileAsync();
            if (selectedImage != null)
            {
                tempFileName = "temp_" + DateTime.Now.Ticks + ".jpg";
                await selectedImage.CopyAsync(App.ProfileImgFolder, tempFileName);
                SelectedImageSrc = new Uri(Path.Combine(App.ProfileImgFolder.Path, tempFileName));

                // Ensure the stream is disposed once the image is loaded
                using (IRandomAccessStream fileStream = await selectedImage.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);

                    double height = MinEdgeSize * AspectRatio;
                    double width = MinEdgeSize;

                    if (decoder.PixelHeight >= (uint)(AspectRatio * MinEdgeSize) &&
                        decoder.PixelWidth >= (uint)MinEdgeSize)
                    {
                        xMin = 0;// (Navigation.PageFrame.ActualWidth - decoder.PixelWidth) / 2;
                        xMax = xMin + decoder.PixelWidth;
                        yMin = 0;// (Navigation.PageFrame.ActualHeight - decoder.PixelHeight) / 2;
                        yMax = yMin + decoder.PixelHeight;
                        OuterRect = new Rect(xMin, yMin, Navigation.PageFrame.ActualWidth, Navigation.PageFrame.ActualHeight);

                        ResetCorner((Navigation.PageFrame.ActualWidth - width)/2, (Navigation.PageFrame.ActualWidth + width)/2,
                            (Navigation.PageFrame.ActualHeight - height)/2, (Navigation.PageFrame.ActualHeight + height)/2);
                    }
                    else
                    {
                        string message = string.Format("Seçilen Görüntünün Boyutu({0}x{1}) en az {2}x{3} olmalıdır.\nLütfen Tekrar Deneyiniz.", decoder.PixelHeight, decoder.PixelWidth, height, width);
                        var messageDialog = new MessageDialog(message, "Görüntü Boyutu");

                        // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                        messageDialog.Commands.Add(new UICommand("Tamam", new UICommandInvokedHandler(ReSelectImageCommandInvokedHandler)));

                        // Set the command that will be invoked by default
                        messageDialog.DefaultCommandIndex = 1;

                        // Set the command to be invoked when escape is pressed
                        messageDialog.CancelCommandIndex = 0;

                        // Show the message dialog
                        messageDialog.ShowAsync();
                    }
                }
            }
        }
        
        public void ResetCorner(double leftX, double rightX,
            double topY, double bottomY)
        {
            this.LeftX = leftX;
            this.RightX = rightX;
            this.TopY= topY;
            this.BottomY = bottomY;

            UpdateSelectedRect();
        }

        private void UpdateSelectedRect()
        {
            SelectedRect = new Rect(leftX, topY, rightX-leftX, bottomY-topY);
        }

        async public Task SaveCroppedBitmapAsync()
        {
            // Convert start point and size to integer.
            uint startPointX = (uint)Math.Floor(selectedRect.X);
            uint startPointY = (uint)Math.Floor(selectedRect.Y);
            uint height = (uint)Math.Floor(selectedRect.Height);
            uint width = (uint)Math.Floor(selectedRect.Width);

            fileName = "photo_" + DateTime.Now.Ticks + ".jpg";
            StorageFile newImageFile = await (await ApplicationData.Current.LocalFolder.GetFolderAsync(App.PROFILE_PHOTO_FOLDER)).CreateFileAsync(fileName);

            using (IRandomAccessStream originalImgFileStream = await selectedImage.OpenReadAsync())
            {
                // Create a decoder from the stream. With the decoder, we can get 
                // the properties of the image.
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(originalImgFileStream);

                // Refine the start point and the size. 
                if (startPointX + width > decoder.PixelWidth)
                {
                    startPointX = decoder.PixelWidth - width;
                }

                if (startPointY + height > decoder.PixelHeight)
                {
                    startPointY = decoder.PixelHeight - height;
                }

                // Get the cropped pixels.
                byte[] pixels = await GetPixelData(decoder, startPointX, startPointY, width, height,
                    decoder.PixelWidth, decoder.PixelHeight);

                using (IRandomAccessStream newImgFileStream = await newImageFile.OpenAsync(FileAccessMode.ReadWrite))
                {

                    Guid encoderID = Guid.Empty;

                    switch (newImageFile.FileType.ToLower())
                    {
                        case ".png":
                            encoderID = BitmapEncoder.PngEncoderId;
                            break;
                        case ".bmp":
                            encoderID = BitmapEncoder.BmpEncoderId;
                            break;
                        default:
                            encoderID = BitmapEncoder.JpegEncoderId;
                            break;
                    }

                    // Create a bitmap encoder

                    BitmapEncoder bmpEncoder = await BitmapEncoder.CreateAsync(
                        encoderID,
                        newImgFileStream);

                    // Set the pixel data to the cropped image.
                    bmpEncoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Straight,
                        width,
                        height,
                        decoder.DpiX,
                        decoder.DpiY,
                        pixels);

                    // Flush the data to file.
                    await bmpEncoder.FlushAsync();
                }
            }
        }

        async static private Task<byte[]> GetPixelData(BitmapDecoder decoder, uint startPointX, uint startPointY,
            uint width, uint height, uint scaledWidth, uint scaledHeight)
        {

            BitmapTransform transform = new BitmapTransform();
            BitmapBounds bounds = new BitmapBounds();
            bounds.X = startPointX;
            bounds.Y = startPointY;
            bounds.Height = height;
            bounds.Width = width;
            transform.Bounds = bounds;

            transform.ScaledWidth = scaledWidth;
            transform.ScaledHeight = scaledHeight;

            // Get the cropped pixels within the bounds of transform.
            PixelDataProvider pix = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.IgnoreExifOrientation,
                ColorManagementMode.ColorManageToSRgb);
            byte[] pixels = pix.DetachPixelData();
            return pixels;
        }
    }
}
