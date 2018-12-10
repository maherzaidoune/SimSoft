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
    public class StockMEPageModel : FreshMvvm.FreshBasePageModel
    {

        private PIECE_NATURE _selectednature
        {
            get;
            set;
        }
        public int numligne
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
                        var comp = await _restService.getPieceDiversNumber() + numligne;
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
                if (article != null)
                {
                    try
                    {
                        var q = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), value.DEPID.ToString()).Result.ARDSTOCKREEL;
                        //Quantity = reelQuantity.ToString();
                    }
                    catch
                    {
                        _dialogService.ShowMessage("ce produit n'existe pas dans ce depo", true);
                    }

                }
                //depin no need to know the quatitity
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

        public string Quantity { get; set;}

        private string _pht ;
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

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        //save
        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {
            try
            {
                var q = (float)_restService.GetARTDEPOTbyDepid(article.ARTID.ToString(), selectedDepot.DEPID.ToString()).Result.ARDSTOCKREEL;
                //Quantity = reelQuantity.ToString();
            }
            catch
            {
                _dialogService.ShowMessage("ce produit n'existe pas dans ce depo", true);
                return;
            }

            try
            {
                if(Quantity.Equals(null)){
                    _dialogService.ShowMessage("Erreur : veillez saisir un code valid ", true);
                    return;
                }
                if (int.Parse(Quantity) < 0)
                {
                    _dialogService.ShowMessage("Erreur : quantite doit être supérieur à 0 ", true);
                    return;
                }
                if (int.Parse(pht) < 0)
                {
                    _dialogService.ShowMessage("Erreur : prix doit être supérieur à 0 ", true);
                    return;
                }
                var comp = _restService.getPieceDiversNumber().Result + numligne;
                numeroPiece = numauto.NUMSOUCHE + "000" + comp;
                numligne++;
                StockLigne stockLigne = new StockLigne
                {
                    code = code,
                    designation = designation,
                    quantite = Quantity,
                    prix = pht,
                    depin = selectedDepot,
                    depout = null,
                    sense = 1,
                    article = article,
                    artfamilles_cpt = artfamilles_cpt,
                    artarifligne = artarifligne,
                    numpiece = numeroPiece,
                    numauto = numauto,
                    pIECE_NATURE = selectednature
                };
                Task.Run(async () =>
                {
                    if (await _dataServices.addStockLigneMEAsync(stockLigne))
                    {
                        _dialogService.ShowMessage("article " + code + " ajouter list avec succes", false);
                        MessagingCenter.Send(this, "ME");
                    }
                    else
                        _dialogService.ShowMessage("error", true);
                });
            }catch{

            }

        }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

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
                    }catch(Exception e){
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
                        if (article == null)
                        {
                            _dialogService.ShowMessage("code a barre indisponible ", true);
                            //return;
                        }
                        artfamilles_cpt = await _restService.GetARTFAMILLES_CPTbyARFID(article.ARTID.ToString());
                        artarifligne = await _restService.GetRTTARIFLIGNEbyARTID(article.ARTID.ToString());
                        code = article.ARTCODE;
                        designation = article.ARTDESIGNATION;
                        pht = artarifligne.ATFPRIX.ToString();
                    }
                    catch (Exception e)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            isBusy = false;
                            isEnabled = true;
                        });
                        //_dialogService.ShowMessage("Erreur" + e.Message, true);
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


        public StockMEPageModel(IRestServices _restService , IDataServices _dataServices , IDialogService _dialogService)
        {
            numligne = 1;
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
                //numligne = 1;
                nature = await _restService.GetPieceNaturebyPINID("19");
                depo = await _restService.GetDepot("o");
                selectedDepot = depo[0];
                selectednature = nature[0];
                numauto = await _restService.getNumPiecenyNature(selectednature.PINID.ToString());
                var comp = await _restService.getPieceDiversNumber() + numligne;
                numeroPiece = numauto.NUMSOUCHE + "000" + comp;
            });
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = false;
                isEnabled = true;

            });

        }
    }
}
