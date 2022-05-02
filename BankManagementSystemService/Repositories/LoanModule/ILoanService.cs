using BankManagementSystemService.Data.Entities;

namespace BankManagementSystemService.Repositories.LoanModule
{
    public interface ILoanService
    {
        Loan ApplyLoan(Loan loan);
    }
}
