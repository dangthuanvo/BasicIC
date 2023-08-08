using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class CartDetailModel : BaseModel
    {
        [Required]
        public Guid cart_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public decimal cart_total_price { get; set; }
    }
}