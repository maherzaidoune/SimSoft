using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFE.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyDetailsPage : ContentPage
    {

        public BuyDetailsPage()
        {
            //if (Device.RuntimePlatform == Device.Android)
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
