using BankManagementSystemService.Data;
using BankManagementSystemService.Middleware.Error;
using BankManagementSystemService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BankManagementSystemService.Repositories.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration iconfiguration;
        private readonly BankDBContext _bankDBContext;

        public TokenService(BankDBContext bankDBContext, IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
            this._bankDBContext = bankDBContext;
        }
        public Tokens GenerateToken(Users user)
        {
            return GenerateJWTTokens(user);
        }

        public Tokens GenerateRefreshToken(Users user)
        {
            return GenerateJWTTokens(user);
        }

        public Tokens GenerateJWTTokens(Users user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Name == "admin" ? "admin" : "customer")
                  }),
                    Expires = DateTime.Now.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens { Access_Token = tokenHandler.WriteToken(token), Refresh_Token = refreshToken };
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public bool IsValidUser(Users user)
        {
            try
            {
                bool IsUserExist = _bankDBContext.Customer.Where(x => x.Username == user.Name && x.Password == user.Password).Any();
                return IsUserExist;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
            finally
            {
                if (_bankDBContext != null)
                    _bankDBContext.Dispose();
            }
        }
    }
}
