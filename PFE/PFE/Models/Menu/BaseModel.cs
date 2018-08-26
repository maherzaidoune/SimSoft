using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFE.Models.Menu
{
    public class BaseModel
    {
        public BaseModel()
        {
        }

        public string Title { get; set; }
        public string Details { get; set; }
        public int Id { get; set; }

    }
}


