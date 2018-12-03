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
    public class StockMSPageModel : FreshMvvm.FreshBasePageModel
    {
        public float reelQuantity { get; set; }

        private PIECE_NATURE _selectednature
        {
            get;
            set;
        }
        public PIECE_NATURE selectednature
        {
            get
            {
                return _selectednature;
            }
            set
            {
                _selectednature = value;
                Task.Run(async() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    }); 
                    try
                    {
                        numauto = await _restService.getNumPiecenyNature(value.PINID.ToString());
                        var comp = numauto.NUMCOMPTEUR + 1;
                        numeroPiece = numauto.NUMSOUCHE + "000" + comp;

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
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public NUMAUTO numauto
        {
            get;
            set;
        }
        private IDataServices _dataServices;
        private IRestServices _restService;
        private IDialogService _dialogService;
        public IList<PIECE_NATURE> nature { get; set; }
        public string numeroPiece { get; set; }

        public IList<depot> depo { get; set; }
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
                        try{
                            reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), value.DEPID.ToString()).Result.ARDSTOCKREEL;
                            Quantity = reelQuantity.ToString();
                        }
                        catch
                        {
                            _dialogService.ShowMessage("ce produit n'existe pas dans ce depo", true);
                            Quantity = "0";
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

        public string barreCode
        {
            get;
            set;
        }
        public string designation
        {
            get; set;
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

        public string code
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

        public string Quantity { get; set; }

        private string _pht;
        public string pht
        {
            get
            {
                return _pht;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _pht = value;
            }
        }

        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {

            try
            {
                reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepot.DEPID.ToString()).Result.ARDSTOCKREEL;
                Quantity = reelQuantity.ToString();
                if (string.IsNullOrEmpty(Quantity))
                {
                    _dialogService.ShowMessage("Erreur : quantite doit être supérieur à 0 ", true);
                    return;
                }
                if (int.Parse(Quantity) < 0)
                {
                    _dialogService.ShowMessage("Erreur : quantite doit être supérieur à 0 ", true);
                    return;
                }
                if (int.Parse(Quantity) > reelQuantity)
                {
                    _dialogService.ShowMessage("Erreur : quantite doit être supérieur à  " + reelQuantity, true);
                    return;
                }
                if (int.Parse(pht) < 0)
                {
                    _dialogService.ShowMessage("Erreur : prix doit être supérieur à 0 ", true);
                    return;
                }
            }
            catch
            {
                _dialogService.ShowMessage("Erreur  ", true);
            }
            StockLigne stockLigne = new StockLigne
            {
                code = code,
                designation = designation,
                quantite = Quantity,
                prix = pht,
                depin = null,
                depout = selectedDepot,
                sense = -1,
                article = article,
                artfamilles_cpt = artfamilles_cpt,
                artarifligne = artarifligne,
                numpiece = numeroPiece,
                numauto = numauto,
                pIECE_NATURE = selectednature
            };
            Task.Run(async () =>
            {
                if (await _dataServices.addStockLigneMSAsync(stockLigne))
                {
                    _dialogService.ShowMessage("article " + code + " ajouter list avec succes", false);
                    MessagingCenter.Send(this, "MS");
                }
                else
                    _dialogService.ShowMessage("error", true);
            });
        }

        //get data by bc
        public ICommand valid => new Command(_valid);
        //scan br
        public ICommand scan => new Command(_Scan);

        private void _Scan(object obj)
        {
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                {
                    barreCode = result.Text;
                    _valid(null);
                }

            });
        }
        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
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
                        code = article.ARTCODE;
                        designation = article.ARTDESIGNATION;
                        if(selectedDepot != null){
                            reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepot.DEPID.ToString()).Result.ARDSTOCKREEL;
                            Quantity = reelQuantity.ToString();
                        }

                        _pht = artarifligne.ATFPRIX.ToString();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        _dialogService.ShowMessage("Error" + e.Message, true);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isBusy = false;
                        isEnabled = true;
                    });

                });
            }

        }


        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    isEnabled = false;
                    isBusy = true;
                });
                nature = await _restService.GetPieceNaturebyPINID("20");
                depo = await _restService.GetDepot("o");
                selectedDepot = depo[0];
                selectednature = nature[0];
                numauto = await _restService.getNumPiecenyNature(selectednature.PINID.ToString());
                var comp = numauto.NUMCOMPTEUR + 1;
                numeroPiece = numauto.NUMSOUCHE + "000" + comp;
                //reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), depo[0].DEPID.ToString()).Result.ARDSTOCKREEL;
                //Quantity = reelQuantity.ToString();
                Device.BeginInvokeOnMainThread(() =>
                {
                    isBusy = false;
                    isEnabled = true;

                });
            });


        }

        public StockMSPageModel(IRestServices _restService, IDataServices _dataServices, IDialogService _dialogService)
        {
            this._restService = _restService;
            this._dataServices = _dataServices;
            this._dialogService = _dialogService;
        }
    }
}
