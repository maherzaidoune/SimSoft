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
        private IDialogService _dialogService;

        public SellDetailsPageModel(IDataServices _dataService , IDialogService _dialogService)
        {
            this._dataService = _dataService;
            this._dialogService = _dialogService;

        }

        public ICommand delete => new Command(_delete);

        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {
            if(_dataService.RemoveSellElements()){
                productList.Clear();
                _dialogService.ShowMessage("working on valiid", true);
            }
        }

        private void _delete(object obj)
        {
            if (productList == null)
                _dialogService.ShowMessage("List already empty !", true);
            else if (productList == null)
                _dialogService.ShowMessage("select item to be deleted !", true);
            else{
                Task.Run(async () =>
                {
                    try
                    {
                        if (await _dataService.removeSellElementsAsync(selectedProdut))
                        {
                            productList.Remove(selectedProdut);
                            _dialogService.ShowMessage("name : " + selectedProdut.articles.ARTDESIGNATION + " type : " + selectedProdut.type + " deleted !", false);
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
                list = await _dataService.getSellElementAsync();
            }).Wait();
            if(list != null)
                productList = new ObservableCollection<SellElements>(list);
        }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
