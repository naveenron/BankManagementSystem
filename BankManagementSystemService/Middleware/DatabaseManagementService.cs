using BankManagementSystemService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankManagementSystemService.Middleware
{
    public static class DatabaseManagementService
    {
        //to check if there is migrations which is not implemented in the db to keep the database updated with the latest migration
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Takes all of our migrations files and apply them against the database in case they are not implemented
                serviceScope.ServiceProvider.GetService<BankDBContext>().Database.Migrate();
            }
        }
    }
}
