using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

using AuthApp.Data;

namespace AuthApp.Services
{
	public static class PrepDb
	{
		public static void PrepPopulation(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
			}
		}
		public static void SeedData(ApplicationDbContext context)
		{
			System.Console.WriteLine("Applying migrations, standby!!!");
			context.Database.Migrate();
		}

	}


}
