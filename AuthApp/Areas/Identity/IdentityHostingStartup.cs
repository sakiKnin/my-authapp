using System;
using AuthApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore;

using AuthApp.Areas.Identity.Data;

[assembly: HostingStartup(typeof(AuthApp.Areas.Identity.IdentityHostingStartup))]
namespace AuthApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                /*services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));*/
		services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
			{
				var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

				string connectionString;
				 
				if(env == "Development")
				{
					connectionString = context.Configuration.GetConnectionString("DefaultConnection");

				}else
				{
					var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        				// Parse connection URL to connection string for Npgsql
        				connUrl = connUrl.Replace("postgres://", string.Empty);
        				var pgUserPass = connUrl.Split("@")[0];
        				var pgHostPortDb = connUrl.Split("@")[1];
        				var pgHostPort = pgHostPortDb.Split("/")[0];
        				var pgDb = pgHostPortDb.Split("/")[1];
        				var pgUser = pgUserPass.Split(":")[0];
        				var pgPass = pgUserPass.Split(":")[1];
        				var pgHost = pgHostPort.Split(":")[0];
        				var pgPort = pgHostPort.Split(":")[1];
        				connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};sslmode=Require;Trust Server Certificate=true";
				}
				 
				options.UseNpgsql(connectionString);
			});
                services.AddEntityFrameworkNpgsql().AddDefaultIdentity<ApplicationUser>(options => 
			{
			options.SignIn.RequireConfirmedAccount = false; 
			options.Password.RequireLowercase=false;
			options.Password.RequireUppercase=false;
			options.Password.RequireNonAlphanumeric=false;
			})
                    .AddEntityFrameworkStores<ApplicationDbContext>()
		    .AddDefaultTokenProviders();
		//services.AddMvc();
            });
        }
    }
}
