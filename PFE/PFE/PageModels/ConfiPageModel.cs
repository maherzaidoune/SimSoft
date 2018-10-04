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
    class ConfiPageModel : FreshMvvm.FreshBasePageModel
    {
        private IRestServices _restservices;
        public string ipServer
        {
            get;
            set;
        }
        public string db
        {
            get;
            set;
        }
        public string user
        {
            get;
            set;
        }
        public string passord
        {
            get;
            set;
        }
        public string port
        {
            get;
            set;
        }

        public ICommand valider => new Command(_valider);
        public ICommand tester => new Command(_tester);

        private void _tester(object obj)
        {
            _restservices.testServer(ipServer + ':' + port);
        }

        private void _valider(object obj)
        {
            if (_restservices.testServer(ipServer + ':' + port))
            {
                Helper.Config.URL = ipServer;
                Helper.Config.port = port;
                Task.Run(async () =>
                {
                    await CoreMethods.PopPageModel();
                    RaisePropertyChanged();
                    RaisePageWasPopped();
                });
            }
        }

        public ConfiPageModel(IRestServices _restservices)
        {
            this._restservices = _restservices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            ipServer = Helper.Config.URL;
            port = Helper.Config.port;
        }
    }
}
