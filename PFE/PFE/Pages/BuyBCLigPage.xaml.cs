using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class BuyBCLigPage : ContentPage
    {
        public BuyBCLigPage()
        {
            InitializeComponent();
            //if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
