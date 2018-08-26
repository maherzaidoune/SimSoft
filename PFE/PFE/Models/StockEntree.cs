using System;
using System.Collections.Generic;
using System.Text;


namespace PFE.Models
{

    public class StockEntree
    {
        public int ARTID { get; set; }
        public decimal ARTmontant { get; set; }
        public int DEPID { get; set; }
        public decimal QteColise { get; set; }
        public string ARTCODEBARRE { get; set; }
        public string ARTCODE { get; set; }
        public string ARTDESIGNATION { get; set; }
        public string DoPiece { get; set; }
        public decimal ARDSTOCKREEL { get; set; }
        public string DEPINTITULE { get; set; }
        public string DEPCODE { get; set; }
        public string PINCODE { get; set; }
        public int PINID { get; set; }
        public string PINLIBELLE { get; set; }
        public int PCDID { get; set; }
        public decimal NV_Qte_C { get; set; }
        public decimal QteNV { get; set; }
        public decimal Qte_phy {get ; set ; }
        public String USRLOGIN { get; set; }
    }
    }
