using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class ImportModel : BaseModel
    {
        [Required]
        public Guid supplier_id { get; set; }
        public DateTime arrive_date { get; set; }
        public decimal total_price { get; set; }
    }
}