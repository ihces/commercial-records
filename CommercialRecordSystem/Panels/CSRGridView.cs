using Windows.UI.Xaml.Controls;
using CommercialRecordSystem.ViewModels;

namespace CommercialRecordSystem.Panels
{
    class CSRGridView : GridView
    {
        public CSRGridView()
        {
            Tapped += CSRGridView_Tapped;
        }

        private void CSRGridView_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CSRGridView obj = sender as CSRGridView;
            var clickedItem = ItemContainerGenerator.ContainerFromItem(obj.SelectedItem);
            if (null != clickedItem)
            {
                VariableSizedWrapGrid.SetColumnSpan(clickedItem as GridViewItem, (int)(obj.SelectedItem as FirmVM).FirmItemWidth);
                VariableSizedWrapGrid.SetRowSpan(clickedItem as GridViewItem, 2);
            }
        }

        protected override void PrepareContainerForItemOverride
        (Windows.UI.Xaml.DependencyObject element, object item)
        {
            var tile = item as FirmVM;
            if (tile != null)
            {
                var griditem = element as GridViewItem;
                if (griditem != null)
                {
                    VariableSizedWrapGrid.SetColumnSpan(griditem, (int)tile.FirmItemWidth);
                    VariableSizedWrapGrid.SetRowSpan(griditem, 2);
                }
            }
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
