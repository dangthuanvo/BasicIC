using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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