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

        protected virtual void RaisePropertyChanged(string propertyName, bool editableByUser = true)
        {
            if (!Dirty && editableByUser)
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
            if (null == str)
                return str;

            string [] tokens = str.Split();
            string resultStr = string.Empty;
            foreach (string token in tokens)
                if (token.Length > 0)
                    resultStr += char.ToUpper(token[0]) + token.Substring(1) + " ";
            return resultStr.Trim();
        }
    }
}
