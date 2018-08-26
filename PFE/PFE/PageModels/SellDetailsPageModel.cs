using PFE.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SellDetailsPageModel : FreshMvvm.FreshBasePageModel
    {
        public ObservableCollection<Articles> productList { get; set; }
        public Articles selectedProdut { get; set; }
        public SellDetailsPageModel()
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            productList = new ObservableCollection<Articles>()
            {
                new Articles(){ DEPINTITULE = "Test"}
            };
        }
    }
}
