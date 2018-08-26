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
    class SellMainPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand devis => new Command(_devis);
        public ICommand bondecommand => new Command(_bondecommand);
        public ICommand bondelivr => new Command(_bondelivr);

        private void _bondelivr(object obj)
        {
            Navigation.initTabsSellBL();
        }

        private void _bondecommand(object obj)
        {
            Navigation.initTabsSellBC();
        }

        private void _devis(object obj)
        {
            Navigation.initTabsSellBC();
        }

        public SellMainPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
