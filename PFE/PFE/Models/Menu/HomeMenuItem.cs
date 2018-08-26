using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFE.Models.Menu
{
    public enum MenuType
    {
        Home,
        ListLieu,
        MonProfil,
        ListGuide,
        Favoris,
        AjoutLieu,
        Parametre,
        SeDeconnecter,
        SeConnecter

    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Parametre;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}

