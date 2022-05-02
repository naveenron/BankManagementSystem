using BankManagementSystemService.Data;
using BankManagementSystemService.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BankManagementSystemService.Repositories.LoanModule
{
    public class LoanService : ILoanService
    {
        private readonly ILogger _logger;
        private readonly BankDBContext _bankDbContext;
        public LoanService(BankDBContext bankDbContext, ILogger<LoanService> logger)
        {
            _bankDbContext = bankDbContext;
            _logger = logger;
        }
        public Loan ApplyLoan(Loan loan)
        {
            _bankDbContext.Loan.Add(loan);
            _bankDbContext.SaveChanges();
            return loan;
        }
    }
}
