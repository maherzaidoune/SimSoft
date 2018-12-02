using FreshMvvm;
using PFE.Helper;
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
    class CTieresPageModel : FreshMvvm.FreshBasePageModel
    {
        public string search { get; set; }
        public string info { get; set; }
        public TIERS tiers { get; set; }
        public string type
        {
            get;
            set;
        }
        public ObservableCollection<TIERS> tiersList { get; set; }
        private IRestServices _restServices;
        public ICommand find => new Command(_find);
        public ICommand validate => new Command(_validate);
        public ICommand back => new Command(_back);

        private void _back(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePageWasPopped();
                RaisePropertyChanged();
            });
        }

        private void _validate(object obj)
        {
            if(tiers != null)
                MessagingCenter.Send<CTieresPageModel,TIERS> (this, "tiers",tiers);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePageWasPopped();
                RaisePropertyChanged();
            });
        }

        private void _find(object obj)
        {
            
            IList<TIERS> list = null;
            Task.Run(async () =>
                {
                    try
                    {
                    list = await _restServices.GetTiers(search,type);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                }).Wait();
            if (list != null)
                tiersList = new ObservableCollection<TIERS>(list);

        }

        public CTieresPageModel(IRestServices _restServices)
        {
            this._restServices = _restServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            if (initData != null)
                type = initData as string;
            else
                type = "C";
        }
    }
}
