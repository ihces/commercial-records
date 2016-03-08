using CommercialRecords.ViewModels.FrameVMs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CommercialRecords.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImagePicker : ViewBase
    {
        Dictionary<uint, Point?> pointerPositionHistory = new Dictionary<uint, Point?>();

        public ImagePicker()
            : base(typeof(ImagePickerFrameVM))
        {
            this.InitializeComponent();

            AddCornerEvents(topLeftCorner);
            AddCornerEvents(topRightCorner);
            AddCornerEvents(bottomLeftCorner);
            AddCornerEvents(bottomRightCorner);

            selectRegion.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
        }

        private void AddCornerEvents(Control corner)
        {
            corner.PointerPressed += Corner_PointerPressed;
            corner.PointerMoved += Corner_PointerMoved;
            corner.PointerReleased += Corner_PointerReleased;
        }

        private void Corner_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            (sender as UIElement).CapturePointer(e.Pointer);

            Windows.UI.Input.PointerPoint pt = e.GetCurrentPoint(this);

            // Record the start point of the pointer.
            ImagePickerFrameVM viewModel = (ImagePickerFrameVM)DataContext;
            Point previousPosition;
            switch ((sender as ContentControl).Name)
            {
                case ImagePickerFrameVM.TOP_LEFT_CORNER:
                    previousPosition.X = viewModel.LeftX;
                    previousPosition.Y = viewModel.TopY;
                    break;
                case ImagePickerFrameVM.TOP_RIGHT_CORNER:
                    previousPosition.X = viewModel.RightX;
                    previousPosition.Y = viewModel.TopY;
                    break;
                case ImagePickerFrameVM.BOTTOM_LEFT_CORNER:
                    previousPosition.X = viewModel.LeftX;
                    previousPosition.Y = viewModel.BottomY;
                    break;
                case ImagePickerFrameVM.BOTTOM_RIGHT_CORNER:
                    previousPosition.X = viewModel.RightX;
                    previousPosition.Y = viewModel.BottomY;
                    break;
                default:
                    previousPosition = pt.Position;
                    break;
            }
            pointerPositionHistory[pt.PointerId] = (previousPosition);

            e.Handled = true;
        }

        private void Corner_PointerMoved(object sender, PointerRoutedEventArgs e)
        {

            Windows.UI.Input.PointerPoint pt = e.GetCurrentPoint(this);
            uint ptrId = pt.PointerId;

            if (pointerPositionHistory.ContainsKey(ptrId) && pointerPositionHistory[ptrId].HasValue)
            {
                Point currentPosition = pt.Position;
                Point previousPosition = pointerPositionHistory[ptrId].Value;

                ((ImagePickerFrameVM)DataContext).changeCornerCoor((sender as ContentControl).Name, currentPosition, previousPosition);
            }

            e.Handled = true;
        }

        private void Corner_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            uint ptrId = e.GetCurrentPoint(this).PointerId;
            if (this.pointerPositionHistory.ContainsKey(ptrId))
            {
                this.pointerPositionHistory.Remove(ptrId);
            }

            (sender as UIElement).ReleasePointerCapture(e.Pointer);
            e.Handled = true;
        }
    }
}
