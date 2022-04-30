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

        [HttpPost, Route("authenticate")]
        public IActionResult Auth([FromBody] Users loginModel)
        {
            var token = tokenService.GenerateToken(loginModel.Name);
            return Ok(token);
        }

        [HttpPost,Route("refresh")]
        public IActionResult Refresh(Tokens tokens)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(tokens.Access_Token);
            var username = principal.Identity?.Name;

            var token = tokenService.GenerateRefreshToken(username);
            return Ok(token);
        }
    }
}
