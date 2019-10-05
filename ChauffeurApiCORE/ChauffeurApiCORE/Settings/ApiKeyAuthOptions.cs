using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace ChauffeurApiCORE.Settings
{
	public class ApiKeyAuthOptions : AuthenticationSchemeOptions
	{
		public const string DefaultScheme = "apikey";
		public string Scheme => DefaultScheme;
		public StringValues ApiKey { get; set; }
	}
}
