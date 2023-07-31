using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ProductModel : BaseModel
    {
        public string product_name { get; set; }
        public decimal price { get; set; }
        public int inventory { get; set; }
    }
}