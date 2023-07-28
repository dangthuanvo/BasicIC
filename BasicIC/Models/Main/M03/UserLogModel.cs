using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class UserLogModel : BaseModel
    {
        public Guid user_id { get; set; }
        public int user_type { get; set; }
        public string activity_type { get; set; }
    }
}