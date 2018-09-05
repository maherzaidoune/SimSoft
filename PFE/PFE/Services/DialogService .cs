using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PFE.Services
{
    class DialogService : FreshMvvm.FreshBasePageModel, IDialogService
    {
        public void ShowMessage(string message, bool error)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var toastConfig = new ToastConfig(message);
                    toastConfig.SetDuration(1500);
                    if (error)
                    {
                        toastConfig.SetBackgroundColor(Color.Red);
                    }
                    else
                    {
                        toastConfig.SetBackgroundColor(Color.LimeGreen);
                    }
                    toastConfig.MessageTextColor = Color.White;
                    toastConfig.SetPosition(ToastPosition.Bottom);
                    UserDialogs.Instance.Toast(toastConfig);
                }
                catch (Exception e)
                {
                    var toastConfig = new ToastConfig("ERROR " + e.Message);
                    toastConfig.SetDuration(1000);
                    toastConfig.SetBackgroundColor(Color.Red);
                    toastConfig.MessageTextColor = Color.White;
                    toastConfig.SetPosition(ToastPosition.Bottom);
                    UserDialogs.Instance.Toast(toastConfig);
                }
                
            });
        }
    }
}
