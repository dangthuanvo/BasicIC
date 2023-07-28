using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class CartDetailModel : BaseModel
    {
        public Nullable<System.Guid> cart_id { get; set; }
        public Nullable<System.Guid> product_id { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> cart_total_price { get; set; }
    }
}