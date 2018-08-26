using PFE.PageModels;
using PFE.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PFE.Helper
{
    class Navigation
    {
        public static void initTabsSellBC()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<SellEntetePageModel>("Entete","add.png");
            tabs.AddTab<SellLignePageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
        public static void initTabsSellBL()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<SellBLEntPageModel>("Entete", "add.png");
            tabs.AddTab<SellLignePageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
    }
}
