using Windows.UI.Xaml.Controls;
using CommercialRecords.ViewModels;

namespace CommercialRecords.Panels
{
    class CSRFirmGridView : GridView
    {
        public CSRFirmGridView()
        {
            //DoubleTapped += CSRFirmGridView_DoubleTapped;
        }
        /*
        private void CSRFirmGridView_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            CSRFirmGridView obj = sender as CSRFirmGridView;
            FirmVM selectedFirmItem = obj.SelectedItem as FirmVM;
            GridViewItem tappedElement = obj.ContainerFromItem(obj.SelectedItem) as GridViewItem;
            selectedFirmItem.ShowGoodList = !selectedFirmItem.ShowGoodList;

            PrepareContainerForItemOverride(tappedElement, selectedFirmItem);
        }

        protected override void PrepareContainerForItemOverride
        (Windows.UI.Xaml.DependencyObject element, object item)
        {
            var firm = item as FirmVM;
            if (firm != null)
            {
                var griditem = element as GridViewItem;
                if (griditem != null)
                {
                    griditem.SetValue(VariableSizedWrapGrid.RowSpanProperty, 1);
                    griditem.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, firm.ColumnSpan);
                }
            }
            base.PrepareContainerForItemOverride(element, item);
        }*/
    }
}
