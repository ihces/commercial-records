using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models;
using System;

namespace CommercialRecordSystem.ViewModels.FrameVMs.Goods
{
    class GoodInfoFrameVM : InfoFrameVMBase<GoodVM, Good>
    {
        public GoodInfoFrameVM(FrameNavigation navigation)
            : base(navigation, "Ürün", 1.0)
        {
            if (null != navigation.Message && navigation.Message is Int32 []) 
            {
                Int32[] Ids = (Int32 []) navigation.Message;
                CurrentInfo.FirmId = Ids[0];
                CurrentInfo.Id = Ids[1];
            }
        }
    }
}
