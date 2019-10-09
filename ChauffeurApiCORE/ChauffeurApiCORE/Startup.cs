using System;
using System.Text;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Configurations;
using ChauffeurApiCORE.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.UriParser;

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
			services.AddRouting();
			services.AddOData();
			services.AddODataQueryFilter();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(cfg =>
					{
						cfg.RequireHttpsMetadata = false;
						cfg.SaveToken = true;
						cfg.TokenValidationParameters = new TokenValidationParameters
						{
							ValidIssuer = ConfigurationManager.AppSettings["JwtIssuer"],
							ValidAudience = ConfigurationManager.AppSettings["JwtIssuer"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["JwtKey"])),
							ClockSkew = TimeSpan.Zero
						};
					});

			RegisterServices(services);
			RegisterCommands(services);

			services
				.AddMvcCore(options =>
				{
					options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
				})
				.AddFormatterMappings()
				.AddJsonFormatters()
				.AddAuthorization()
				.AddApiExplorer()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

			app
			.UseAuthentication()
			.UseMvc(routes =>
			{
				routes.EnableDependencyInjection(x => x.AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(ODataUriResolver), sp => new StringAsEnumResolver()));
			})
			.UseStaticFiles()
			.Build();
		}

		void RegisterCommands(IServiceCollection services)
		{
			services.AddSingleton<ITestCommand, TestCommand>();
			services.AddSingleton<ICreateCustomerCommand, CreateCustomerCommand>();
			services.AddSingleton<ICreateBookingCommand, CreateBookingCommand>();
			services.AddSingleton<IEditBookingCommand, EditBookingCommand>();
			services.AddSingleton<ICancelBookingCommand, CancelBookingCommand>();
			services.AddSingleton<ICreateDriverCommand, CreateDriverCommand>();
			services.AddSingleton<IEditDriverCommand, EditDriverCommand>();
			services.AddSingleton<ICreateStopCommand, CreateStopCommand>();
			services.AddSingleton<IGenerateTokenCommand, GenerateTokenCommand>();
		}

		void RegisterQueries(IServiceCollection services)
		{

		}

		void RegisterFactories(IServiceCollection services)
		{

		}

		void RegisterServices(IServiceCollection services)
		{
			var dbConnectionStr = ConfigurationManager.AppSettings["DefaultConnection"];
			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(dbConnectionStr);
			})
			.AddDefaultIdentity<ApplicationUser>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddUserManager<UserManager<ApplicationUser>>()
			.AddDefaultTokenProviders();

			services.AddScoped<IChaufferDbContext>(x => x.GetRequiredService<ApplicationDbContext>());
		}
	}	
}
