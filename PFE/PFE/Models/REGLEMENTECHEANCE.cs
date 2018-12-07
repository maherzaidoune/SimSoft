using System;
namespace PFE.Models
{
    public class REGLEMENTECHEANCE
    {
        public int ECHID { get; set; }
        public int PEREID { get; set; }
        public string PERECLASSE { get; set; }
        public int ECHNUMERO { get; set; }
        public int RGTID { get; set; }
        public string ECHLIBELLE { get; set; }
        public string ECHISLIBELLEAUTO { get; set; }
        public int ECHJOUR { get; set; }
        public string ECHTYPE { get; set; }
        public int ECHLE { get; set; }
        public int ECHTAUX { get; set; }
        public DateTime? ECHDATE { get; set; }
        public string PITCODE { get; set; }
    }
}
