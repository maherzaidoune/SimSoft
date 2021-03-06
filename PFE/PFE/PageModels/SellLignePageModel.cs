﻿using FreshMvvm;
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
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
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
                            _dialogService.ShowMessage("ce produit n'existe pas dans ce depo", true);
                            storeQuantity = "0";
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
            get; set;
        }
        private string _storeQuantity;
        public string storeQuantity
        {
            get
            {
                return _storeQuantity;
            }
            set
            {
                _storeQuantity = value;
            }
        }
        private string _LivredQuantity;
        public string LivredQuantity
        {
            get
            {
                return _LivredQuantity;
            }
            set
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(value) || int.Parse(value) >= 0)
                        _LivredQuantity = value;
                }
                catch
                {
                    _LivredQuantity = "1";
                }
            }
        }
        private string _pht;
        public string pht
        {
            get
            {
                return _pht;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) || float.Parse(value) >= 0)
                {
                    try
                    {
                        _pht = value;
                        if (float.Parse(value) > 0 && LivredQuantity != null)
                        {
                            mtht = ((int.Parse(LivredQuantity) * float.Parse(value)) * (100 - float.Parse(remise)) / 100).ToString();
                            pttc = (float.Parse(value) * (1 + tva.TVATAUX) * (100 - float.Parse(remise)) / 100).ToString();
                            mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                        }
                    }
                    catch
                    {
                        _dialogService.ShowMessage("erreur", true);
                    }


                }

            }
        }
        public string mtht
        {
            get; set;
        }
        private string _remise;

        public string remise
        {
            get
            {
                return _remise == null ? "0" : _remise;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (float.Parse(value) < 100)
                        _remise = value;
                    if (pht != null && LivredQuantity != null)
                    {
                        mtht = ((int.Parse(LivredQuantity) * float.Parse(pht))  - float.Parse(remise)).ToString();
                        //pttc = (float.Parse(pht) * (1 + tva.TVATAUX)  - float.Parse(remise)).ToString();
                        mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                    }
                }
            }
        }
        public string unite
        {
            get; set;
        }
        public string pttc
        {
            get; set;
        }
        public string mtttc
        {
            get; set;
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
                            //return;
                        }
                        artfamilles_cpt = await _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());
                        artarifligne = await _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        tva = await _restService.GetTVAbyTVACODE(artfamilles_cpt.TVACODE_FR.ToString());
                        if (selectedDepot != null)
                            storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                        artunite = await _restService.GetRTUNITE("v");
                        designation = article.ARTDESIGNATION;
                        unite = (artunite.ARUCOEF > 0) ? artunite.ARUINTITULE : " ";
                        pht = artarifligne.ATFPRIX.ToString();
                    }
                    catch (Exception e)
                    {
                        _dialogService.ShowMessage("code a barre indisponible ", true);
                        Console.WriteLine(e.StackTrace);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            isEnabled = true;
                            isBusy = false;
                        });
                    }


                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = true;
                        isBusy = false;
                    });
                });
            }

        }
        private IDialogService _dialogService;
        private void _validate(object obj)
        {
            try
            {
                try
                {
                    var r = float.Parse(remise);
                    var m = float.Parse(mtht);
                }
                catch
                {
                    _dialogService.ShowMessage("veillew saisir un montant valide ", true);
                    return;
                }

                try
                {
                    var q = int.Parse(LivredQuantity);
                }
                catch
                {
                    _dialogService.ShowMessage("veillew saisir une quantitees valide ", true);
                    return;
                }
                if (LivredQuantity == null || int.Parse(LivredQuantity) == 0)
                {
                    _dialogService.ShowMessage("quantite doit etre superieur a 0", true);
                    return;
                }
                if (selectedDepot != null){
                    storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), _selectedDepo.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                    if (int.Parse(LivredQuantity) > int.Parse(storeQuantity))
                    {
                        _dialogService.ShowMessage("quantite colise doit etre inferieur a celle du depot", true);
                        return;
                    }
                }else{
                    _dialogService.ShowMessage("depo invalid", true);
                    return;
                }
                    
                mtht = ((int.Parse(LivredQuantity) * float.Parse(pht)) - float.Parse(remise)).ToString();
                mtttc = (float.Parse(mtht) * (1 + tva.TVATAUX)).ToString();
                SellElements sell = new SellElements
                {
                    depot = selectedDepot,
                    tva = tva,
                    articles = article,
                    artarifligne = artarifligne,
                    remise = remise,
                    LivredQuantity = int.Parse(LivredQuantity),
                    mutht = float.Parse(pht),
                    mtht = float.Parse(mtht),
                    mttc = float.Parse(mtttc),
                    ligneUpdated = true
                };
                Task.Run(async () =>
                {
                    if (await _dataService.updateAsyncSellElement(sell))
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
                _dialogService.ShowMessage("erreur", true);
            }

        }


        private IDataServices _dataService;
        public SellLignePageModel(IRestServices _restService, IDataServices _dataService, IDialogService _dialogService)
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
                try
                {
                    var d = await _restService.getDepPrincipal();
                    (depo = new List<depot>()).Add(d);
                    selectedDepot = depo[0];
                }
                catch
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isBusy = false;
                        isEnabled = true;
                    });
                }
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    isEnabled = false;
                //    isBusy = true;
                //});

                //storeQuantity = _restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepot.DEPID.ToString()).Result.ARDSTOCKREEL.ToString();
                //
            });
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = false;
                isEnabled = true;
            });
            _LivredQuantity = "1";
        }
    }
}