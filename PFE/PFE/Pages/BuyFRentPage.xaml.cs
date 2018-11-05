using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class BuyFRentPage : ContentPage
    {
        public BuyFRentPage()
        {
            InitializeComponent();
            //if (Device.RuntimePlatform == Device.Android)
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
