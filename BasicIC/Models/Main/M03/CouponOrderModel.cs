using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class CouponOrderModel : BaseModel
    {
        public Guid coupon_id { get; set; }
        public Guid order_id { get; set; }
    }
}