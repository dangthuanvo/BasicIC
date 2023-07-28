using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class RatingModel : BaseModel
    {
        public Guid customer_id { get; set; }
        public Guid product_id { get; set; }
        public int star { get; set; }
        public string comment { get; set; }
    }
}