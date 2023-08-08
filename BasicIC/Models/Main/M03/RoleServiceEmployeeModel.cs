using System;
using System.ComponentModel.DataAnnotations;

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