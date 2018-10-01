using FreshMvvm;
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
    class StockManPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand stockInfo => new Command(_stockInfo);
        public ICommand me => new Command(_me);

        private void _me(object obj)
        {
            Navigation.initStockME();
        }

        public ICommand ms => new Command(_ms);

        private void _ms(object obj)
        {
            Navigation.initStockMS();
        }

        public ICommand mt => new Command(_mt);

        private void _mt(object obj)
        {
            Navigation.initStockMT();
        }

        private void _stockInfo(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<StockPageModel>();
                RaisePropertyChanged();
            });
        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        public StockManPageModel()
        {

        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
