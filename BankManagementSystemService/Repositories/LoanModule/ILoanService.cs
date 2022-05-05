using BankManagementSystemService.Data.Entities;
using System.Collections.Generic;

namespace BankManagementSystemService.Repositories.LoanModule
{
    public interface ILoanService
    {
        Loan ApplyLoan(Loan loan);

        List<Loan> GetAllLoanDetails();
    }
}
