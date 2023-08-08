using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class MenuModel : BaseModel
    {
        [Required]
        public Guid parent_menu_id { get; set; }
        public string menu_name { get; set; }
        public int is_leaf { get; set; }
    }
}