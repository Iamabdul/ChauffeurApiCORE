using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChauffeurApiCORE.Configurations;
using ChauffeurApiCORE.Models;
using Microsoft.IdentityModel.Tokens;

namespace ChauffeurApiCORE.Commands
{
	public class GenerateTokenCommand : IGenerateTokenCommand
	{
		public string Execute(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.UserName)
			};

			var jwtkey = ConfigurationManager.AppSettings["JwtKey"];
			var expiryInDays = ConfigurationManager.AppSettings["JwtExpireDays"];
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(expiryInDays));

			var token = new JwtSecurityToken(
								null,
								null,
								claims,
								expires: expires,
								signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}

	public interface IGenerateTokenCommand
	{
		string Execute(ApplicationUser user);
	}
}
