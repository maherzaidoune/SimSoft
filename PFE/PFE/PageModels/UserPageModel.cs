using PFE.Models;
using PFE.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    class UserPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restService;
        private IDialogService _diqlogServices;
        public string username { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public bool enabled
        {
            get;
            set;
        }
        private UTILISATEUR _selectedUser;
        public UTILISATEUR selectedUser { 
            get{
                return _selectedUser;
            }
            set{
                login = value.USRLOGIN;
                username = value.USRNOM;
                pass = value.USRPWD;
                //selectedrole.CODEGRP = (int)value.USERGRP;
                foreach(UTILISATEURSGRP role in _role){
                    if (role.CODEGRP == value.USERGRP)
                        selectedrole = role;
                }
                _selectedUser = value;
            } 
        }
        public IList<UTILISATEURSGRP> _role { get; set; }
        public IList<UTILISATEUR> _users { get; set; }
        public UTILISATEURSGRP selectedrole { get; set; }
        public IList<UTILISATEUR> _newusers { get; set; }
        public ObservableCollection<UTILISATEUR> userList { get; set; }
        public ICommand validate => new Command(_validate);
        public ICommand modifier => new Command(_modifier);
        public ICommand delete => new Command(_delete);
        public ICommand save => new Command(_save);

        private void _save(object obj)
        {

            if(_newusers == null){
                _newusers = new List<UTILISATEUR>();
                _diqlogServices.ShowMessage("aucun nouveau utilisateur ", false);
                return;
            }
            if (_newusers.Count > 0){
                Task.Run(async () =>
                {
                    try
                    {
                        if (await _restService.addUsers(_newusers))
                        {
                            _diqlogServices.ShowMessage("utilisateur ajoute avec succes", false);
                            _delete(null);
                            userList = null;
                        }
                    }
                    catch (Exception e)
                    {
                        _diqlogServices.ShowMessage("Error " + e.Message, true);

                    }
                });
               
            }

        }

        private void _delete(object obj)
        {
            userList.Remove(selectedUser);
            Task.Run(async () =>
            {
                if (!_newusers.Contains(selectedUser))
                {
                    if (await _restService.DeleteUser((int)selectedUser.USRID))
                        _diqlogServices.ShowMessage(selectedUser.USRNOM + " a ete effacee du base de donnee avec succes ", false);
                }
            });

        }

        private void _modifier(object obj)
        {
            try{
                if (selectedUser == null)
                {
                    _diqlogServices.ShowMessage("veuillez sélectionner l'utilisateur a supprimer ", true);
                }
                else if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(username) || String.IsNullOrEmpty(pass))
                {
                    _diqlogServices.ShowMessage("Login , Nom utilisateur et mot de passe doivent etre non null ", true);
                }
                else
                {
                    selectedUser.USRLOGIN = login;
                    selectedUser.USRNOM = username;
                    selectedUser.USRPWD = pass;
                    selectedUser.USERGRP = selectedrole.CODEGRP;
                    if (!_newusers.Contains(selectedUser))
                    {
                        Task.Run(async () =>
                        {
                            if (await _restService.UpdateUser(selectedUser, (int)selectedUser.USRID))
                            {
                                _diqlogServices.ShowMessage(selectedUser.USRNOM + " a ete modifiee avec succes", false);
                            }
                        });
                    }
                }
            }
            catch{
                _diqlogServices.ShowMessage("Login , Nom utilisateur et mot de passe doivent etre non null ", true);
            }

        }

        private void _validate(object obj)
        {
            _newusers.Add(new UTILISATEUR()
            {
                USRLOGIN = login,
                USRNOM = username,
                USRPWD = pass,
                USERGRP = selectedrole.CODEGRP
            });
            userList.Add(new UTILISATEUR()
            {
                USRLOGIN = login,
                USRNOM = username,
                USRPWD = pass,
                USERGRP = selectedrole.CODEGRP
            });
            if (!enabled)
                enabled = true;
        }

        public UserPageModel(IRestServices _restService, IDialogService _diqlogServices)
        {
            this._restService = _restService;
            this._diqlogServices = _diqlogServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            enabled = false;
            Task.Run(async () =>
                {
                    _role = await _restService.GetGroupAsync();
                    _users = await _restService.GetUserAsync();
            }).Wait();
            userList = new ObservableCollection<UTILISATEUR>(_users);
            _newusers = new List<UTILISATEUR>();
        }
    }
}
