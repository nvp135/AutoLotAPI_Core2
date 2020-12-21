using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace AutoLotDAL_Core2.EF
{
    class AutoLotContextFactory : IDesignTimeDbContextFactory<AutoLotContext>
    {
        public AutoLotContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AutoLotContext>();
            
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("AutoLot");

            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
                   //.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            return new AutoLotContext(optionsBuilder.Options);
        }
    }
}
