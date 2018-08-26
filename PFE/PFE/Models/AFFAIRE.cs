using System;
namespace PFE.Models
{
    public class AFFAIRE
    {
        public int? AFFID { get; set; }
        public string AFFCODE { get; set; }
        public string AFFINTITULE { get; set; }
        public int? MEMOID { get; set; }
        public int? TIRID { get; set; }
        public string AFFISACTIF { get; set; }
        public string AFFCRITREGROUPE { get; set; }
        public int? MODID { get; set; }
        public DateTime DATECREATE { get; set; }
        public DateTime DATEUPDATE { get; set; }
        public string USRMODIF { get; set; }
    }
}
