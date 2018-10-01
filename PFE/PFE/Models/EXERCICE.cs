using System;
namespace PFE.Models
{
    public class EXERCICE
    {
        public int? EXEID { get; set; }
        public string EXEINTITULE { get; set; }
        public string EXEETAT { get; set; }
        public DateTime? EXEDATEDEB { get; set; }
        public DateTime? EXEDATEFIN { get; set; }
        public int? JORIDAN { get; set; }
        public int? CPTIDBENEFICE { get; set; }
        public int? CPTIDPERTE { get; set; }
    }
}
