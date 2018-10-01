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

        public static void initTabsSellBR()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<SellBREntPageModel>("Entete", "add.png");
            tabs.AddTab<SellLignePageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }

        public static void initTabsSellFV()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<SellFVEntPageModel>("Entete", "add.png");
            tabs.AddTab<SellLignePageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }

        public static void initTabsSellFR()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<SellFREntPageModel>("Entete", "add.png");
            tabs.AddTab<SellLignePageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }

        public static void initTabsBuyBC()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<BuyBCEntPageModel>("Entete", "add.png");
            tabs.AddTab<BuyBCLigPageModel>("Ligne", "details.png");
            tabs.AddTab<SellDetailsPageModel>("Details", "valid.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
        public static void initStockME()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<StockMEPageModel>("Entete", "add.png");
            tabs.AddTab<StockLignePageModel>("Ligne", "details.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
        public static void initStockMS()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<StockMSPageModel>("Entete", "add.png");
            tabs.AddTab<StockLignePageModel>("Ligne", "details.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
        public static void initStockMT()
        {
            var tabs = new FreshMvvm.FreshTabbedNavigationContainer() { BarTextColor = Color.Black, BarBackgroundColor = Color.White };
            tabs.AddTab<StockMTPageModel>("Entete", "add.png");
            tabs.AddTab<StockLignePageModel>("Ligne", "details.png");
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = tabs;
            });
        }
    }
}
