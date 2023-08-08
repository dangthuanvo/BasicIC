using System;
using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main.M03
{
    public class RoleServiceModel : BaseModel
    {
        [Required]
        public Guid role_id { get; set; }
        [Required]
        public Guid service_id { get; set; }
        public int access_level { get; set; }
    }
}