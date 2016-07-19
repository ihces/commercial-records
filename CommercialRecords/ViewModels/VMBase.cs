using CommercialRecords.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CommercialRecords.ViewModels
{
    public abstract class VMBase : INotifyPropertyChanged
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

                if (dirty)
                {
                    CrsAuthentication.getInstance().updateTimeoutDate();
                }

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

        public override bool Equals(object obj)
        {
            if (null != obj && this.GetType().Equals(obj.GetType()))
            {
                IEnumerable<PropertyInfo> obj1Props = obj.GetType().GetRuntimeProperties();
                IEnumerable<PropertyInfo> obj2Props = this.GetType().GetRuntimeProperties();

                foreach (PropertyInfo prop1 in obj1Props)
                {
                    PropertyInfo prob2 = obj2Props.Where(p => p.Name == prop1.Name).Single();
                    if (null == prob2 || (null != prop1.GetValue(obj) && !prop1.GetValue(obj).Equals(prob2.GetValue(this))))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
            


            return base.Equals(obj);
        }
    }
}
