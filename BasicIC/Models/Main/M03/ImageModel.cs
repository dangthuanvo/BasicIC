using System;
using System.ComponentModel.DataAnnotations;

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