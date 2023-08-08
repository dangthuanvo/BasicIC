using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class CouponOrderModel : BaseModel
    {
        [Required]
        public Guid coupon_id { get; set; }
        [Required]
        public Guid order_id { get; set; }
    }
}