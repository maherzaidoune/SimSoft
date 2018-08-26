using PFE.Models;
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
    class GroupManPageModel : FreshMvvm.FreshBasePageModel
    {
        //public UTILISATEURSGRP _group { get; set; }
        public UTILISATEURSGRP selectedGroup { get; set; }
        public string group { get; set; }

        public ObservableCollection<UTILISATEURSGRP> groupeList { get; set; }


        public ICommand validate => new Command(_validate);
        public ICommand modifier => new Command(_modifier);
        public ICommand delete => new Command(_delete);
        public bool refreshing { get; set; }

        private void _delete(object obj)
        {
            groupeList.Remove(selectedGroup);
        }

        private void _modifier(object obj)
        {
            group = "";
        }

        private void _validate(object obj)
        {
            if(!string.IsNullOrEmpty(group))
                groupeList.Add(new UTILISATEURSGRP() { INTITULEGRP = group });
        }


        public GroupManPageModel()
        {
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            groupeList = new ObservableCollection<UTILISATEURSGRP>
        {
            new UTILISATEURSGRP(){ INTITULEGRP = "Administrator"},
            new UTILISATEURSGRP(){ INTITULEGRP = "User"}
        };
        }
    }
}
