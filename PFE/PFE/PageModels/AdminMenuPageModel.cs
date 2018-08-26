using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class AdminMenuPageModel : FreshMvvm.FreshBasePageModel
    {

        public ICommand sellManagment => new Command(_sellManagment);
        public ICommand stockManagment => new Command(_stockManagment);
        public ICommand buyManagment => new Command(_buyManagment);
        public ICommand userManagment => new Command(_userManagment);
        public ICommand Config => new Command(_Config);
        public ICommand quitter => new Command(_quitter);

        private void _quitter(object obj)
        {
            // disconnect
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<LoginPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _Config(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<ConfiPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _userManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<UserManPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _buyManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<BuyManPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _stockManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<StockManPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _sellManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<SellMainPageModel>();
                RaisePropertyChanged();
            });
        }



        public AdminMenuPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
