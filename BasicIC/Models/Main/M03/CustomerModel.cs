using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class CustomerModel : BaseModel
    {
        public string customer_name { get; set; }
        public int gender { get; set; }
        public string phone_number { get; set; }
        public string account_name { get; set; }
        public string password { get; set; }
        public DateTime last_login { get; set; }
    }
}