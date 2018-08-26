using System;
namespace PFE.Models
{
    public class ARTDEPOT
    {
        public float? ARTID { get; set; }
        public float? DEPID { get; set; }
        public string ARDEMPLACEMENT { get; set; }
        public float? ARDSEUILMIN { get; set; }
        public float? ARDSEUILMAX { get; set; }
        public float? ARDSTOCKREEL { get; set; }
        public float? ARDSTOCKCDE { get; set; }
        public float? ARDSTOCKRSV { get; set; }
        public DateTime? ARDLASTDATEIN { get; set; }
        public DateTime? ARDLASTDATEOUT { get; set; }
        public float? ARDSTOCKFAB { get; set; }
        public float? ARDSTOCKSAV { get; set; }
        public float? ARDSTOCKCTM { get; set; }
        public string ARDISBLOQUE { get; set; }
    }
}
