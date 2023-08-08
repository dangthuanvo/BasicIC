using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class OrderDetailModel : BaseModel
    {
        [Required]
        public Guid order_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
        public decimal product_price { get; set; }
        public int quantity { get; set; }
        public decimal total_price { get; set; }
    }
}