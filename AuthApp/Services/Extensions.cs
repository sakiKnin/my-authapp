using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AuthApp.Data;

namespace AuthApp.Services
{ 
	public static class Extensions
	{

		public static IHost MigrateDatabase<DbContext>(this IHost webHost)
		{
    			var serviceScopeFactory =  webHost.Services;

    			using(var scope = serviceScopeFactory.CreateScope())
    			{
        			var services = scope.ServiceProvider;
        			try
        			{
					SeedData(services.GetRequiredService<ApplicationDbContext>());
            				 
       				 }
        			catch (Exception ex)
        			{
            				var logger = services.GetRequiredService<ILogger<Program>>();
            				logger.LogError(ex, "An error occurred while migrating the database.");
        			}
    			}
    			return webHost;
		}
	        public static void SeedData(ApplicationDbContext context)
		{
			System.Console.WriteLine("Applying migrations...");
			context.Database.Migrate();
		}
	}
}
