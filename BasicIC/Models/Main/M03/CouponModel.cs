namespace BasicIC.Models.Main.M03
{
    public class CouponModel : BaseModel
    {
        public string coupon_name { get; set; }
        public string coupon_code { get; set; }
        public int is_active { get; set; }
        public int discount_type { get; set; }
        public decimal discount_amount { get; set; }
    }
}