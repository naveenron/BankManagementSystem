using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Repositories.LoanModule;
using BankManagementSystemService.Repositories.Registration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult CreateAccount([FromBody] Customer customer)
        {
            try
            {
                var result = _registerService.CreateAccount(customer);
                return Ok("Successfully Created !!!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, Route("apply-loan")]
        public IActionResult ApplyLoan([FromBody] Loan loan)
        {
            try
            {
                var result = _loanService.ApplyLoan(loan);
                return Ok("Loan Applied Successfully");
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet, Authorize(Roles = "admin"), Route("get-loan-details")]
        public IActionResult GetAllLoanDetails()
        {
            return Ok(_loanService.GetAllLoanDetails());
        }

        [HttpGet, Authorize(Roles = "admin"), Route("get-customer-details")]
        public IActionResult GetAccountDetails()
        {
            return Ok(_registerService.GetAccountDetails());
        }
    }
}
