﻿using System;
namespace PFE.Models
{
    public class SellElements
    {
        public PIECE_NATURE pIECE_NATURE
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
        public Articles articles
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

    }
}