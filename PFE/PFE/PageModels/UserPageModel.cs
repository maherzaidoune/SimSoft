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
        public UTILISATEUR selectedUser { get; set; }
        public IList<UTILISATEURSGRP> _role { get; set; }
        public UTILISATEURSGRP selectedrole { get; set; }
        public ObservableCollection<UTILISATEUR> userList { get; set; }
        public ICommand validate => new Command(_validate);
        public ICommand modifier => new Command(_modifier);
        public ICommand delete => new Command(_delete);
        public ICommand save => new Command(_save);

        private void _save(object obj)
        {
            try{
                if(_restService.addUsers(userList)){
                    _diqlogServices.ShowMessage("User list updated", false);
                    _delete(null);
                    userList = null;
                }
            }catch(Exception e)
            {
                _diqlogServices.ShowMessage("Error " + e.Message, true);

            }
        }

        private void _delete(object obj)
        {
            userList.Remove(selectedUser);
        }

        private void _modifier(object obj)
        {
            login = string.Empty;
            username = string.Empty;
            pass = string.Empty;
        }

        private void _validate(object obj)
        {
            //if(string.IsNullOrEmpty(login) && string.IsNullOrEmpty(username) && string.IsNullOrEmpty(pass))
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
                });
            userList = new ObservableCollection<UTILISATEUR>();
        }
    }
}
