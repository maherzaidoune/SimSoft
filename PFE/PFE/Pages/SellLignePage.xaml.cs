﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PFE.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SellLignePage : ContentPage
	{
		public SellLignePage ()
		{
            if (Device.RuntimePlatform == Device.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent ();
		}
	}
}