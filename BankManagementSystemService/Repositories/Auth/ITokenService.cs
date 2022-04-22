using System.Collections.Generic;
using System.Security.Claims;

namespace BankManagementSystemService.Repositories.Auth
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
