using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChauffeurApiCORE
{
	class Program
	{

		static void Main(string[] args)
		{
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseKestrel()
				.Build();
		}
	}
}
