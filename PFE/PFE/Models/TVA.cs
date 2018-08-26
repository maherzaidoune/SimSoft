using System;
namespace PFE.Models
{
    public class TVA
    {
        public float? TVACODE { get; set; }
        public float? TVATAUX { get; set; }
        public string TVADEDUCTIBLE { get; set; }
        public string TVACOLLECTE { get; set; }
        public string TVAIMMO { get; set; }
        public string TVAACTIF { get; set; }
        public string TVAISDEBIT { get; set; }
        public string USRMODIF { get; set; }
        public DateTime? DATECREATE { get; set; }
        public DateTime? DATEUPDATE { get; set; }
        public string TVAENCAISSEMENT { get; set; }
        public string TVADECAISSEMENT { get; set; }
        public string TVAINTITULE { get; set; }
    }
}
