using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class BuyManPageModel : FreshMvvm.FreshBasePageModel
    {
        public BuyManPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
