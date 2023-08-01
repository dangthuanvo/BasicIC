using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class SupplierModel : BaseModel
    {
        public string supplier_name { get; set; }
        public string supplier_email { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
    }
}