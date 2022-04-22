using BankManagementSystemService.Models;
using BankManagementSystemService.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BankManagementSystemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;
        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.UserName),
            new Claim(ClaimTypes.Role, "Customer")
        };
            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();
            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost,Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            //var user = userContext.LoginModels.SingleOrDefault(u => u.UserName == username);
            //if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            //{
            //    return BadRequest("Invalid client request");
            //}
            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            //userContext.SaveChanges();
            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
