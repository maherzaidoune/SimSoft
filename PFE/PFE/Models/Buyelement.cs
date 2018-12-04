using System;
namespace PFE.Models
{
    public class Buyelement
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
        public PIECEACHAT pIECEACHAT
        {
            get;
            set;
        }
        public PIECEACHATLIGNE pIECEVENTELIGNE
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
    }
}
