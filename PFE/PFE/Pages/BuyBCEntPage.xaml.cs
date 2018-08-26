using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class BuyBCEntPage : ContentPage
    {
        public BuyBCEntPage()
        {
            if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
