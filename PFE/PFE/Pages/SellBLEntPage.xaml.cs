﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PFE.Pages
{
    public partial class SellBLEntPage : ContentPage
    {
        public SellBLEntPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
