using System;
using PropertyChanged;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StockSEentPageModel : FreshMvvm.FreshBasePageModel
    {
        public StockSEentPageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
