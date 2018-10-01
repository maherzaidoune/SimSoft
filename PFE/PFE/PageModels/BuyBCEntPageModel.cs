using System;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class BuyBCEntPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            Application.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }
        public BuyBCEntPageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
