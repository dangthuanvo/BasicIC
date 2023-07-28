using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class WishListModel : BaseModel
    {
        public Guid customer_id { get; set; }
        public Guid product_id { get; set; }
    }
}