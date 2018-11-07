using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class BuyBRecentetPage : ContentPage
    {
        public BuyBRecentetPage()
        {
            InitializeComponent();
            //if (Device.RuntimePlatform == Device.Android)
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
