using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PFE.Models;
using PFE.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StockLignePageModel : FreshMvvm.FreshBasePageModel
    {

        public bool refresh
        {
            get;
            set;
        }
        public bool grid
        {
            get{
                return !refresh;
            }
        }

        public ICommand save => new Command(_save);

        private void _save(object obj)
        {
            Task.Run(() =>
            {
                refresh = true;
            });
           try{
                if (_restService.PostToStock(stockLigne))
                {
                    //_dialogService.ShowMessage("L'aj", false);
                    if(_dataServices.RemoveStockLigne()){
                        stockLigne.Clear();
                    }else{
                        _dialogService.ShowMessage("can't remove from database ",true);
                    }
                }
            }catch(Exception e){
                _dialogService.ShowMessage("Error" + e.Message, true);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await CoreMethods.PushPageModel<StockManPageModel>();
                    RaisePropertyChanged();
                });
            }
            refresh = false;
        }

        public ICommand delete => new Command(_delete);

        private void _delete(object obj)
        {
            if (stockLigne == null)
                _dialogService.ShowMessage("liste vide !", true);
            else if (selectedelement == null)
                _dialogService.ShowMessage("choisissez l'element a supprimer!", true);
            else{
                try{
                    Task.Run(async () =>
                    {
                        if (selectedelement.sense == 1){
                            if (await _dataServices.RemoveStockLigneMEAsync(selectedelement)){
                                stockLigne.Remove(selectedelement);
                                _dialogService.ShowMessage(selectedelement.code + " deleted !", false);
                            }
                        }
                        if (selectedelement.sense == -1){
                            if (await _dataServices.RemoveStockLigneMSAsync(selectedelement)){
                                _dialogService.ShowMessage(selectedelement.code + " deleted !", false);
                                stockLigne.Remove(selectedelement);
                            }
                        }
                        else{
                            stockLigne.Remove(selectedelement);
                        }
                    });

                 }catch(Exception e){
                    _dialogService.ShowMessage(e.StackTrace, true);
                }
            }

        }

        public ObservableCollection<StockLigne> stockLigne
        {
            get;
            set;
        }
        public StockLigne selectedelement
        {
            get;
            set;
        }
        private IDataServices _dataServices;
        private IRestServices _restService;
        private IDialogService _dialogService;

        public StockLignePageModel(IRestServices _restService, IDataServices _dataServices, IDialogService _dialogService)
        {
            this._restService = _restService;
            this._dataServices = _dataServices;
            this._dialogService = _dialogService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            MessagingCenter.Subscribe<StockMEPageModel>(this, "ME", updateME);
            MessagingCenter.Subscribe<StockMSPageModel>(this, "MS", updateMS);
            MessagingCenter.Subscribe<StockMTPageModel>(this, "MT", updateMT);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "ME", updateME);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "MS", updateMS);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "MT", updateMT);
        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            MessagingCenter.Subscribe<StockMEPageModel>(this, "ME", updateME);
            MessagingCenter.Subscribe<StockMTPageModel>(this, "MT", updateMT);
            MessagingCenter.Subscribe<StockMSPageModel>(this, "MS", updateMS);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "ME", updateME);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "MS", updateMS);
            MessagingCenter.Subscribe<StockSEentPageModel>(this, "MT", updateMT);
        }

        private void updateMT(StockMTPageModel obj)
        {
            refreshPageMT();
        }

        private void updateMS(StockMSPageModel obj)
        {
            refreshPageMS();
        }

        private void updateME(StockMEPageModel obj)
        {
            refreshPageME();
        }

        private void updateMT(StockSEentPageModel obj)
        {
            refreshPageMT();
        }

        private void updateMS(StockSEentPageModel obj)
        {
            refreshPageMS();
        }

        private void updateME(StockSEentPageModel obj)
        {
            refreshPageME();
        }

        public void refreshPageME(){

            Device.BeginInvokeOnMainThread(() =>
            {
                refresh = true;

            });
            Task.Run(async () =>
            {
                try
                {
                    var list = await _dataServices.getStockLigneObjectsMEAsync();
                    if (list != null)
                        stockLigne = new ObservableCollection<StockLigne>(list);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    refresh = false;
                });

            });
        }
        public void refreshPageMT()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                refresh = true;

            });
            Task.Run(async () =>
            {
                try
                {
                    var list = await _dataServices.getStockLigneObjectsMTAsync();
                    if (list != null)
                        stockLigne = new ObservableCollection<StockLigne>(list);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    refresh = false;
                });

            });
        }
        public void refreshPageMS()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                refresh = true;

            });
            Task.Run(async () =>
            {
                try
                {
                    var list = await _dataServices.getStockLigneObjectsMSAsync();
                    if (list != null)
                        stockLigne = new ObservableCollection<StockLigne>(list);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    refresh = false;
                });

            });
        }
    }
}
