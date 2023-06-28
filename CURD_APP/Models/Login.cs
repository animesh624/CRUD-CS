using System.ComponentModel.DataAnnotations;

namespace CURD_APP.Models
{
    public class Login
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
