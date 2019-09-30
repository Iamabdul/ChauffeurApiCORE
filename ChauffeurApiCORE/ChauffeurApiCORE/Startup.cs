using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChauffeurApiCORE
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration Configuration)
		{
			this.Configuration = Configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles()
			   .Build();
		}
	}
}
