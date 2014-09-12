﻿using CommercialRecordSystem.ViewModels;
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

namespace CommercialRecordSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Sales : Page
    {
        public Sales()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (null != e.Parameter)
            {
                TransactTypeVM transactBuff = (TransactTypeVM)e.Parameter;
                DateTime transactDateBuff =  transactBuff.SelectedDate;
                string transactDateStr = transactDateBuff.ToString("dd.MM.yyyy");

                this.DataContext = new SaleVM(transactDateStr, transactBuff.SelectedCustomer.Id);
            }
        }

        private void backButton_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Payments));
        }
    }
}