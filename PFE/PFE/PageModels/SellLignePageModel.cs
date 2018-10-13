using FreshMvvm;
using PFE.Models;
using PFE.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class SellLignePageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restService;
        public IList<depot> depo { get; set; }
        private depot _selectedDepo;
        public depot selectedDepot
        { 
            get{
                return _selectedDepo;
            } set{
                _selectedDepo = value;
                Task.Run(() =>
                {
                    storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString() , value.DEPID.ToString()).ARDSTOCKREEL.ToString();
                });
            }
        }
        public TVA tva
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
        public string barreCode
        {
            get;
            set;
        }
        public string designation
        {
            get;set;
        }
        private string _storeQuantity;
        public string storeQuantity
        {
            get{
                if(!string.IsNullOrEmpty(_storeQuantity)){
                    if (int.Parse(_storeQuantity) > 0)
                        isEnabled = true;
                    else
                        isEnabled = false;
                }
                return _storeQuantity;
            }
            set{
                _storeQuantity = value;
            }
        }
        private string _LivredQuantity;
        public string LivredQuantity
        {
            get{
                return _LivredQuantity;
            }
            set{
                try{
                    if (int.Parse(value) > 0)
                        _LivredQuantity = value;
                }catch{
                    _LivredQuantity = "1";
                }
            }
        }
        private string _pht;
        public string pht
        {
            get{
                return _pht;
            }
            set{
                if(!string.IsNullOrEmpty(value))
                    _pht = value;

                {
                    if (float.Parse(_pht) > 0 && LivredQuantity != null)
                    {
                        mtht = ((int.Parse(LivredQuantity) * float.Parse(_pht)) * (100 - float.Parse(remise)) / 100).ToString();
                        pttc = (float.Parse(_pht) * (1 + tva.TVATAUX) * (100 - float.Parse(remise)) / 100).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                    }
                }
            }
        }
        public string mtht
        {
            get;set;
        }
        private string _remise;
        public bool isEnabled { get; set; }

        public string remise
        {
            get{
                return _remise??"0";
            }
            set{
                if (!string.IsNullOrEmpty(value))
                {
                    if (float.Parse(value) < 100)
                        _remise = value;
                    if (pht != null && LivredQuantity != null)
                    {
                        mtht = ((int.Parse(LivredQuantity) * float.Parse(_pht)) * (100 - float.Parse(remise)) / 100).ToString();
                        pttc = (float.Parse(_pht) * (1 + tva.TVATAUX) * (100 - float.Parse(remise)) / 100).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                    }
                }
            }
        }
        public string unite
        {
            get;set;
        }
        public string pttc
        {
            get;set;
        }
        public string mtttc
        {
            get;set;
        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
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
                        Console.WriteLine("Scanned Barcode: " + result.Text);
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
            if(! string.IsNullOrEmpty(barreCode)){
                Task.Run(() =>
                {
                    try{
                        article = _restService.getArticlebyBC(barreCode);
                        artfamilles_cpt = _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());

                        artarifligne = _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        tva = _restService.GetTVAbyTVACODE(artfamilles_cpt.TVACODE_FR.ToString());
                        if(selectedDepot != null)
                            storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).ARDSTOCKREEL.ToString();

                        artunite = _restService.GetRTUNITE("v");
                    }
                    catch(Exception e){
                        Console.WriteLine(e.StackTrace);
                    }

                    designation = article.ARTDESIGNATION;
                    unite = (artunite.ARUCOEF>0)? artunite.ARUCOEF.ToString() : "0" ;
                    pht = artarifligne.ATFPRIX.ToString();
                });
            }

        }

        private void _validate(object obj)
        {
            SellElements sell = new SellElements
            {
                depot = selectedDepot,
                tva = tva,
                articles = article,
                artarifligne = artarifligne,
                LivredQuantity = int.Parse(LivredQuantity),
                mutht = float.Parse(pht),
                mtht = float.Parse(mtht),
                mttc = float.Parse(mtttc)
             };
            _dataService.updateAsyncSellElementBL(sell);
        }


        private IDataServices _dataService;
        public SellLignePageModel(IRestServices _restService, IDataServices _dataService)
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
            isEnabled = false;
            _LivredQuantity = "1";
        }
    }
}
