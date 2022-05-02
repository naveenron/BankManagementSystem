using System.ComponentModel.DataAnnotations;

namespace BankManagementSystemService.Models
{
    public class Users
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
