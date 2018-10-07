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

        public LoginPageModel(IDialogService _dialogService,IRestServices _restServices)
        {
            this._dialogService = _dialogService;
            this._restServices = _restServices;
        }

        public bool isEnabled { get; set; }
        public bool loading { get; set; }


        public ICommand validate => new Command(login);
        private void login(object obj)
        {

            if(selectedrole == null || selecteduser == null)
            {
                _dialogService.ShowMessage("make sure you select a valid user and role ", true);
            }
            else
            {
                if (selecteduser.USRPWD == password)
                {
                    _dialogService.ShowMessage("Login success : " + selecteduser.USRLOGIN, false);
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
                    _dialogService.ShowMessage("Invalid Password " + selecteduser.USRLOGIN, true);
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
                    _user = await _restServices.GetUserByGroupIdAsync(_selectedrole.CODEGRP.ToString());
                }).Wait();
                if (_user != null)
                {
                    isEnabled = true;
                    loading = false;
                }

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
                    try
                    {
                        if (!_restServices.testServer())
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
                                _dialogService.ShowMessage("server down ", true);
                        }
                        isEnabled = false;
                        loading = false;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(e.Message);
                        _dialogService.ShowMessage("ERROR" + ex.StackTrace, true);
                        _role = null;
                        _user = null;
                        isEnabled = false;
                        loading = false;
                    }

                });

            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

        }
    }
}
