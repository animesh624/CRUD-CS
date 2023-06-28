using System.ComponentModel.DataAnnotations;

namespace CURD_APP.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
