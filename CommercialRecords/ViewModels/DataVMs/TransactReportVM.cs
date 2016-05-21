using CommercialRecords.Common;
using CommercialRecords.Models;
using CommercialRecords.ViewModels.DataVMs.Settings;
using System;

namespace CommercialRecords.ViewModels.DataVMs
{
    class TransactReportVM : DataVMBase<TransactReport>
    {
        private string transType;
        public string TransType
        {
            get
            {
                return transType;
            }
            set
            {
                transType = value;
                RaisePropertyChanged("TransType");
            }
        }

        private int operatorId;
        public int OperatorId
        {
            get
            {
                return operatorId;
            }
            set
            {
                operatorId = value;
                RaisePropertyChanged("OperatorId");
                RaisePropertyChanged("OperatorName");
            }
        }

        private string operatorName;
        public string OperatorName
        {
            get
            {
                UserVM user = new UserVM();
                user.get(OperatorId);
                return user.Id + " - " + user.Name + " " + user.Surname;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }

        private string oldData;
        public string OldData
        {
            get
            {
                return oldData;
            }
            set
            {
                oldData = value;
                RaisePropertyChanged("OldData");
                RaisePropertyChanged("OldDataCvt");
            }
        }

        private string newData;
        public string NewData
        {
            get
            {
                return newData;
            }
            set
            {
                newData = value;
                RaisePropertyChanged("NewData");
                RaisePropertyChanged("NewDataCvt");
            }
        }

        public string NewDataCvt
        {
            get
            {
                return localizeData(NewData);
            }
        }

        public string OldDataCvt
        {
            get
            {
                return localizeData(OldData);
            }
        }

        private string localizeData(string data)
        {
            string strBuff = string.Empty;
            string[] lines = data.Split('\n');

            foreach (string line in lines)
            {
                string[] tokens = line.Split(new char[] { '[', ']' });
                if (tokens.Length > 1)
                strBuff += CrsDictionary.getInstance().lookup2(tokens[1]);
                strBuff += " " + line.Substring(line.IndexOf(']') + 1) + '\n';

            }
            return strBuff;
        }
    }
}
