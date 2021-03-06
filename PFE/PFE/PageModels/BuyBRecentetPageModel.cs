﻿using System;
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
    public class BuyBRecentetPageModel : FreshMvvm.FreshBasePageModel
    {

        public IList<PIECE_NATURE> nature { get; set; }

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
                        var comp = await _restService.getPieceAchat() + 1;
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
            Device.BeginInvokeOnMainThread(async () => {
                await CoreMethods.PushPageModel<CTieresPageModel>("F");
                RaisePropertyChanged();
            });
            MessagingCenter.Subscribe<CTieresPageModel, TIERS>(this, "tiers", getTiers);
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

        public string numeroPiece { get; set; }
        public DateTime date { get; set; }

        public string numerofournisseur { get; set; }
        public string fournisseurintitule { get; set; }
        public string Codeaffaire { get; set; }
        public string affaireintitule { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }

        public ICommand quit => new Command(_quit);

        private void _quit(object obj)
        {
            Application.Current.MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AdminMenuPageModel>());
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                try{
                    nature = await _restService.GetPieceNature("A", "B", null, "1");
                    selectednature = nature[0];
                    numauto = await _restService.getNumPiecenyNature(selectednature.PINID.ToString());
                    var comp = await _restService.getPieceAchat() + 1;
                    numeroPiece = numauto.NUMSOUCHE + "000" + comp;
                }catch{
                    _dialogService.ShowMessage("erreur", true);
                }

            });
            isBusy = false;
            isEnabled = true;
        }
        public ICommand validate => new Command(_validate);

        private IDataServices _dataService;
        private IDialogService _dialogService;
        private int numligne;

        private void _validate(object obj)
        {
            if (Tiers == null)
            {
                _dialogService.ShowMessage("veuillez choisir un tiers ", true);
                return;
            }
            var comp = _restService.getPieceAchat().Result + 1;

            Buyelement buy = new Buyelement
            {
                pIECE_NATURE = selectednature,
                type = "BRE",
                affaire = affaires,
                tiers = Tiers,
                numpiece = numeroPiece,
                numauto = numauto,
                count = comp
            };
            Task.Run(async () =>
            {
                if (await _dataService.addBuyElementAsync(buy))
                    _dialogService.ShowMessage("Ajouter au details avec success", false);
            });
        }

        public BuyBRecentetPageModel(IRestServices _restService, IDataServices _dataService, IDialogService _dialogService)
        {
            date = DateTime.Today;
            this._dataService = _dataService;
            this._dialogService = _dialogService;
            this._restService = _restService;
        }

    }
}
