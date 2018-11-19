using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using PFE.Models;
using PFE.Services;
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
        private depot _selectedDepo;
        public depot selectedDepot
        {
            get
            {
                return _selectedDepo;
            }
            set
            {
                _selectedDepo = value;
                Task.Run(() =>
                {
                    storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), value.DEPID.ToString()).ARDSTOCKREEL.ToString();
                });
            }
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
        public ARTICLE article
        {
            get;
            set;
        }
        public ARTUNITE artunite
        {
            get;
            set;
        }
        public ARTFAMILLES_CPT artfamilles_cpt
        {
            get;
            set;
        }
        public ARTTARIFLIGNE artarifligne
        {
            get;
            set;
        }
        public TVA tva
        {
            get;
            set;
        }

        //save
        public ICommand validate => new Command(_validate);
        //get data by bc
        public ICommand valid => new Command(_valid);
        //scan br
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
                        _valid(null);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }


            });

        }

        private void _valid(object obj)
        {
            if (!string.IsNullOrEmpty(barreCode))
            {
                Task.Run(() =>
                {
                    try
                    {
                        article = _restService.getArticlebyBC(barreCode);
                        artfamilles_cpt = _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());

                        artarifligne = _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        tva = _restService.GetTVAbyTVACODE(artfamilles_cpt.TVACODE_FR.ToString());
                        if (selectedDepot != null)
                            storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).ARDSTOCKREEL.ToString();

                        artunite = _restService.GetRTUNITE("v");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                    designation = article.ARTDESIGNATION;
                    //unite = (artunite.ARUCOEF > 0) ? artunite.ARUCOEF.ToString() : "0";
                    //pht = artarifligne.ATFPRIX.ToString();
                });
            }

        }

        private void _validate(object obj)
        {
            //mtht = ((int.Parse(LivredQuantity) * float.Parse(_pht)) * (100 - float.Parse(remise)) / 100).ToString();
            //mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
            Buyelement buy = new Buyelement
            {
                depot = selectedDepot,
                tva = tva,
                articles = article,
                artarifligne = artarifligne,
                ligneUpdated = true
            };
            _dataService.updateAsyncBuyElement(buy);
        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        private IRestServices _restService;
        private IDataServices _dataService;
        public BuyBCLigPageModel(IRestServices _restService, IDataServices _dataService)
        {
            this._restService = _restService;
            this._dataService = _dataService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                depo = await _restService.GetDepot("o");
            });
            //_LivredQuantity = "1";
        }
    }
}
