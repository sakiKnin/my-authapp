using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AuthApp.Services;
using AuthApp.Data;

namespace AuthApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
	    var root = Directory.GetCurrentDirectory();
	    var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);
	    
            CreateHostBuilder(args)
			.ConfigureAppConfiguration((hostingContext, config)=>
			{
				
				var env = hostingContext.HostingEnvironment;

				if (env.IsDevelopment() || env.EnvironmentName.ToLower() == "dev")
                    			config.AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true);
                		else if (env.IsStaging() || env.EnvironmentName.ToLower() == "stage")
                    			config.AddJsonFile("appsettings.stage.json", optional: true, reloadOnChange: true);                    

                		config.AddEnvironmentVariables();

			})
			.Build()
			.MigrateDatabase<ApplicationDbContext>()
			.Run();
        }
	private static bool IsDevelopment =>
        	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

	public static string HostPort =>
        	IsDevelopment
            		? "80"
            		: Environment.GetEnvironmentVariable("PORT");
        
	
        public static IHostBuilder CreateHostBuilder(string[] args) =>

	    
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    
		    webBuilder.UseUrls($"http://+:{HostPort}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
