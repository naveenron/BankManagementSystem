using BankManagementSystemService.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace BankManagementSystemService.Repositories.Auth
{
    public interface ITokenService
    {
        Tokens GenerateToken(string userName);
        Tokens GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
