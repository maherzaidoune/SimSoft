using System;
using System.Collections.Generic;
using PFE.Models;
using PropertyChanged;

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

        public BuyBCLigPageModel()
        {
        }
    }
}
