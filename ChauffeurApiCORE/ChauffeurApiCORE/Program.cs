using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
					options.ListenAnyIP(80);
					options.Configure();
				})
				.Build();

			await webHost.RunAsync();
		}
	}
}
