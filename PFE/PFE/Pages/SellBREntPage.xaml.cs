using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class SellBREntPage : ContentPage
    {
        public SellBREntPage()
        {
            InitializeComponent();
            //if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}