using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace ChauffeurApiCORE.Settings
{
	public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthOptions>
	{

		public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
				return Task.FromResult(AuthenticateResult.Fail("Cannot read authorization header."));

			if (authorization.Any(key => Options.ApiKey.All(ak => ak != key)))
				return Task.FromResult(AuthenticateResult.Fail("Invalid api key."));

			var claims = new List<Claim> { new Claim(ClaimTypes.Role, "admin") };
			var identities = new List<ClaimsIdentity> { new ClaimsIdentity(claims, "apikey") };
			var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);

			return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}