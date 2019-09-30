using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace ChauffeurApiCORE
{
	public class Startup
	{
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
