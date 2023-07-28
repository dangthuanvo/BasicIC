using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class OrderDetailModel : BaseModel
    {
        public Guid order_id { get; set; }
        public Guid product_id { get; set; }
        public decimal product_price { get; set; }
        public int quantity { get; set; }
        public decimal total_price { get; set; }
    }
}