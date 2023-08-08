using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicIC.Models.Main.M03
{
    public class RoleServiceEmployeeModel : BaseModel
    {
        [Required]
        public Guid roleservice_id { get; set; }
        [Required]
        public Guid employee_id { get; set; }
    }
}