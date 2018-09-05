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
        public ICommand valid => new Command(_valid);
        public ICommand tester => new Command(_teser);

        private void _teser(object obj)
        {
            try
            {

                stock = new List<stockElement>(){
                  //  new stockElement(){label="ARTID" , info =  depot.ARTID.ToString()},
                   // new stockElement(){label="DEPID" , info = depot.DEPID.ToString()},
                   // new stockElement(){label="ARDEMPLACEMENT" , info = depot.ARDEMPLACEMENT?? " "},
                   // new stockElement(){label="ARDSEUILMIN" , info = depot.ARDSEUILMIN.ToString()},
                  //  new stockElement(){label="ARDSEUILMAX" , info = depot.ARDSEUILMAX.ToString()},
                    new stockElement(){label="ARDSTOCKREEL" , info = depot.ARDSTOCKREEL.ToString()},
                    new stockElement(){label="ARDSTOCKCDE" , info = depot.ARDSTOCKCDE.ToString()},
                    new stockElement(){label="ARDSTOCKRSV" , info = depot.ARDSTOCKRSV.ToString()},
                    new stockElement() {label="terme" , info = (depot.ARDSTOCKREEL + depot.ARDSTOCKCDE - depot.ARDSTOCKRSV).ToString()}
                   /*
                    new stockElement(){label="ARDLASTDATEIN" , info = depot.ARDLASTDATEIN.ToString()},
                    new stockElement(){label="ARDLASTDATEOUT" , info = depot.ARDLASTDATEOUT.ToString()},
                    new stockElement(){label="ARDSTOCKFAB" , info = depot.ARDSTOCKFAB.ToString()},
                    new stockElement(){label="ARDSTOCKSAV" , info = depot.ARDSTOCKSAV.ToString()},
                    new stockElement(){label="ARDSTOCKCTM" , info = depot.ARDSTOCKCTM.ToString()},
                    new stockElement(){label="ARDISBLOQUE" , info = depot.ARDISBLOQUE?? " "}, */
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void _valid(object obj)
        {
            try
            {
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
        public IRestServices _restService
        {
            get;
            set;
        }
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
        public ARTDEPOT depot
        {
            get;
            set;
        }
        public StockPageModel(IRestServices _restService)
        {
            this._restService = _restService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

        }
    }
}