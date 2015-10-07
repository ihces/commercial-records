using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using CommercialRecordSystem.Views.Customers;
using CommercialRecordSystem.Views.Transacts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CommercialRecordSystem.ViewModels
{
    class CustomerListFrameVM : FrameVMBase
    {
        #region Properties
        private readonly ICommand findCustomersCmd;
        public ICommand FindCustomersCmd
        {
            get
            {
                return findCustomersCmd;
            }
        }

        private readonly ICommand doOper4SelectedCustomerCmd;
        public ICommand DoOper4SelectedCustomerCmd
        {
            get
            {
                return doOper4SelectedCustomerCmd;
            }
        }

        private readonly ICommand addCustomerCmd;
        public ICommand AddCustomerCmd
        {
            get
            {
                return addCustomerCmd;
            }
        }

        private string queryText = string.Empty;
        public string QueryText
        {
            get
            {
                return queryText;
            }
            set
            {
                queryText = value;
                RaisePropertyChanged("QueryText");
            }
        }

        private int orderByIndex = 0;
        public int OrderByIndex
        {
            get
            {
                return orderByIndex;
            }
            set
            {
                orderByIndex = value;
                if (value != orderByIndex)
                {
                    List<Expression<Func<Customer, object>>>  orderByClauses = 
                        new List<Expression<Func<Customer, object>>>();
                    switch (value)
                    {
                        case 0:
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setCustomers(null, orderByClauses);
                            break;
                        case 1:
                            orderByClauses.Add(c => c.Surname);
                            orderByClauses.Add(c => c.Name);
                            setCustomers(null, orderByClauses);
                            break;
                        case 2:
                            orderByClauses.Add(c => c.LastTransactDate);
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setCustomers(null, orderByClauses);
                            break;
                        case 3:
                            orderByClauses.Add(c => c.AccountCost);
                            orderByClauses.Add(c => c.Name);
                            orderByClauses.Add(c => c.Surname);
                            setCustomers(null, orderByClauses);
                            break;
                    }
                    RaisePropertyChanged("OrderByIndex");
                }
            }
        }



        private ObservableCollection<CustomerVM> customers;
        public ObservableCollection<CustomerVM> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        private CustomerVM selectedCustomer;
        public CustomerVM SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                RaisePropertyChanged("SelectedCustomer");
            }
        }

        private Int32 rowCount;
        public Int32 RowCount
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = value;
                RaisePropertyChanged("RowCount");
            }
        }

        private double totalAccount;
        public double TotalAccount
        {
            get
            {
                return totalAccount;
            }
            set
            {
                totalAccount = value;
                RaisePropertyChanged("TotalAccount");
            }
        }

        #endregion "Properties"

        public CustomerListFrameVM(FrameNavigation navigation)
            : base(navigation)
        {
            findCustomersCmd = new ICommandImp(findCustomers_execute);
            doOper4SelectedCustomerCmd = new ICommandImp(doOper4SelectedCustomer_execute);
            addCustomerCmd = new ICommandImp(addCustomer_execute);
            setCustomers();
        }

        #region Command Method
        public void findCustomers_execute(object parameter)
        {
            setCustomers();
        }

        private void doOper4SelectedCustomer_execute(object obj)
        {
            if (null != SelectedCustomer)
            {
                if (Navigation.Back.PageType.Equals(typeof(TransactTypeSelector)))
                {
                    Navigation.GoBack(SelectedCustomer.Id);
                }
                else
                {
                    Navigation.Navigate<CustomerAccount>(SelectedCustomer.Id);
                }
            }
        }

        private void addCustomer_execute(object obj)
        {
            Navigation.Navigate(typeof(CustomerInfo));
        }
        #endregion

        private async Task setCustomers(Expression<Func<Customer, bool>> whereClause = null, List<Expression<Func<Customer, object>>> orderByClauses = null)
        {
            if (null == orderByClauses)
            {
                orderByClauses = new List<Expression<Func<Customer, object>>>();
                orderByClauses.Add(c => c.Name);
                orderByClauses.Add(c => c.Surname);
            }

            if (string.IsNullOrWhiteSpace(QueryText))
                whereClause = c => c.Type == Customer.TYPE.REGISTERED;
            else
                whereClause = c => c.Type == Customer.TYPE.REGISTERED && (c.Name.Contains("") || c.Surname.Contains("ss"));

            Customers = new ObservableCollection<CustomerVM>(await CustomerVM.getList<CustomerVM>(whereClause, orderByClauses));
            RowCount = Customers.Count;

            double totalAccountBuff = 0.0;
            foreach (CustomerVM customerBuff in Customers)
            {
                totalAccountBuff += customerBuff.AccountCost;
            }
            TotalAccount = totalAccountBuff;
        }
    }
}
