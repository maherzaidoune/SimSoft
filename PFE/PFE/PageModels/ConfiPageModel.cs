using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class ConfiPageModel : FreshMvvm.FreshBasePageModel
    {
        public ConfiPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
