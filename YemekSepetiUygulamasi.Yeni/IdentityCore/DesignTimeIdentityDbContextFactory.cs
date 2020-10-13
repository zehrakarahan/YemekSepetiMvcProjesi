using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.IdentityCore
{
    public class DesignTimeIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
            //This code Use To MsSql
            //var connectionString = configuration.GetConnectionString("IdentityConnection");
            var connectionString = configuration.GetConnectionString("MySqlConnecitonUserIdentity");
            builder.UseMySql(connectionString);
            return new ApplicationIdentityDbContext(builder.Options);

        }
    }
}
