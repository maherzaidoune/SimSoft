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
    class SellEntetePageModel : FreshMvvm.FreshBasePageModel
    {

        private PIECE_NATURE _selectednature
        {
            get;
            set;
        }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
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
                        var comp = await _restService.getPieceVente() + 1;
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
        public IList<PIECE_NATURE> nature { get; set; }
        public string numeroPiece { get; set; }
        public DateTime date { get; set; }

        public string numerofournisseur { get; set; }
        public string fournisseurintitule { get; set; }
        public string Codeaffaire { get; set; }
        public string affaireintitule { get; set; }
        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            Application.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }
        public ICommand tiers => new Command(_tiers);
        public ICommand affairs => new Command(_affairs);
        public NUMAUTO numauto
        {
            get;
            set;
        }

        private void _affairs(object obj)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await CoreMethods.PushPageModel<AffEntetePageModel>();
                RaisePropertyChanged();
            });
            MessagingCenter.Subscribe<AffEntetePageModel, AFFAIRE>(this, "affaire", getAff);
        }

        private void getAff(AffEntetePageModel arg1, AFFAIRE arg2)
        {
            Console.WriteLine("tiers recieved");
            Device.BeginInvokeOnMainThread(() =>
            {
                Codeaffaire = arg2.AFFCODE;
                affaireintitule = arg2.AFFINTITULE;
                affaires = arg2;
            });
        }

        private IRestServices _restService;
        private void _tiers(object obj)
        {
            Device.BeginInvokeOnMainThread(async()=>{
                await CoreMethods.PushPageModel<CTieresPageModel>("C");
                RaisePropertyChanged();
            });

            MessagingCenter.Subscribe<CTieresPageModel,TIERS>(this, "tiers", getTiers);
        }

        private void getTiers(CTieresPageModel arg1, TIERS arg2)
        {
            Console.WriteLine("tiers recieved");
            Device.BeginInvokeOnMainThread(() =>
            {
                numerofournisseur = arg2.TIRCODE;
                fournisseurintitule = arg2.TIRSOCIETE;
                Tiers = arg2;
            });

        }
        public AFFAIRE affaires
        {
            get;
            set;
        }

        public TIERS Tiers
        {
            get;
            set;
        }


        public ICommand validate => new Command(_validate);

        private IDataServices _dataService;
        private IDialogService _dialogService;

        private void _validate(object obj)
        {
            if (Tiers == null)
            {
                _dialogService.ShowMessage("veuillez choisir un tiers ", true);
                return;
            }
            var comp = _restService.getPieceVente().Result;
            SellElements sell = new SellElements
            {
                pIECE_NATURE = selectednature,
                type = "SBC",
                affaire = affaires,
                tiers = Tiers,
                numpiece = numeroPiece,
                numauto = numauto,
                count = comp
            };
            Task.Run(async () =>
            {
                if (await _dataService.addSellElementAsync(sell))
                    _dialogService.ShowMessage("Ajouter au details avec success", false);
            });
        }

        public SellEntetePageModel(IRestServices _restService , IDataServices _dataService , IDialogService _dialogService)
        {
            date = DateTime.Today;
            this._dataService = _dataService;
            this._dialogService = _dialogService;
            this._restService = _restService;
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
                try{
                    nature = await _restService.GetPieceNature("V", "C", "Commande", "1", true);
                    if (nature.Equals(null) || nature.Count == 0)
                    {
                        nature = await _restService.GetPieceNature("V", "C");
                    }
                    selectednature = nature[0];
                    numauto = await _restService.getNumPiecenyNature(selectednature.PINID.ToString());
                    var comp = await _restService.getPieceVente() + 1;
                    numeroPiece = numauto.NUMSOUCHE + "000" + comp;
                }catch{
                    _dialogService.ShowMessage("erreur", true);
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    isBusy = false;
                    isEnabled = true;
                });
            });
        }

       /* public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData == null)
                Console.WriteLine("tiers null");
            var data = returnedData as TIERS;
            Console.WriteLine("reverse init");
            numerofournisseur = data.TIRCODE;
            fournisseurintitule = data.TIRSOCIETE;
        }
        */

    }
}
