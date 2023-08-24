using System;

namespace BasicIC.Models.Main.M03
{
    public class CustomerModel : BaseModel
    {
        public string customer_name { get; set; }
        public int gender { get; set; }
        public string phone_number { get; set; }
        public string account_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime? last_login { get; set; }
        public CustomerModel() { }
        public CustomerModel(Guid id) { this.id = id; }
    }
}