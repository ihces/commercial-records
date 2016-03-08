using CommercialRecords.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace CommercialRecords.ViewModels.DataVMs
{
    class OrderedListItemVM<E, F> : VMBase
        where E : DataVMIntf<F>, new()
        where F : ModelBase, new()
    {
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private ObservableCollection<E> subList;
        public ObservableCollection<E> SubList
        {
            get
            {
                return subList;
            }
            set
            {
                subList = value;
                RaisePropertyChanged("SubList");
            }
        }

        public OrderedListItemVM()
        {
            title = "";
            subList = new ObservableCollection<E>();
        }
    }

    class OrderedListVM<E, F> : ObservableCollection<OrderedListItemVM<E, F>>
        where E : DataVMBase<F>, new()
        where F : ModelBase, new()
    {
        public void FillList(List<E> data, PropertyInfo property, bool alphaNumericOrder = false, bool reverse = false)
        {
            if (null == data || 0 == data.Count)
                return;

           int reverseInt = reverse?-1:1;
            data.Sort(delegate(E obj1, E obj2)
            {
                IComparable value1 = (IComparable)property.GetValue(obj1);
                IComparable value2 = (IComparable)property.GetValue(obj2);
                if (null == value1 && null == value2)
                    return 0;
                else if (null == value1 && null != value2)
                    return reverseInt * -1;
                else if (null != value1 && null == value2)
                    return reverseInt * 1;

                return reverseInt*value1.CompareTo(value2);
            });
            
            E previousRecord = data[0];
            OrderedListItemVM<E, F> itemBuff;

            if (alphaNumericOrder)
            {
                itemBuff = new OrderedListItemVM<E, F>();
                itemBuff.Title = ((string)property.GetValue(previousRecord))[0].ToString();

                foreach (E currentRecord in data)
                {
                    string value1 = (string)property.GetValue(previousRecord);
                    string value2 = (string)property.GetValue(currentRecord);

                    if ((null != value1 && null != value2) && value1[0] != value2[0])
                    {
                        Items.Add(itemBuff);
                        itemBuff = new OrderedListItemVM<E, F>();
                        itemBuff.Title = ((string)property.GetValue(currentRecord))[0].ToString();
                    }

                    itemBuff.SubList.Add(currentRecord);
                    previousRecord = currentRecord;
                }
            }
            else
            {
                itemBuff = new OrderedListItemVM<E, F>();
                itemBuff.Title = (string)property.GetValue(previousRecord);

                foreach (E currentRecord in data)
                {
                    IComparable value1 = (IComparable)property.GetValue(previousRecord);
                    IComparable value2 = (IComparable)property.GetValue(currentRecord);

                    if ((null == value1 && null != value2) ||
                        (null != value1 && null == value2) ||
                        (null != value1 && value1.CompareTo(value2) != 0))
                    {
                        Items.Add(itemBuff);
                        itemBuff = new OrderedListItemVM<E, F>();
                        itemBuff.Title = (string)property.GetValue(currentRecord);
                    }

                    itemBuff.SubList.Add(currentRecord);
                    previousRecord = currentRecord;
                }
            }

            Items.Add(itemBuff);
        }
    }
}
