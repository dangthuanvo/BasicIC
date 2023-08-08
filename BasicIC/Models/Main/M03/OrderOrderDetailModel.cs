using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class OrderOrderDetailModel : BaseModel
    {
        [Required]
        public Guid customer_id { get; set; }
        [Required]
        public Guid employee_id { get; set; }
        [Required]
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
        public List<OrderDetailModel> orderDetailModel { get; set; } = new List<OrderDetailModel>();

        public OrderOrderDetailModel(OrderModel orderModel = null, List<OrderDetailModel> listOrderDetailModel = null)
        {
            if (orderModel != null)
            {
                id = orderModel.id;
                customer_id = orderModel.customer_id;
                employee_id = orderModel.employee_id;
                addresses_id = orderModel.addresses_id;
                customer_phone_number = orderModel.customer_phone_number;
                total_price = orderModel.total_price;
                total_price_coupon = orderModel.total_price_coupon;
                payment_method = orderModel.payment_method;
                arrived_date = orderModel.arrived_date;
                shipping_address = orderModel.shipping_address;
                shipping_status = orderModel.shipping_status;
                shipping_fee = orderModel.shipping_fee;
                order_payment_collection = orderModel.order_payment_collection;
                note = orderModel.note;
                orderDetailModel = listOrderDetailModel;
                UpdateInfo(orderModel);
            }
        }
        public OrderModel CreateOrderModel()
        {
            OrderModel x = new OrderModel();
            x.id = id;
            x.customer_id = customer_id;
            x.employee_id = employee_id;
            x.addresses_id = addresses_id;
            x.customer_phone_number = customer_phone_number;
            x.total_price = total_price;
            x.total_price_coupon = total_price_coupon;
            x.payment_method = payment_method;
            x.arrived_date = arrived_date;
            x.shipping_address = shipping_address;
            x.shipping_status = shipping_status;
            x.shipping_fee = shipping_fee;
            x.order_payment_collection = order_payment_collection;
            x.note = note;
            x.create_by = create_by;
            x.create_time = create_time;
            x.tenant_id = tenant_id;
            x.modify_by = modify_by;
            x.modify_time = modify_time;
            return x;
        }
    }
}