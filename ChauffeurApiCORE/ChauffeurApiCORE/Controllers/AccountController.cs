using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Models;
using ChauffeurApiCORE.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		readonly UserManager<ApplicationUser> userManager;
		readonly SignInManager<ApplicationUser> signInManager;
		readonly IGenerateTokenCommand generateTokenCommand;

		public AccountController
		(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IGenerateTokenCommand generateTokenCommand
		)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.generateTokenCommand = generateTokenCommand;
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<ActionResult> Register(RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };

			var result = await userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		// POST api/Account/Login
		[AllowAnonymous]
		[HttpPost]
		[Route("Login")]
		[Consumes("application/x-www-form-urlencoded")]
		public async Task<ActionResult> Login([FromForm]LoginBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return NotFound();

			var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe , false);

			if (!result.Succeeded)
			{
				return BadRequest();
			}

			var jwtToken = generateTokenCommand.Execute(user);
			var successResponse = new
			{
				jwtToken,
				user.Email
			};

			return Ok(successResponse);
		}

		ActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return BadRequest();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
