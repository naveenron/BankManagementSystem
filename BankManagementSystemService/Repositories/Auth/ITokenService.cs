using BankManagementSystemService.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace BankManagementSystemService.Repositories.Auth
{
    public interface ITokenService
    {
        Tokens GenerateToken(Users user);
        Tokens GenerateRefreshToken(Users user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        bool IsValidUser(Users user);
    }
}
