using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class CartModel : BaseModel
    {
        [Required]
        public Guid customer_id { get; set; }
    }
}