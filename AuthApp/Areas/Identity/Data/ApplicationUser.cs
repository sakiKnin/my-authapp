using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace AuthApp.Areas.Identity.Data
{
	public class ApplicationUser:IdentityUser
	{
		public string FirstName{get; set;}

                public string LastName{get; set;}


	}
}
