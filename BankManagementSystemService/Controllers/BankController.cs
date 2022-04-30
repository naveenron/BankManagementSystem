using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        [HttpGet, Route("test")]
        public IActionResult Test()
        {
            return Ok("I am Up");
        }
    }
}
