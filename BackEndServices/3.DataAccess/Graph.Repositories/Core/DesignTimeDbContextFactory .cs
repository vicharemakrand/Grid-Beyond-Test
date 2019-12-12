using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Graph.Repositories.Core
{

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public virtual DataContext CreateDbContext(string[] args)
        {
            DbContextOptions<DataContext> options = GetDbContextOptions();
            return new DataContext(options);
        }

        public static DbContextOptions<DataContext> GetDbContextOptions()
        {

            var enviornmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = Path.GetDirectoryName(typeof(DesignTimeDbContextFactory).Assembly.Location);
            var configuration = new ConfigurationBuilder()
                                       .SetBasePath(basePath)
                                       .AddJsonFile("appsettings.json")
                                       .AddJsonFile($"appsettings.{enviornmentName}.json", optional: false)
                                       .Build();

            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DataContext).Assembly.GetName().Name));
            return builder.Options;
        }
    }
}
