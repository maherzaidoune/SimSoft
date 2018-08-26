using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFE.Models
{
  public  class UTILISATEUR
    {
      
        public int? USRID { get; set; }

      
        public string USRLOGIN { get; set; }

        public int PROID { get; set; }

        public long USERGRP { get; set; }

        public string USRNOM { get; set; }

      
        public string USRPRENOM { get; set; }

     
        public string USRPOSTE { get; set; }

        public string USRTELEPHONE { get; set; }

       
        public string USRMAIL { get; set; }

       
        public string USRRESERVE { get; set; }

      
        public string USRORGANISATION { get; set; }

        public int? SOCID { get; set; }

        public int? TIRID { get; set; }

        public int? TIRIDREP { get; set; }

      
        public string USRISACTIF { get; set; }

       
        public string USRISRESPONSABLE { get; set; }

     
       
        public string USRISNOMADE { get; set; }

        public int? USRCOLOR { get; set; }

        public int? LAGID { get; set; }

   
        public string USRPWD { get; set; }
    }
}