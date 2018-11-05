using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class BuyFAentPage : ContentPage
    {
        public BuyFAentPage()
        {
            InitializeComponent();
            //if (Device.RuntimePlatform == Device.Android)
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
