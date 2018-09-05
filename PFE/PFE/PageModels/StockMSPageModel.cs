using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using PFE.Models;
using PFE.Services;
using PropertyChanged;
using Xamarin.Forms;

namespace PFE.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class StockMSPageModel : FreshMvvm.FreshBasePageModel
    {

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
        public NUMAUTO numauto
        {
            get;
            set;
        }
        private IRestServices _restService;
        public IList<PIECE_NATURE> nature { get; set; }
        public string numeroPiece { get; set; }

        public IList<depot> depo { get; set; }
        private depot _selectedDepo;
        public depot selectedDepot
        {
            get
            {
                return _selectedDepo;
            }
            set
            {
                _selectedDepo = value;
                Task.Run(() =>
                {
                    storeQuantity = _restService.GetARTDEPOTbyDepid(value.DEPID.ToString()).ARDSTOCKREEL.ToString();
                });
            }
        }

        public string barreCode
        {
            get;
            set;
        }
        public string designation
        {
            get; set;
        }

        public TVA tva
        {
            get;
            set;
        }
        public ARTICLE article
        {
            get;
            set;
        }

        public string code
        {
            get;
            set;
        }

        public ARTFAMILLES_CPT artfamilles_cpt
        {
            get;
            set;
        }
        public ARTTARIFLIGNE artarifligne
        {
            get;
            set;
        }

        private string storeQuantity { get; set; }

        private string _pht;
        public string pht
        {
            get
            {
                return _pht;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _pht = value;
            }
        }

        //save
        public ICommand validate => new Command(_validate);

        private void _validate(object obj)
        {
            throw new NotImplementedException();
        }

        //get data by bc
        public ICommand valid => new Command(_valid);
        //scan br
        public ICommand Scan => new Command(_Scan);

        private void _Scan(object obj)
        {
            throw new NotImplementedException();
        }

        private void _valid(object obj)
        {
            if (!string.IsNullOrEmpty(barreCode))
            {
                Task.Run(() =>
                {
                    try
                    {
                        article = _restService.getArticlebyBC(barreCode);
                        code = article.ARTCODE;
                        if (selectedDepot != null)
                            storeQuantity = _restService.GetARTDEPOTbyDepid(selectedDepot.DEPID.ToString()).ARDSTOCKREEL.ToString();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }

                    designation = article.ARTDESIGNATION;
                    pht = artarifligne.ATFPRIX.ToString();
                });
            }

        }

        public StockMSPageModel(IRestServices _restService)
        {
            this._restService = _restService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Task.Run(async () =>
            {
                nature = await _restService.GetPieceNaturebyPINID("20");
                depo = await _restService.GetDepot("o");
            });
        }
    }
}
