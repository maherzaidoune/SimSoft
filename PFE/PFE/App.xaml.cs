using FreshMvvm;
using PFE.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace PFE
{
	public partial class App : Application
	{
		public App ()
		{
		    InitializeComponent();
            SetUpIOC();
            var rootPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<PageModels.LoginPageModel>();
            MainPage = new FreshMvvm.FreshNavigationContainer(rootPage);
        }
        // setting up ioc container 
        void SetUpIOC()
        {
            FreshMvvm.FreshIOC.Container.Register<IDialogService, DialogService>();
            FreshMvvm.FreshIOC.Container.Register<IRestServices, RestServices>();
        }


        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
