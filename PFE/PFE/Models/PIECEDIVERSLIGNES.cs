namespace PFE.Models
{
    using System;


    public partial class PIECEDIVERSLIGNES
    {
       
        public int PLDID { get; set; }

        public int PCDID { get; set; }

        public int PLDNUMLIGNE { get; set; }

       
        public string PLDTYPE { get; set; }

        public DateTime PLDDATE { get; set; }

        public int? DEPID { get; set; }

        public int? AFFID { get; set; }

        public int? TIRIDFOU { get; set; }

        public int? PROID { get; set; }

        public int? ARTID { get; set; }

   
        public string ARTTYPE { get; set; }

      
        public string PLDISFORFAIT { get; set; }

     
        public string PLDISSOUMISESC { get; set; }

    
        public string PLDDESIGNATION { get; set; }

        public int? TVACODE { get; set; }

        public int? TPFCODE { get; set; }

        public int? CPTID { get; set; }

        public int? ANSID { get; set; }

        public decimal PLDQTE { get; set; }

        public decimal PLDQTEUS { get; set; }

        public decimal PLDQTETRANSFO { get; set; }
        
        public decimal PLDCOEFFUV { get; set; }

        public int? PLDIDORG { get; set; }

      
        public decimal PLDPUBRUT { get; set; }

        public decimal PLDPUNET { get; set; }


        public decimal PLDMNTNET { get; set; }


        public decimal PLDMNTNETHT { get; set; }

      
        public decimal PLDLASTPA { get; set; }

 
        public decimal PLDPMP { get; set; }

     
        public decimal PLDCUMP { get; set; }

 
        public string PLDREMISE_F { get; set; }

  
        public string PLDREMISE_T { get; set; }


        public decimal? PLDREMISE_MNT { get; set; }

        public int PLDSTOTID { get; set; }

        public int? PLDPEREID { get; set; }

    
        public decimal PLDFRAIS1 { get; set; }


        public string PLDFRAIS1T { get; set; }

     
        public decimal PLDFRAIS2 { get; set; }
        
        public string PLDFRAIS2T { get; set; }

        public decimal PLDFRAIS3 { get; set; }

        public string PLDFRAIS3T { get; set; }

    
        public decimal PLDFRAISTOTAL { get; set; }

      
        public decimal PLDPOIDS { get; set; }

        public int PLDUNITEPOIDS { get; set; }

  
        public string PLDDIVERS { get; set; }

       
        public string PLDCOMMENTAIRE { get; set; }

        public string PLDNUMLOT { get; set; }

  
        public string PLDNUMSERIE { get; set; }

        public DateTime? PLDFEFOFABRICATION { get; set; }

        public DateTime? PLDFEFOPEREMPTION { get; set; }

        public string PLDFEFODIVERS { get; set; }

        public decimal PLDFRAIS { get; set; }
        
        public string PLDFEFODIVERS1 { get; set; }

        public string PLDFEFODIVERS2 { get; set; }

        public string PLDFEFODIVERS3 { get; set; }

  
        public decimal? PLDLASTPR { get; set; }

        public decimal? PLDPRMP { get; set; }

    
        public decimal? PLDCRUMP { get; set; }

  


    }
}
