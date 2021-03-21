using System;
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
	    Console.WriteLine("This is my port number: " + HostPort);
            CreateHostBuilder(args)
			.Build()
			.MigrateDatabase<ApplicationDbContext>()
			.Run();
        }
	private static bool IsDevelopment =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

	public static string HostPort =>
        	IsDevelopment
            		? "5000"
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
