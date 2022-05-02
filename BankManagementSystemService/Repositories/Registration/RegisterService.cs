using BankManagementSystemService.Data;
using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Middleware.Error;
using BankManagementSystemService.Utilities;
using Microsoft.Extensions.Logging;
using System;

namespace BankManagementSystemService.Repositories.Registration
{
    public class RegisterService : IRegisterService
    {
        private readonly ILogger _logger;
        private readonly BankDBContext _context;
        public RegisterService(BankDBContext context, ILogger<RegisterService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Customer CreateAccount(Customer customer)
        {
            try
            {
                if (!Utility.IsValidEmail(customer.EmailAddress))
                    throw new AppException("Email Address is not valid.");
                if (!Utility.IsValidPan(customer.Pan))
                    throw new AppException("Pan Number is not valid.");
                if (!Utility.IsValidMobileNumber(customer.ContactNo))
                    throw new AppException("Phone Number is not valid.");

                _logger.LogInformation("Assigning Current UTC Date");
                customer.CreatedDate = DateTime.UtcNow;

                _logger.LogInformation("Saving a data in the DB");
                _context.Customer.Add(customer);
                _context.SaveChanges();
                _logger.LogInformation("Data saved in the DB");
                return customer;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
