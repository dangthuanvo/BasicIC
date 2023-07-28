using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class RoleServiceEmployeeModel : BaseModel
    {
        public Guid roleservice_id { get; set; }
        public Guid employee_id { get; set; }
    }
}