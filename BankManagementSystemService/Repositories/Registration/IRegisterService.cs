using BankManagementSystemService.Data.Entities;
using System.Collections.Generic;

namespace BankManagementSystemService.Repositories.Registration
{
    public interface IRegisterService
    {
        Customer CreateAccount(Customer customer);

        List<Customer> GetAccountDetails();
    }
}
