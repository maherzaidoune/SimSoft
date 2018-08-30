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
        public ICommand bonderetout => new Command(_bonderetout);
        public ICommand facturev => new Command(_facturev);
        public ICommand facturer => new Command(_facturer);

        private void _facturer(object obj)
        {
            Navigation.initTabsSellFR();
        }

        private void _facturev(object obj)
        {
            Navigation.initTabsSellFV();
        }

        private void _bonderetout(object obj)
        {
            Navigation.initTabsSellBR();
        }

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
