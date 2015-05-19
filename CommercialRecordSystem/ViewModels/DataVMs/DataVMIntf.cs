using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace CommercialRecordSystem.ViewModels
{
    interface DataVMIntf<E>
    {
        void Refresh();

        void initWithModel(E model);

        E convert2Model();

        int save();

        DataVMIntf<E> get(int id);

        DataVMIntf<E> delete();
    }
}
