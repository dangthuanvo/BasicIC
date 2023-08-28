using BasicIC.Models.Main.M03;
using System.Collections.Generic;

namespace BasicIC.Models.Main
{
    public class OrderConfirmEmailModel
    {
        public string order_id { get; set; }
        public string customer_name { get; set; }
        public string customer_phone_number { get; set; }
        public decimal total_price { get; set; }
        public string shipping_address { get; set; }
        public int shipping_fee { get; set; }
        public List<OrderDetailModel> orderDetailModel { get; set; } = new List<OrderDetailModel>();
        public string to_email { get; set; }
        public string subject { get; set; }

        public OrderConfirmEmailModel(string order_id, string customer_name, string customer_phone_number, decimal total_price, string shipping_address, int shipping_fee, List<OrderDetailModel> orderDetailModel, string to_email, string subject)
        {
            this.order_id = order_id;
            this.customer_name = customer_name;
            this.customer_phone_number = customer_phone_number;
            this.total_price = total_price;
            this.shipping_address = shipping_address;
            this.shipping_fee = shipping_fee;
            this.orderDetailModel = orderDetailModel;
            this.to_email = to_email;
            this.subject = subject;
        }
    }
}