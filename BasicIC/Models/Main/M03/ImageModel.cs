using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ImageModel : BaseModel
    {
        [Required]
        public Guid product_id { get; set; }
        public string image_url { get; set; }
        public int display_order { get; set; }
    }
}