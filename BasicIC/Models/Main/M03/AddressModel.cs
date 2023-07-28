using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class AddressModel : BaseModel
    {
        public Guid customer_id { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
    }
}