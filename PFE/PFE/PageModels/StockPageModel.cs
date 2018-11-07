using FreshMvvm;
using PFE.Models;
using PFE.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class StockPageModel : FreshMvvm.FreshBasePageModel
    {

        public ICommand quit => new Command(_quit);
        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        public ICommand scan => new Command(_Scan);

        private void _Scan(object obj)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    barreCode = result.Text;
                    _valid(null);
                }

            });
        }

        public ICommand valid => new Command(_valid);


        private void _valid(object obj)
        {
            try
            {
                if(string.IsNullOrEmpty(barreCode)){
                    _dialogService.ShowMessage("entrer un code " , true);
                    return;
                }
                article = _restService.getArticlebyBC(barreCode);
                depot = _restService.GetARTDEPOTbyDepArtid(article.ARTID.ToString());
                code = article.ARTCODE;
                Designation = article.ARTDESIGNATION;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }


        public string terme
        {
            get;
            set;
        }
        public class stockElement
        {
            public string label { get; set; }
            public string info { get; set; }
        }
        public IList<stockElement> stock { get; set; }
        public ARTICLE article
        {
            get;
            set;
        }
        private IRestServices _restService
        {
            get;
            set;
        }
        private IDialogService _dialogService { get; set; }
        public string barreCode
        {
            get;
            set;
        }
        public string code
        {
            get;
            set;
        }
        public string Designation
        {
            get;
            set;
        }
        public IList<ARTDEPOT> depot
        {
            get;
            set;
        }
        public StockPageModel(IRestServices _restService , IDialogService _dialogService)
        {
            this._restService = _restService;
            this._dialogService = _dialogService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

        }
    }
}