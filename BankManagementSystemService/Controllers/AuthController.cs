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
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost, Route("authenticate")]
        public IActionResult Auth([FromBody] Users loginModel)
        {
            var IsUserExist = _tokenService.IsValidUser(loginModel);
            if (IsUserExist)
            {
                var token = _tokenService.GenerateToken(loginModel);
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpPost,Route("refresh")]
        public IActionResult Refresh(Tokens tokens)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokens.Access_Token);
            Users user = new Users();
            user.Name = principal.Identity?.Name;
            var token = _tokenService.GenerateRefreshToken(user);
            return Ok(token);
        }

    }
}
