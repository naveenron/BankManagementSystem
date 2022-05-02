using BankManagementSystemService.Data.Entities;

namespace BankManagementSystemService.Repositories.Registration
{
    public interface IRegisterService
    {
        Customer CreateAccount(Customer customer);
    }
}
