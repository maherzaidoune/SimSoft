using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PFE.Models;
using PFE.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class AffEntetePageModel : FreshMvvm.FreshBasePageModel
    {
        public string search { get; set; }
        public string info { get; set; }
        public AFFAIRE affaire { get; set; }
        public ObservableCollection<AFFAIRE> affaireList { get; set; }
        private IRestServices _restServices;
        public ICommand find => new Command(_find);
        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {
            if (affaire != null)
                MessagingCenter.Send<AffEntetePageModel, AFFAIRE>(this, "affaire", affaire);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await CoreMethods.PopPageModel();
                RaisePageWasPopped();
                RaisePropertyChanged();
            });
        }

        private void _find(object obj)
        {

            IList<AFFAIRE> list = null;
            Task.Run(async () =>
            {
                try
                {
                    list = await _restServices.GetAffaire(search);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }).Wait();
            if (list != null)
                affaireList = new ObservableCollection<AFFAIRE>(list);

        }

        public AffEntetePageModel(IRestServices _restServices)
        {
            this._restServices = _restServices;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
