using System;
using System.Collections.Generic;
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
    public class BuyDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<Buyelement> productList { get; set; }
        public Buyelement selectedProdut { get; set; }
        private IDataServices _dataService;
        private IRestServices _restservices;
        private IDialogService _dialogService;



        public ICommand delete => new Command(_delete);

        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {


            if (_restservices.PostBuyElements(productList))
            {

                if (_dataService.RemoveBuyElements())
                {
                    productList.Clear();
                    _dialogService.ShowMessage("Done", false);
                }
            }

        }

        private void _delete(object obj)
        {
            if (productList == null)
                _dialogService.ShowMessage("liste vide !", true);
            else if (productList == null)
                _dialogService.ShowMessage("choisissez l'element a supprimer", true);
            else
            {
                Task.Run(async () =>
                {
                    try
                    {
                        if (await _dataService.removeBuyElementsAsync(selectedProdut))
                        {
                            productList.Remove(selectedProdut);
                            _dialogService.ShowMessage("name : " + selectedProdut.articles.ARTDESIGNATION + " type : " + selectedProdut.type + " effacer avec succes !", false);
                        }
                    }
                    catch (Exception e)
                    {
                        _dialogService.ShowMessage("Erreur " + e.Message, true);
                    }
                });
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            IList<Buyelement> list = new List<Buyelement>();
            Task.Run(async () =>
            {
                list = await _dataService.getBuyElementAsync();
            }).Wait();
            if (list != null)
                productList = new ObservableCollection<Buyelement>(list);
        }


        public BuyDetailsPageModel(IDataServices _dataService, IDialogService _dialogService, IRestServices _restservices)
        {
            this._dataService = _dataService;
            this._dialogService = _dialogService;
            this._restservices = _restservices;
        }
    }
}
