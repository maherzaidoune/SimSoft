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

        private void _stockInfo(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<StockPageModel>();
                RaisePropertyChanged();
            });
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
