using ChauffeurApiCORE.Configurations;
using ChauffeurApiCORE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChauffeurApiCORE
{
	internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
			builder.UseSqlServer(ConfigurationManager.AppSettings["DefaultConnection"]);
			var context = new ApplicationDbContext(builder.Options);
			return context;
		}
	}
}
