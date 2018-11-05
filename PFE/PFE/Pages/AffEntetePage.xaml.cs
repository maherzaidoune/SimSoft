using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class AffEntetePage : ContentPage
    {
        public AffEntetePage()
        {
            //if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
