using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class OrderModel : BaseModel
    {
        public Guid customer_id { get; set; }
        public Guid employee_id { get; set; }
        public Guid addresses_id { get; set; }
        public string customer_phone_number { get; set; }
        public decimal total_price { get; set; }
        public decimal total_price_coupon { get; set; }
        public int payment_method { get; set; }
        public DateTime arrived_date { get; set; }
        public string shipping_address { get; set; }
        public int shipping_status { get; set; }
        public int shipping_fee { get; set; }
        public int order_payment_collection { get; set; }
        public string note { get; set; }
    }
}