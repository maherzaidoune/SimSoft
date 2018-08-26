using System;
using PropertyChanged;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class BuyBCEntPageModel : FreshMvvm.FreshBasePageModel
    {
        public BuyBCEntPageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
