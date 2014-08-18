using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TicariKayitSistemi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Costumers : Page
    {
        public Costumers()
        {
            this.InitializeComponent();
        }

        private void backButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void costumersList_clicked(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerAccount));
        }

        private void addCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerInfo));
        }

        private void editCustButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerInfo));
        }

        private void deleteCustButton_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Seçili müşteri kalıcı olarak silmek istediğinizden emin misiniz?", "Müşteri Silme");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand("Hayır", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(this.CommandInvokedHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            messageDialog.ShowAsync();
        }
        #region Commands
        /// <summary>
        /// Callback function for the invocation of the dialog commands.
        /// </summary>
        /// <param name="command">The command that was invoked.</param>
        private void CommandInvokedHandler(IUICommand command)
        {

        }
        #endregion
    }
}
