using System;

using Xamarin.Forms;

namespace PFE.PageModels
{
    public class BuyFAentPage : ContentPage
    {
        public BuyFAentPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

