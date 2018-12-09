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
    class SellDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<SellElements> productList { get; set; }
        public SellElements selectedProdut { get; set; }
        private IDataServices _dataService;
        private IRestServices _restservices;
        private IDialogService _dialogService;

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        public ICommand delete => new Command(_delete);

        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {

            Task.Run(async() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    isEnabled = false;
                    isBusy = true;
                });

                foreach(SellElements s in productList){
                    if (!s.ligneUpdated){
                        productList.Remove(s);
                    }
                }
                if(productList.Count > 0){
                    if (await _restservices.PostSellLignes(productList))
                    {

                        if (_dataService.RemoveSellElements())
                        {
                            productList.Clear();
                            _dialogService.ShowMessage("succes", false);
                        }
                        else
                        {
                            _dialogService.ShowMessage("erreur , veuillez reessayer plus tard", true);
                        }
                    }
                    else
                    {
                        _dialogService.ShowMessage("erreur , veuillez reessayer", true);
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    isBusy = false;
                    isEnabled = true;
                });
                
            });

        }

        private void _delete(object obj)
        {
            if (productList == null)
                _dialogService.ShowMessage("liste vide !", true);
            else if (productList == null)
                _dialogService.ShowMessage("choisissez l'element a supprimer", true);
            else{
                Task.Run(async () =>
                {
                    try
                    {
                        if (await _dataService.removeSellElementsAsync(selectedProdut))
                        {
                            productList.Remove(selectedProdut);
                            _dialogService.ShowMessage("name : " + selectedProdut.articles.ARTDESIGNATION + " type : " + selectedProdut.type + " effacer avec succes !", false);
                        }
                    }
                    catch (Exception e)
                    {
                        _dialogService.ShowMessage("Error " + e.Message, true);
                    }
                });
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            IList<SellElements> list = new List<SellElements>();
            Task.Run(async() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    isEnabled = false;
                    isBusy = true;
                });
                list = await _dataService.getSellElementAsync();
                Device.BeginInvokeOnMainThread(() =>
                {
                    isBusy = false;
                    isEnabled = true;
                });
            }).Wait();
            if(list != null)
                productList = new ObservableCollection<SellElements>(list);
        }

        public SellDetailsPageModel(IDataServices _dataService, IDialogService _dialogService, IRestServices _restservices)
        {
            this._dataService = _dataService;
            this._dialogService = _dialogService;
            this._restservices = _restservices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            isBusy = false;
            isEnabled = true;
        }
    }
}
