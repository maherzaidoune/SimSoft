using System;
using System.Collections.Generic;
using System.Windows.Input;
using FreshMvvm;
using PFE.Models;
using PropertyChanged;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class BuyBCLigPageModel : FreshMvvm.FreshBasePageModel
    {
        public string barreCode
        {
            get;
            set;
        }
        public string designation
        {
            get;
            set;
        }     
        public IList<depot> depo
        {
            get;
            set;
        }
        public string selectedDepot
        {
            get;
            set;
        }
        public string storeQuantity
        {
            get;
            set;
        }
        public string CQuantity
        {
            get;
            set;
        }
        public string puht
        {
            get;
            set;
        }
        public string remise
        {
            get;
            set;
        }
        public string puttc
        {
            get;
            set;
        }
        public string EQuantity
        {
            get;
            set;
        }
        public string Quantity
        {
            get;
            set;
        }
        public string mtht
        {
            get;
            set;
        }
        public string cond
        {
            get;
            set;
        }
        public string mtttc
        {
            get;
            set;
        }

        public ICommand scan => new Command(_Scan);

        private void _Scan(object obj)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                    var result = await scanner.Scan();

                    if (result != null)
                    {
                        barreCode = result.Text;
                        //_valid(null);
                        Console.WriteLine("Scanned Barcode: " + result.Text);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }


            });

        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        public BuyBCLigPageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
