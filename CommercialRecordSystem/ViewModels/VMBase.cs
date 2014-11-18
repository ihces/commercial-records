using System.ComponentModel;

namespace CommercialRecordSystem.ViewModels
{
    abstract class VMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool dirty = false;
        public bool Dirty
        {
            get
            {
                return dirty;
            }
            set
            {
                dirty = value;
                Raise("Dirty");
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (!Dirty)
                Dirty = true;
            Raise(propertyName);
        }

        private void Raise(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string UpperCaseFirst(string str)
        {
            string [] tokens = str.Split();
            string resultStr = string.Empty;
            foreach (string token in tokens)
                resultStr += char.ToUpper(token[0]) + token.Substring(1) + " ";
            return resultStr.Trim();
        }

        public void Refresh()
        { }
    }
}
