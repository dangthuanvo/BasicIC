using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class RatingModel : BaseModel
    {
        [Required]
        public Guid customer_id { get; set; }
        [Required]
        public Guid product_id { get; set; }
        public int star { get; set; }
        public string comment { get; set; }
    }
}