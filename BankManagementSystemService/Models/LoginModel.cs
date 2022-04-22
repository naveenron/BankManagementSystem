
using System;

namespace BankManagementSystemService.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
