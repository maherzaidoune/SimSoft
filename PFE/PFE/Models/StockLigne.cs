using System;
namespace PFE.Models
{
    public class StockLigne
    {
        public string code
        {
            get;
            set;
        }
        public string designation
        {
            get;
            set;
        }
        public string quantite
        {
            get;
            set;
        }
        public string prix
        {
            get;
            set;
        }
        public depot depin
        {
            get;
            set;
        }
        public depot depout
        {
            get;
            set;
        }
        public int sense
        {
            get;
            set;
        }
        public NUMAUTO numauto
        {
            get;
            set;
        }
      public ARTICLE article
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
        public string numpiece
        {
            get;
            set;
        }
        public PIECE_NATURE pIECE_NATURE
        {
            get;
            set;
        }
    }
}
