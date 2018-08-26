namespace PFE.Models
{
    using System;
    using System.Collections.Generic;


    public partial class PIECEDIVERS
    {
        public int PCDID { get; set; }

        public string PCDNUM { get; set; }


        public string PCDNUMEXT { get; set; }


        public string PITCODE { get; set; }

        public int PINID { get; set; }

        public string PINCODE { get; set; }

        public int EXEID { get; set; }

        public int NUMID { get; set; }

        public int? AFFID { get; set; }

        public int? TRFID { get; set; }

        public int DEPID_OUT { get; set; }

        public int DEPID_IN { get; set; }

        public int? EXPID { get; set; }


        public decimal PCDPOIDS { get; set; }

        public int PCDUNITEPOIDS { get; set; }

        public DateTime PCDDATEEFFET { get; set; }

        public DateTime? PCDDATE_DEBUT { get; set; }

        public DateTime? PCDDATE_FIN { get; set; }


        public string PCDISSOLDE { get; set; }


        public string PCDISACTIVE { get; set; }


        public string PCDISCLOS { get; set; }


        public string PCDISPRINT { get; set; }

        public short PCDNBPRINT { get; set; }

        public DateTime? PCDDATEPRINT { get; set; }

        public short? PCDNBIMPRESSION { get; set; }

        public int? MODID { get; set; }

        public decimal PCDMNTHT { get; set; }


        public decimal PCDMNTTTC { get; set; }

        public string USRMODIF { get; set; }

        public DateTime? DATEUPDATE { get; set; }

        public DateTime? DATECREATE { get; set; }

        public int? MEMOID { get; set; }

        public int SOCID { get; set; }



    }
}
