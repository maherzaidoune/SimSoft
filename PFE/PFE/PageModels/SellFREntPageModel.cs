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
    public class SellFREntPageModel : FreshMvvm.FreshBasePageModel
    {
        PIECE_NATURE _selectednature
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

                try
                {
                    numauto = _restService.getNumPiecenyNature(value.PINID.ToString());
                    var comp = numauto.NUMCOMPTEUR + 1;
                    numeroPiece = numauto.NUMSOUCHE + "000" + comp;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
        public IList<PIECE_NATURE> nature { get; set; }
        public string numeroPiece { get; set; }
        public DateTime date { get; set; }

        public string reference { get; set; }
        public string intitule { get; set; }
        public string representant { get; set; }
        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            App.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
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
                affaires = arg2;
                representant = arg2.AFFINTITULE;
            });
        }

        private IRestServices _restService;
        private void _tiers(object obj)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await CoreMethods.PushPageModel<CTieresPageModel>();
                RaisePropertyChanged();
            });
            MessagingCenter.Subscribe<CTieresPageModel, TIERS>(this, "tiers", getTiers);
        }

        private void getTiers(CTieresPageModel arg1, TIERS arg2)
        {
            Console.WriteLine("tiers recieved");
            Device.BeginInvokeOnMainThread(() =>
            {
                Tiers = arg2;
                reference = arg2.TIRREFFOUCLI;
                intitule = arg2.TIRSOCIETE;
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

        private void _validate(object obj)
        {
            SellElements sell = new SellElements
            {
                pIECE_NATURE = selectednature,
                type = "SFR",
                affaire = affaires,
                tiers = Tiers
            };
            _dataService.addSellElementAsync(sell);
        }

        private IDataServices _dataService;


        public SellFREntPageModel(IRestServices _restService, IDataServices _dataService)
        {
            this._restService = _restService;
            this._dataService = _dataService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                nature = await _restService.GetPieceNature("v", "f", "%avoir%", null, true);
            });
        }
    }
}
