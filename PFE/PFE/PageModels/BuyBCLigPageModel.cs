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
                    if (article != null)
                    {
                        try
                        {
                            storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), value.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                        }
                        catch
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                isBusy = false;
                                isEnabled = true;
                            });
                        }
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
            get
            {
                return _CQuantity != null ? _CQuantity : "0";
            }
            set
            {
                if (String.IsNullOrEmpty(value) || float.Parse(value) >= 0)
                {
                    try{
                        var q = int.Parse(value);
                        _CQuantity = value;
                        mtht = ((int.Parse(CQuantity) * float.Parse(puht) - float.Parse(value))).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX) - float.Parse(value)).ToString();
                    }catch{

                    }
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
                if (!string.IsNullOrWhiteSpace(value) || float.Parse(value) >= 0)
                {
                    _puht = value;
                    try
                    {
                        puttc = (float.Parse(value) * (1 + tva.TVATAUX) / 100).ToString();
                        mtht = (int.Parse(CQuantity) * float.Parse(value)).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                    }
                    catch
                    {
                        //_dialogService.ShowMessage("erreur", true);
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


        private string _remise;
        public string remise
        {
            get{
                return _remise;
            }
            set{
                if(!String.IsNullOrWhiteSpace(value)){
                    try{
                        var r = float.Parse(value);
                        _remise = value;
                        mtht = ((int.Parse(CQuantity) * float.Parse(puht) - float.Parse(value))).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX) - float.Parse(value)).ToString();

                    }
                    catch{

                    }
                }
            }
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
                Task.Run(async () =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    });
                    try
                    {
                        article = await _restService.getArticlebyBC(barreCode);
                        if (article == null)
                        {
                            _dialogService.ShowMessage("code a barre indisponible ", true);
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                isBusy = false;
                                isEnabled = true;
                            });
                            return;
                        }
                        designation = article.ARTDESIGNATION;
                        artfamilles_cpt = await _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());
                        artarifligne = await _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        puht = artarifligne.ATFPRIX.ToString();
                        artunite = await _restService.GetRTUNITE("A");
                        tva = await _restService.GetTVAbyTVACODE(artfamilles_cpt.TVACODE_FR.ToString());
                        if (selectedDepot != null)
                            storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                        cond = (artunite.ARUCOEF > 0) ? artunite.ARUCOEF.ToString() : "0";
                        if (String.IsNullOrEmpty(CQuantity) || int.Parse(CQuantity) <= 0)
                        {
                            CQuantity = "1";
                        }
                        mtht = ((int.Parse(CQuantity) * float.Parse(puht))).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                        puttc = (float.Parse(puht) * (1 + tva.TVATAUX)).ToString();
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

                });
            }

        }
        private IDialogService _dialogService;
        private void _validate(object obj)
        {
            try
            {
                try{
                    var r = float.Parse(remise);
                    var m = float.Parse(mtht);
                }
                catch{
                    _dialogService.ShowMessage("veillew saisir un montant valide ", true);
                    return;
                }

                try{
                    var q = int.Parse(CQuantity);
                }catch{
                    _dialogService.ShowMessage("veillew saisir une quantitees valide ", true);
                    return;
                }

                if (CQuantity == null || int.Parse(CQuantity) < 0)
                {
                    _dialogService.ShowMessage("quantite doit etre superieur a 0", true);
                    return;
                }
                if (remise == null || float.Parse(remise) < 0)
                {
                    _dialogService.ShowMessage("remise doit etre superieur a 0", true);
                    return;
                }
                mtht = ((int.Parse(CQuantity) * float.Parse(puht) - float.Parse(remise))).ToString();
                mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX) - float.Parse(remise)).ToString();
                puttc = (float.Parse(puht) * (1 + tva.TVATAUX)).ToString();
                Buyelement buy = new Buyelement
                {
                    depot = selectedDepot,
                    tva = tva,
                    articles = article,
                    remise = remise,
                    artarifligne = artarifligne,
                    LivredQuantity = int.Parse(CQuantity),
                    mutht = float.Parse(puht),
                    mtht = float.Parse(mtht),
                    mttc = float.Parse(mtttc),
                    ligneUpdated = true
                };
                Task.Run(async () =>
                {
                    if (await _dataService.updateAsyncBuyElement(buy))
                    {
                        _dialogService.ShowMessage("ligne modifiee ", false);
                    }
                    else
                    {
                        _dialogService.ShowMessage("erreur inattendue ", true);
                    }
                });
            }
            catch
            {
                _dialogService.ShowMessage("erreur inattendue ", true);
            }


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
                try
                {
                    depo = await _restService.GetDepot("o");
                    selectedDepot = depo[0];
                }
                catch
                {

                }


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