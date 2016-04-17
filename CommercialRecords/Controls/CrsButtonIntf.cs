using System;
using Windows.UI.Xaml;

namespace CommercialRecords.Controls
{
    interface CrsButtonIntf
    {
        int FunctionalPermission { get; set; }

        bool Validation { get; set; }

        bool Disabled { get; set; }

        void setClickHandler(Action<object, RoutedEventArgs> assignButtonCanExecute);

        void setCommandCanExecute(bool canExecute);
    }
}
