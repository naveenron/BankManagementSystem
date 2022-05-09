using BankManagementSystemService.Data;
using BankManagementSystemService.Data.Entities;
using BankManagementSystemService.Middleware.Error;
using BankManagementSystemService.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
                //validation checks
                if (!Utility.IsValidEmail(customer.EmailAddress))
                    throw new AppException("Email Address is not valid.");
                if (!Utility.IsValidPan(customer.Pan))
                    throw new AppException("Pan Number is not valid.");
                if (!Utility.IsValidMobileNumber(customer.ContactNo))
                    throw new AppException("Phone Number is not valid.");

                _logger.LogInformation("Checking user is already exists or not");
                var isExist = _context.Customer.Where(x=>x.Username == customer.Username).Any();

                if (!isExist)
                {
                    _logger.LogInformation("Assigning Current UTC Date");
                    customer.CreatedDate = DateTime.UtcNow;

                    _logger.LogInformation("Saving a data in the DB");
                    _context.Customer.Add(customer);
                    _context.SaveChanges();
                    _logger.LogInformation("Data saved in the DB");
                    return customer;
                }
                else
                {
                    _logger.LogError("Username already exists");
                    throw new AppException("Username already exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                if (_context != null)
                    _context.Dispose();
            }
        }

        public List<Customer> GetAccountDetails()
        {
            try 
            {
                var data = _context.Customer.ToList();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            finally
            {
                if (_context != null)
                    _context.Dispose();
            }
        }
    }
}
