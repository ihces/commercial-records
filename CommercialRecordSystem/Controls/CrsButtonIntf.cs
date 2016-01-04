using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace CommercialRecordSystem.Controls
{
    interface CrsButtonIntf
    {
        bool Validation { get; set; }

        bool Disabled { get; set; }

        void setClickHandler(Action<object, RoutedEventArgs> assignButtonCanExecute);

        void setCommandCanExecute(bool canExecute);
    }
}
