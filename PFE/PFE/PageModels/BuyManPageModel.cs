using PFE.Helper;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class BuyManPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand boncomm => new Command(_boncomm);

        private void _boncomm(object obj)
        {
            Navigation.initTabsBuyBC();
        }

        public BuyManPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
