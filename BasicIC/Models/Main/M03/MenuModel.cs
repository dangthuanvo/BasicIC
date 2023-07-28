using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class MenuModel : BaseModel
    {
        public Guid parent_menu_id { get; set; }
        public string menu_name { get; set; }
        public int is_leaf { get; set; }
    }
}