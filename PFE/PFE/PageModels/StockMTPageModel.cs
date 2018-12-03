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
    public class StockMTPageModel : FreshMvvm.FreshBasePageModel
    {

        public float reelQuantity { get; set; }

        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

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
        private depot _selectedDepoin;
        public depot selectedDepotin
        {
            get
            {
                return _selectedDepoin;
            }
            set
            {
                _selectedDepoin = value;
            }
        }
        private depot _selectedDepotout;
        public depot selectedDepotout
        {
            get
            {
                return _selectedDepotout;
            }
            set
            {
                Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    }); 
                    _selectedDepotout = value;
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

        public string code
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
                _pht = value;
            }
        }

        //save
        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {
            try
            {
                reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepotout.DEPID.ToString()).Result.ARDSTOCKREEL;
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
            catch{
                _dialogService.ShowMessage("Error  ", true);
            }           

            StockLigne stockLigne = new StockLigne
            {
                code = code,
                designation = designation,
                quantite = Quantity,
                prix = pht,
                depin = selectedDepotin,
                depout = selectedDepotout,
                sense = 0,
                article = article,
                artfamilles_cpt = artfamilles_cpt,
                artarifligne = artarifligne,
                numpiece = numeroPiece,
                numauto = numauto,
                pIECE_NATURE = selectednature
            };
            Task.Run(async () =>
            {
                if (await _dataServices.addStockLigneMTAsync(stockLigne))
                {
                    _dialogService.ShowMessage("article " + code + " ajouter list avec succes ", false);
                    MessagingCenter.Send(this, "MT");
                }
                else
                    _dialogService.ShowMessage("error", true);
            });
        }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
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
                    Console.WriteLine("Scanned Barcode: " + result.Text);
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
                        code = article.ARTCODE;
                        designation = article.ARTDESIGNATION;
                        if (selectedDepotout != null)
                        {
                            reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepotout.DEPID.ToString()).Result.ARDSTOCKREEL;
                            Quantity = reelQuantity.ToString();
                        }

                        _pht = artarifligne.ATFPRIX.ToString();
                    }
                    catch (Exception e)
                    {
                        _dialogService.ShowMessage("Error" + e.Message, true);
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
        public StockMTPageModel(IRestServices _restService, IDataServices _dataServices, IDialogService _dialogService)
        {
            this._restService = _restService;
            this._dataServices = _dataServices;
            this._dialogService = _dialogService;
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
                nature = await _restService.GetPieceNaturebyPINID("18");
                depo = await _restService.GetDepot("o");
                selectednature = nature[0];
                selectedDepotin = depo[0];
                selectedDepotout = depo[1];
                numauto = await _restService.getNumPiecenyNature(selectednature.PINID.ToString());
                var comp = numauto.NUMCOMPTEUR + 1;
                numeroPiece = numauto.NUMSOUCHE + "000" + comp;
                //reelQuantity = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepotout.DEPID.ToString()).Result.ARDSTOCKREEL;
                //Quantity = reelQuantity.ToString();
            });

            isBusy = false;
            isEnabled = true;
        }
    }
}
