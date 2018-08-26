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
                        toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                    }
                    else
                    {
                        toastConfig.SetBackgroundColor(System.Drawing.Color.LimeGreen);
                    }
                    toastConfig.MessageTextColor = System.Drawing.Color.White;
                    toastConfig.SetPosition(ToastPosition.Bottom);
                    UserDialogs.Instance.Toast(toastConfig);
                }
                catch (Exception e)
                {
                    var toastConfig = new ToastConfig("ERROR " + e.Message);
                    toastConfig.SetDuration(1000);
                    toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
                    toastConfig.MessageTextColor = System.Drawing.Color.White;
                    toastConfig.SetPosition(ToastPosition.Bottom);
                    UserDialogs.Instance.Toast(toastConfig);
                }
                
            });
        }
    }
}
