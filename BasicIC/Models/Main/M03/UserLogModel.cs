using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class UserLogModel : BaseModel
    {
        [Required]
        public Guid user_id { get; set; }
        public int user_type { get; set; }
        public string activity_type { get; set; }
    }
}