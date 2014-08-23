using System;
using System.Linq;
using System.Windows.Input;
using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Popups;

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
        private readonly ICommand saveCustomerCmd;
        public ICommand SaveCustomerCmd
        {
            get
            {
                return saveCustomerCmd;
            }
        }

        private readonly ICommand delCustomerCmd;
        public ICommand DelCustomerCmd
        {
            get
            {
                return delCustomerCmd;
            }
        }
        #endregion

        #region Command Handlers
        private void saveCustomerCmdHandler(object parameter)
        {
            LoadingVisibility = Visibility.Visible;
            Customer newCustomer = new Customer();
            newCustomer.Id = (new Random()).Next();
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
        #endregion

        public CustomerInfoVM()
        {
            saveCustomerCmd = new ICommandImp(saveCustomerCmdHandler);
            delCustomerCmd = new ICommandImp(delCustomerCmdHandler);
        }

        public Customer getCustomer(int customerId) 
        {
            Customer customer = new Customer();
            using (var db = new SQLite.SQLiteConnection(App.DBPath))
            {
                customer = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();
                
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
