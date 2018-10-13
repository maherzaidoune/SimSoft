using PFE.Models;
using PFE.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SellDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<SellElements> productList { get; set; }
        public Articles selectedProdut { get; set; }
        private IDataServices _dataService;

        public SellDetailsPageModel(IDataServices _dataService)
        {
            this._dataService = _dataService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            IList<SellElements> list = new List<SellElements>();
            Task.Run(async() =>
            {
                list = await _dataService.getSellElementBLAsync();
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
