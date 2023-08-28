using System.ComponentModel.DataAnnotations;

namespace BasicIC.Models.Main
{
    public class ConfirmAccountRequest
    {
        [Required]
        public string email { get; set; }
    }
}