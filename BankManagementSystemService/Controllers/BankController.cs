using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Repositories.LoanModule;
using BankManagementSystemService.Repositories.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystemService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoanService _loanService;
        public BankController(IRegisterService registerService, ILoanService loanService)
        {
            _registerService = registerService;
            _loanService = loanService;
        }

        [AllowAnonymous]
        [HttpPost, Route("create-account")]
        public IActionResult CreateAccount([FromBody]Customer customer)
        {
            var result = _registerService.CreateAccount(customer);
            return Ok(result);
        }
        [HttpPost, Route("apply-loan")]
        public IActionResult ApplyLoan([FromBody] Loan loan)
        {
            var result = _loanService.ApplyLoan(loan);
            return Ok(result);
        }
    }
}
