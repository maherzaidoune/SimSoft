using PFE.Models;
using PFE.Services;
using Plugin.Connectivity;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class LoginPageModel : FreshMvvm.FreshBasePageModel
    {
        private IDialogService _dialogService;
        public IRestServices _restServices;
        public string password { get; set; }
        public bool isBusy { get; set; }
        public bool isEnabled { get; set; }
        public LoginPageModel(IDialogService _dialogService,IRestServices _restServices)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
        }

        public bool loading { get; set; }


        public ICommand validate => new Command(login);
        private void login(object obj)
        {

            if(selectedrole == null || selecteduser == null)
            {
                _dialogService.ShowMessage("verifiez vos donnees ", true);
            }
            else
            {
                if (selecteduser.USRPWD == password)
                {
                    _dialogService.ShowMessage(selecteduser.USRLOGIN + " connecté ", false);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        switch (selectedrole.CODEGRP)
                        {
                            case 50:
                                await CoreMethods.PushPageModel<AdminMenuPageModel>();
                                RaisePropertyChanged();
                                break;
                            case 51:
                                await CoreMethods.PushPageModel<SellerMenuPageModel>();
                                RaisePropertyChanged();
                                break;
                            case 10048:
                                await CoreMethods.PushPageModel<StockManPageModel>();
                                RaisePropertyChanged();
                                break;
                            default:
                                _dialogService.ShowMessage("ERROR", true);
                                break;
                        }
                    });
                    Helper.Session.user = selecteduser;
                    Helper.Session.group = selectedrole;
                }
                else
                {
                    _dialogService.ShowMessage("mot de passe incorrect " + selecteduser.USRLOGIN, true);
                }
            }
             
        }

        public ICommand quit => new Command(quitter);

        private void quitter(object obj)
        {
            throw new NotImplementedException();
        }


        private UTILISATEURSGRP _selectedrole;
        public UTILISATEURSGRP selectedrole { get {
                return _selectedrole;
            } set {
                _selectedrole = value;
                loading = true;
                Task.Run(async () =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        isEnabled = false;
                        isBusy = true;
                    }); 
                    try
                    {
                        _user = await _restServices.GetUserByGroupIdAsync(_selectedrole.CODEGRP.ToString());
                    }catch{
                        _dialogService.ShowMessage("erreur inattendue", true);
                        _user = new List<UTILISATEUR>();
                    }
                }).Wait();
                if (_user != null)
                {
                    loading = false;
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    isBusy = false;
                    isEnabled = true;
                });


            }
        }
      

        public UTILISATEUR selecteduser { get; set; }
        

        //mocking role and user  list
        public IList<UTILISATEURSGRP> _role { get; set; }
        public IList<UTILISATEUR> _user { get; set; }

        //public override void ReverseInit(object returnedData)
        //{
        //    base.ReverseInit(returnedData);
        //    Console.WriteLine("reverse init");
        //}

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (!CrossConnectivity.Current.IsConnected)
            {
                var _dialog = new DialogService();
                _dialog.ShowMessage("Verifier votre connection internet", true);
                return;
            }
            else
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
                        if (!await _restServices.testServer())
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await CoreMethods.PushPageModel<ConfiPageModel>();
                                RaisePropertyChanged();
                            });

                        }
                        else
                        {
                            _role = await _restServices.GetGroupAsync();
                            // _user = await _restServices.GetUserByGroupIdAsync(selectedrole.CODEGRP.ToString());
                            if (_role == null)
                                _dialogService.ShowMessage("le serveur a rencontré une erreur interne ", true);
                        }
                        loading = false;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(e.Message);
                        _dialogService.ShowMessage("erreur : " + ex.StackTrace, true);
                        _role = null;
                        _user = null;
                        loading = false;
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
            isBusy = false;
            isEnabled = true;
        }
    }
}
