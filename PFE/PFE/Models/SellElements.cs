using System;
namespace PFE.Models
{
    public class SellElements
    {
        public PIECE_NATURE pIECE_NATURE
        {
            get;
            set;
        }
        public TVA tva
        {
            get;
            set;
        }
        public PIECEVENTE pIECEVENTE
        {
            get;
            set;
        }
        public PIECEVENTELIGNE pIECEVENTELIGNE
        {
            get;
            set;
        }
        public ARTICLE articles
        {
            get;
            set;
        }
        public depot depot
        {
            get;
            set;
        }
        public TIERS tiers
        {
            get;
            set;
        }
        public ARTTARIFLIGNE artarifligne
        {
            get;
            set;
        }
        public float mutht
        {
            get;
            set;
        }
        public float mtht
        {
            get;
            set;
        }
        public float mttc
        {
            get;
            set;
        }
        public AFFAIRE affaire
        {
            get;
            set;
        }
        public int LivredQuantity
        {
            get;
            set;
        }
        public string remise
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        public bool ligneUpdated
        {
            get;
            set;
        }
        public string numpiece
        {
            get;
            set;
        }
        public int count
        {
            get;
            set;
        }
        public NUMAUTO numauto
        {
            get;
            set;
        }
    }
}
