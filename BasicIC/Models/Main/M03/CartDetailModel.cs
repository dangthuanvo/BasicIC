using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class CartDetailModel : BaseModel
    {
        public Guid cart_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public decimal cart_total_price { get; set; }
    }
}