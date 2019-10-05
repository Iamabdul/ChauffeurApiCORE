using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ChauffeurApiCORE
{
	class Program
	{
		static IWebHost webHost;
		static async Task Main(string[] args)
		{
			webHost =  new WebHostBuilder()
				.UseStartup<Startup>()
				.UseKestrel(options =>
				{
					options.ListenAnyIP(99);
					options.Configure();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddConsole();
				})
				.Build();

			await webHost.RunAsync();
		}
	}
}
