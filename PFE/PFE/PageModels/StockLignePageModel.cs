using System;
using PropertyChanged;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StockLignePageModel : FreshMvvm.FreshBasePageModel
    {
        public StockLignePageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
