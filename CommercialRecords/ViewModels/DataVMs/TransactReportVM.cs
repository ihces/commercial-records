using CommercialRecords.Models;
using System;

namespace CommercialRecords.ViewModels.DataVMs
{
    class TransactReportVM : DataVMBase<TransactReport>
    {
        private int type;
        public int Type {
            get {
                return Type;
            }
            set {
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        private int operatorId;
        public int OperatorId {
            get
            {
                return operatorId;
            }
            set
            {
                operatorId = value;
                RaisePropertyChanged("OperatorId");
            }
        }

        private int transactId;
        public int TransactId {
            get
            {
                return transactId;
            }
            set
            {
                transactId = value;
                RaisePropertyChanged("TransactId");
            }
        }

        private DateTime date;
        public DateTime Date {
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

        private string detail;
        public string Detail {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                RaisePropertyChanged("Detail");
            }
        }

        private double cost;
        public double Cost {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
            }
        }
    }
}
