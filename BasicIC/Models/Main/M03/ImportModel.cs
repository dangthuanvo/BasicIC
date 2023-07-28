using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class ImportModel : BaseModel
    {
        public Guid supplier_id { get; set; }
        public DateTime arrive_date { get; set; }
        public decimal total_price { get; set; }
    }
}