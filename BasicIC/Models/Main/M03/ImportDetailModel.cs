using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ImportDetailModel : BaseModel
    {
        public Guid import_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
    }
}