using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class ImportDetailModel : BaseModel
    {
        [Required]
        public Guid import_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
    }
}