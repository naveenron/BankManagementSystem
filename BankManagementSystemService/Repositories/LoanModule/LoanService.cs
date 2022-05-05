using BankManagementSystemService.Data;
using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Middleware.Error;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                if (IsCustomerIdExists(loan.CustomerId))
                {
                    _bankDbContext.Loan.Add(loan);
                    _bankDbContext.SaveChanges();
                    return loan;
                }
                throw new AppException("Customer Id doesn't exists");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
            finally
            {
                if (_bankDbContext != null)
                    _bankDbContext.Dispose();
            }
        }

        public List<Loan> GetAllLoanDetails()
        {
            try
            {
                var Loans = _bankDbContext.Loan?.ToList();
                return Loans;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
            finally
            {
                if (_bankDbContext != null)
                    _bankDbContext.Dispose();
            }
        }

        public bool IsCustomerIdExists(int id)
        {
            try
            {
                bool isExist = _bankDbContext.Customer.Where(x=>x.Id == id).Any();
                return isExist;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message, ex);
            }
            finally
            {
                if (_bankDbContext != null)
                    _bankDbContext.Dispose();
            }
        }
    }
}
