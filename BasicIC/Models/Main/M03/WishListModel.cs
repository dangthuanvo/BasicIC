using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class WishListModel : BaseModel
    {
        [Required]
        public Guid customer_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
    }
}