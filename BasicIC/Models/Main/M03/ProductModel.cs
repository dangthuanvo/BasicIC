using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ProductModel : BaseModel
    {
        public Guid menu_id { get; set; }
        public Guid product_id { get; set; }
    }
}