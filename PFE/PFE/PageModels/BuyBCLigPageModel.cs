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
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    }); 
                    if(article != null){
                        storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), value.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isBusy = false;
                        isEnabled = true;
                    });
                });

            }
        }
        public string storeQuantity
        {
            get;
            set;
        }
        private string _CQuantity;
        public string CQuantity
        {
            get{
                return _CQuantity;
            }
            set{
                if(String.IsNullOrEmpty(value) || float.Parse(value) >= 0){
                    _CQuantity = value;
                }
            }
        }
        private string _puht;
        public string puht
        {
            get
            {
                return _puht;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _puht = value;

                {
                    if (float.Parse(_puht) > 0 && CQuantity != null)
                    {
                        mtht = (int.Parse(CQuantity) * float.Parse(_puht)).ToString();
                        puttc = (float.Parse(_puht) * (1 + tva.TVATAUX) / 100).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                    }
                }
            }
        }
    
        public string puttc
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
                Task.Run(async() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    }); 
                    try
                    {
                        article = await _restService.getArticlebyBC(barreCode);
                        artfamilles_cpt = await _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());
                        artarifligne = await _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        tva = await _restService.GetTVAbyTVACODE(artfamilles_cpt.TVACODE_FR.ToString());
                        if (selectedDepot != null)
                            storeQuantity =  _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();

                        artunite = await _restService.GetRTUNITE("A");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isBusy = false;
                        isEnabled = true;
                    });
                    designation = article.ARTDESIGNATION;
                    cond = (artunite.ARUCOEF > 0) ? artunite.ARUCOEF.ToString() : "0";
                    puht = artarifligne.ATFPRIX.ToString();
                });
            }

        }
        private IDialogService _dialogService;
        private void _validate(object obj)
        {
            mtht = ((int.Parse(CQuantity) * float.Parse(_puht)) / 100).ToString();
            mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
            puttc = (float.Parse(_puht) * (1 + tva.TVATAUX) / 100).ToString();
            Buyelement buy = new Buyelement
            {
                depot = selectedDepot,
                tva = tva,
                articles = article,
                artarifligne = artarifligne,
                LivredQuantity = int.Parse(CQuantity),
                mutht = float.Parse(puht),
                mtht = float.Parse(mtht),
                mttc = float.Parse(mtttc),
                ligneUpdated = true
            };
            Task.Run(async () =>
            {
                if (await _dataService.updateAsyncBuyElement(buy)){
                    _dialogService.ShowMessage("ligne modifiee ", false);
                }else{
                    _dialogService.ShowMessage("erreur inattendue ", true);
                }
            });
           
        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            Application.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        private IRestServices _restService;
        private IDataServices _dataService;
        public BuyBCLigPageModel(IRestServices _restService, IDataServices _dataService, IDialogService _dialogService)
        {
            this._restService = _restService;
            this._dataService = _dataService;
            this._dialogService = _dialogService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    isEnabled = false;
                //    isBusy = true;
                //});
                depo = await _restService.GetDepot("o");
                selectedDepot = depo[0];

            });
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = false;
                isEnabled = true;
            });
            //_LivredQuantity = "1";
        }
    }
}
