using System;
using System.Windows.Input;

namespace CommercialRecordSystem.Common
{
    class ICommandImp : ICommand
    {
        private readonly Action<object> _execute = null;
        private readonly Predicate<object> _canExecute = null;
        public event EventHandler CanExecuteChanged;

        #region Constructors
        public ICommandImp(Action<object> execute) : this(execute, null)
        { 
            // deliberately empty 
        }

        public ICommandImp(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
        }
    }
}
