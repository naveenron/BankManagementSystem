using BankManagementSystemService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystemService.Data
{
    public class BankDBContext : DbContext
    {
        public BankDBContext(DbContextOptions<BankDBContext> opt_Db) : base(opt_Db)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Loan> Loan { get; set; }
    }
}
