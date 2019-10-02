using System.IO;
using Microsoft.Extensions.Configuration;

namespace ChauffeurApiCORE.Configurations
{
	static class ConfigurationManager
	{
		public static IConfiguration AppSettings { get; }

		static ConfigurationManager()
		{
			AppSettings = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json")
#if !DEBUG
							.AddJsonFile("appsettings.Release.json", optional: true)
							.AddEnvironmentVariables()
                            .Build();
#else
							.AddEnvironmentVariables()
							.Build();
#endif
		}
	}
}
