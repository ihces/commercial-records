namespace CommercialRecords.ViewModels
{
    public interface DataVMIntf<E>
    {
        void Refresh();

        void initWithModel(E model);

        E convert2Model();

        int save(bool recordLog);

        DataVMIntf<E> get(int id);

        DataVMIntf<E> delete(bool recordLog);
    }
}
