namespace PFE.Models
{
    using System;
  

    public partial class OPERATIONSTOCK
    {
        
        public int OPEID { get; set; }

        public DateTime? DATECREATE { get; set; }

        public DateTime? DATEUPDATE { get; set; }

   
        public string USRMODIF { get; set; }

        public DateTime OPEDATE { get; set; }

        public int PCID { get; set; }

        public int PLID { get; set; }

     
        public string OPEREFPIECE { get; set; }

        public string PICCODE { get; set; }

        public int PINID { get; set; }

        public int ARTID { get; set; }

        public int DEPID { get; set; }

        public int? PROID { get; set; }

        public string OPENATURESTOCK { get; set; }

        public string ARDEMPLACEMENT { get; set; }

     
        public string OPENUMLOT { get; set; }

     
        public string OPENUMSERIE { get; set; }

       
        public decimal OPEQUANTITE { get; set; }

        public short OPESENS { get; set; }

       
        public decimal OPELASTPA { get; set; }

        public decimal OPEPMP { get; set; }

        public decimal OPECUMP { get; set; }

        public decimal OPEQTERESTANTE { get; set; }

     
        public string OPEISCLOS { get; set; }

        public int? OPEIDORG { get; set; }

        public int? TIRID { get; set; }

        public decimal? OPESTOCKAVANT { get; set; }

        public decimal? OPEPUNET { get; set; }

    
        public string OPEINTITULE { get; set; }

        public int SOCID { get; set; }

        public DateTime? OPEFEFOFABRICATION { get; set; }

        public DateTime? OPEFEFOPEREMPTION { get; set; }

   
        public string OPEFEFODIVERS { get; set; }

     
        public string OPECOMPUTER { get; set; }

     
        public string OPETYPE { get; set; }

      
        public string OPEISMAJPA { get; set; }

        public int CTMID { get; set; }

  
        public decimal OPEFRAIS { get; set; }

       
        public decimal OPEPRMP { get; set; }

        public decimal OPECRUMP { get; set; }


        public string OPEREFEXTPIECE { get; set; }

       
        public string OPEFEFODIVERS1 { get; set; }

       
        public string OPEFEFODIVERS2 { get; set; }


        public string OPEFEFODIVERS3 { get; set; }

       
        public string OPEISBLOQUE { get; set; }
    }
}
