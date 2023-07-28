using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ProductAttributeModel : BaseModel
    {
        public Guid attribute_id { get; set; }
        public Guid product_id { get; set; }
        public string attribute_value { get; set; }
    }
}