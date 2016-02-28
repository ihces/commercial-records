namespace CommercialRecords.ViewModels
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
