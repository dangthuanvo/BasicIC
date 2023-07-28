using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class RoleServiceModel : BaseModel
    {
        public Guid role_id { get; set; }
        public Guid service_id { get; set; }
        public int access_level { get; set; }
    }
}