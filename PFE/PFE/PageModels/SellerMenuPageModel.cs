using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SellerMenuPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand sellManagment => new Command(_sellManagment);
        public ICommand buyManagment => new Command(_buyManagment);
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
        private void _buyManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<BuyManPageModel>();
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

        public SellerMenuPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
