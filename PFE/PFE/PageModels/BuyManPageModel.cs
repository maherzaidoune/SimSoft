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
        public ICommand bonrec => new Command(_bonrec);

        private void _bonrec(object obj)
        {
            Navigation.initTabsBuyBRC();
        }
        public ICommand bonret => new Command(_bonret);

        private void _bonret(object obj)
        {
            Navigation.initTabsBuyBRT();
        }
        public ICommand fachat => new Command(_fachat);

        private void _fachat(object obj)
        {
            Navigation.initTabsBuyBFA();
        }
        public ICommand fret => new Command(_fret);

        private void _fret(object obj)
        {
            Navigation.initTabsBuyBFR();
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
