using FreshMvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class UserManPageModel : FreshMvvm.FreshBasePageModel
    {
        public ICommand groupMangment => new Command(_groupMangment);
        public ICommand userManagment => new Command(_userManagment);
        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        private void _userManagment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<UserPageModel>();
                RaisePropertyChanged();
            });
        }

        private void _groupMangment(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PushPageModel<GroupManPageModel>();
                RaisePropertyChanged();
            });
        }

        public UserManPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
